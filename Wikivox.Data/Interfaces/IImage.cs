using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Wikivox.Models;

namespace Wikivox.Data.Interfaces
{
    public interface IImage
    {
        IEnumerable<Image> GetAll();
        Image Get(int id);
        void Add(Image newImage);
        void Update(Image newImage);
        void Delete(int id);
        string GetPrimaryImageByEntity(int entityId, int resourceId, int primaryImageId);  //PrimaryImage = 1++
        IEnumerable<string> GetAllImagesByEntity(int entityId, int resourceId);  //PrimaryImage = 0
        void UploadImage(List<IFormFile> files, string entity, string resource, 
                int entityId, int resourceId);
    }
}
