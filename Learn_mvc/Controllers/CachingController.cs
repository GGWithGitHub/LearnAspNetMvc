using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class CachingController : Controller
    {
        public ActionResult Index()
        {
            var postImages = HttpContext.Cache.Get("ThousandsPost");

            if (postImages == null)
            {
                postImages = this.getImagesPost();
                HttpContext.Cache.Insert("ThousandsPost", postImages, null, DateTime.Now.AddMinutes(10), Cache.NoSlidingExpiration);
            }
            return View(postImages);
        }

        public List<string> getImagesPost()
        {
            List<string> list_images = new List<string>();
            list_images.Add("Content/img/cafe-gc9cafb9d4_1920.jpg");
            list_images.Add("Content/img/dubai-g686b3fd0a_1920.jpg");
            list_images.Add("Content/img/nature-ge6f96ac7d_1920.jpg");
            return list_images;
        }
    }
}