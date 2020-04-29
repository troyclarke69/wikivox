using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wikivox.VModels;

namespace Wikivox.Controllers
{
    public class SongModController : Controller
    {
        public ActionResult Index()
        {
            var model = new UserModel
            {
                SelectedSongIds = new[] { 2 },
                SongList = GetSongs()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(UserModel model)
        {
            model.SongList = GetSongs();
            if (model.SelectedSongIds != null)
            {
                List<SelectListItem> selectedItems = model.SongList
                    .Where(p => model.SelectedSongIds.Contains(int.Parse(p.Value))).ToList();
                foreach (var Tea in selectedItems)
                {
                    Tea.Selected = true;
                    ViewBag.Message += Tea.Text + " | ";
                }
            }
            return View(model);
        }

        public List<SelectListItem> GetSongs()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            // get songs
            // foreach

            items.Add(new SelectListItem { Text = "General Tea", Value = "1" });
            items.Add(new SelectListItem { Text = "Coffee", Value = "2" });
            items.Add(new SelectListItem { Text = "Green Tea", Value = "3" });
            items.Add(new SelectListItem { Text = "Black Tea", Value = "4" });

            return items;
        }
    }
}