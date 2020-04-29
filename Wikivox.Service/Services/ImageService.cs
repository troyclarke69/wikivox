using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Wikivox.Data;
using Wikivox.Data.Interfaces;
using Wikivox.Models;

namespace Wikivox.Service.Services
{
    public class ImageService : IImage
    {
        private readonly WikivoxDbContext _context;
        public ImageService(WikivoxDbContext context) { _context = context; }
        public void Add(Image newImage)
        {
            _context.Add(newImage);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Image.Where(g => g.Id == id)
               .ToList().ForEach(g => _context.Image.Remove(g));
            _context.SaveChanges();
        }

        public Image Get(int id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Image> GetAll()
        {
            return _context.Image;
        }

        public IEnumerable<string> GetAllImagesByEntity(int entityId, int resourceId)
        {
            var images = _context.Image
                .Where(i => i.Entity.Id == entityId
                && i.ResourceId == resourceId
                && i.PrimaryImage == 0);

            return (images != null) ? images.Select(a => a.ImgUrl) : null;
        }

        //[HttpPost]
        public void UploadImage(List<IFormFile> files, string entity, string resource,
                int entityId, int resourceId)
        {
            long size = files.Sum(f => f.Length);
            var filePaths = new List<string>();

            var imgRootfolder = entity;  //ex. albums
            var imgSubfolder = resource; //ex u2

            //if the imgSubfolder contains spaces, we must get rid of them
            imgSubfolder = imgSubfolder.Replace(" ", "");

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = formFile.FileName;
                  
                    //set Url in Images table
                    var imgUrl = "/images/" + imgRootfolder
                            + "/" + imgSubfolder
                                + "/" + fileName;

                    //set path for upload
                    var filePath = "wwwroot" + imgUrl;

                    //ensure that folder exists
                    var folder = "wwwroot/images/" + imgRootfolder
                            + "/" + imgSubfolder;

                    //create root and sub folders if they don't exist.
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);

                        //upon success, write to Images table
                        var entityModel = _context.Entity.First(e => e.Id == entityId);
                        var newImage = new Image
                        {
                            Entity = entityModel,
                            ResourceId = resourceId,
                            ImgUrl = imgUrl,
                            PrimaryImage = 1
                        };

                        //call local proc
                        Add(newImage);                      
                    }
                }
            }
        }

        public string GetPrimaryImageByEntity(int entityId, int resourceId, int primaryImage)
        {
            var image = _context.Image.FirstOrDefault(i => i.Entity.Id == entityId
                && i.ResourceId == resourceId && i.PrimaryImage == primaryImage);

            return (image != null) ? image.ImgUrl : "/images/no_image.jpg";
        }

        public void Update(Image newImage)
        {
            throw new NotImplementedException();
        }
    }
}
