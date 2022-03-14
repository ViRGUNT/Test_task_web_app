using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_app_NEWS.Models;
using System.Data;

namespace Web_app_NEWS.Controllers
{
    
    public class HomeController : Controller
    {

        private DBContextModel db = new DBContextModel();


        public ActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
            pageSize = (pageSize <= 0) ? 10 : pageSize;

            var rezult = db.News.ToList();

            var totalRecords = rezult.Count();
            var totalPages = Math.Ceiling((double)totalRecords / pageSize);


            var skip = (pageNumber - 1) * pageSize;


            var news = new List<NewsViewModel>();


            news = rezult.Skip(skip).Take(pageSize).Select(u => new NewsViewModel()
            {
                ID = u.ID,
                Publication_Date = u.Publication_Date,
                Guid = u.Guid,
                SourseUrl = u.SourseUrl,
                SourceName = u.SourceName,
                News_title = u.News_title,
                News_description = u.News_description
            }).OrderBy(o => o.ID).ToList();



            return View(news);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }




        ////////////////////////////////////////////////////////////////////////////




        [HttpPost]
        public ActionResult Index(string Source_Name, bool sortToDate, bool sortToSource, int pageNumber = 1, int pageSize = 10)
        {

            var rezult = Read(Source_Name, sortToDate, sortToSource, pageNumber, pageSize);

            return View(rezult);


        }

   
        public IList<NewsViewModel> GetAllNews(string Source_Name, bool sortToDate, bool sortToSource, int pageNumber = 1, int pageSize = 10)
        {
          
            pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
            pageSize = (pageSize <= 0) ? 10 : pageSize;

            var rezults = db.News.ToList();

            if (Source_Name == "all")
            {
                if (sortToDate == false && sortToSource == false)
                {
                    rezults = db.News.OrderBy(o => o.ID).ToList();
                }
                else if (sortToDate == true && sortToSource == false)
                {
                    rezults = db.News.OrderBy(o => o.Publication_Date).ToList();
                }
                else if (sortToDate == false && sortToSource == true)
                {
                    rezults = db.News.OrderBy(o => o.SourceName).ToList();
                }
            }
            else
            {
                if (sortToDate == false && sortToSource == false)
                {
                    rezults = db.News.Where(w => w.SourceName == Source_Name).OrderBy(o => o.ID).ToList();
                }
                else if (sortToDate == true && sortToSource == false)
                {
                    rezults = db.News.Where(w => w.SourceName == Source_Name).OrderBy(o => o.Publication_Date).ToList();
                }
                else if (sortToDate == false && sortToSource == true)
                {
                    rezults = db.News.Where(w => w.SourceName == Source_Name).OrderBy(o => o.SourceName).ToList();
                }
                if (sortToDate == true && sortToSource == true)
                {
                    rezults = db.News.Where(w => w.SourceName == Source_Name).OrderBy(o => o.ID).ToList();
                }
            }


            var totalRecords = rezults.Count();
            var totalPages = Math.Ceiling((double)totalRecords / pageSize);


            var skip = (pageNumber - 1) * pageSize;


            var news = new List<NewsViewModel>();


            news = rezults.Skip(skip).Take(pageSize).Select(u => new NewsViewModel()
            {
                ID = u.ID,
                Publication_Date = u.Publication_Date,
                Guid = u.Guid,
                SourseUrl = u.SourseUrl,
                SourceName = u.SourceName,
                News_title = u.News_title,
                News_description = u.News_description
            }).ToList();


            return news;
        }

        public IEnumerable<NewsViewModel> Read(string Source_Name, bool sortToDate, bool sortToSource, int pageNumber = 1, int pageSize = 10)
        {
            return GetAllNews(Source_Name, sortToDate, sortToSource, pageNumber, pageSize);
        }

    }
}