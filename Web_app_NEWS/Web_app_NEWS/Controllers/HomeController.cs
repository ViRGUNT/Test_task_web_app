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

        private static DBContextModel db = new DBContextModel();


        public List<News> rezult = db.News.ToList();


        public IEnumerable<NewsViewModel> GetingNews(int pageNumber = 1, int pageSize = 10)
        {


            pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
            pageSize = (pageSize <= 0) ? 10 : pageSize;

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

            return news;
        }




        public List<SelectListItem> GetPages(string Source_Name, bool sortToDate, bool sortToSource, int pageNumber = 1, int pageSize = 10)
        {



            if (Source_Name == "Все")
            {
                if (sortToDate == false && sortToSource == false)
                {
                    rezult = db.News.OrderBy(o => o.ID).ToList();
                }
                else if (sortToDate == true && sortToSource == false)
                {
                    rezult = db.News.OrderBy(o => o.Publication_Date).ToList();
                }
                else if (sortToDate == false && sortToSource == true)
                {
                    rezult = db.News.OrderBy(o => o.SourceName).ToList();
                }
            }
            else
            {
                if (sortToDate == false && sortToSource == false)
                {
                    rezult = db.News.Where(w => w.SourceName == Source_Name).OrderBy(o => o.ID).ToList();
                }
                else if (sortToDate == true && sortToSource == false)
                {
                    rezult = db.News.Where(w => w.SourceName == Source_Name).OrderBy(o => o.Publication_Date).ToList();
                }
                else if (sortToDate == false && sortToSource == true)
                {
                    rezult = db.News.Where(w => w.SourceName == Source_Name).OrderBy(o => o.SourceName).ToList();
                }
                if (sortToDate == true && sortToSource == true)
                {
                    rezult = db.News.Where(w => w.SourceName == Source_Name).OrderBy(o => o.ID).ToList();
                }
            }


            var pages = Convert.ToInt32(Math.Round(((decimal)(rezult.Count() / pageSize)), 0));




            List<SelectListItem> items = new List<SelectListItem>();

            for (var n = 1; n <= pages; n++)
            {
                items.Add(new SelectListItem { Text = $"{n}", Value = $"{n}" });
            }



            return items;


        }


        public List<SelectListItem> GetSourse(int pageNumber = 1, int pageSize = 10)
        {


            var pages = Convert.ToInt32(Math.Round(((decimal)(rezult.Count() / pageSize)), 0));



            var sourceNames = db.News.Select(select => select.SourceName).Distinct().ToList();

            var countsourceNames = sourceNames.Count();

            List<SelectListItem> sourceNameItems = new List<SelectListItem>();


            sourceNameItems.Add(new SelectListItem { Text = "Все", Value = "Все" });

            for (var n = 1; n <= countsourceNames; n++)
            {
                sourceNameItems.Add(new SelectListItem { Text = $"{sourceNames.ElementAt(n - 1)}", Value = $"{sourceNames.ElementAt(n - 1)}" });
            }
           
            return sourceNameItems;


        }




        public IList<NewsViewModel> GetAllNews(string Source_Name, bool sortToDate, bool sortToSource, int pageNumber = 1, int pageSize = 10)
        {




            if (Source_Name == "Все")
            {
                if (sortToDate == false && sortToSource == false)
                {
                    rezult = db.News.OrderBy(o => o.ID).ToList();
                }
                else if (sortToDate == true && sortToSource == false)
                {
                    rezult = db.News.OrderBy(o => o.Publication_Date).ToList();
                }
                else if (sortToDate == false && sortToSource == true)
                {
                    rezult = db.News.OrderBy(o => o.SourceName).ToList();
                }
            }
            else
            {
                if (sortToDate == false && sortToSource == false)
                {
                    rezult = db.News.Where(w => w.SourceName == Source_Name).OrderBy(o => o.ID).ToList();
                }
                else if (sortToDate == true && sortToSource == false)
                {
                    rezult = db.News.Where(w => w.SourceName == Source_Name).OrderBy(o => o.Publication_Date).ToList();
                }
                else if (sortToDate == false && sortToSource == true)
                {
                    rezult = db.News.Where(w => w.SourceName == Source_Name).OrderBy(o => o.SourceName).ToList();
                }
                if (sortToDate == true && sortToSource == true)
                {
                    rezult = db.News.Where(w => w.SourceName == Source_Name).OrderBy(o => o.ID).ToList();
                }
            }


            var totalRecords = rezult.Count();
            var totalPages = Math.Ceiling((double)totalRecords / pageSize);


            var skip = (pageNumber - 1) * pageSize;


            var news = GetingNews(pageNumber, pageSize);


            return (IList<NewsViewModel>)news;
        }

        public IEnumerable<NewsViewModel> Read(string Source_Name, bool sortToDate, bool sortToSource, int pageNumber = 1, int pageSize = 10)
        {
            return GetAllNews(Source_Name, sortToDate, sortToSource, pageNumber, pageSize);
        }




   
        /// ///////////////////////////////////////////////////////////////
 


        public ActionResult Index(int pageNumber = 1, int pageSize = 10)
        {

            string Source_Name = "Все";
            bool sortToDate = false;
            bool sortToSource = false;

            ViewBag.pageNumber = GetPages(Source_Name, sortToDate, sortToSource, pageNumber, pageSize);


            ViewBag.Source_Name = GetSourse(pageNumber, pageSize);


            var rezunlNews = GetingNews(pageNumber, pageSize);



            return View(rezunlNews);
        }


        




        public ActionResult DinAjax(int pageNumber = 1, int pageSize = 10)
        {

            string Source_Name = "Все";
            bool sortToDate = false;
            bool sortToSource = false;


            ViewBag.pageNumber = GetPages(Source_Name, sortToDate, sortToSource, pageNumber, pageSize);


            ViewBag.Source_Name = GetSourse(pageNumber, pageSize);


            var rezunlNews = GetingNews(pageNumber, pageSize);

            NewsViewModel newsView = new NewsViewModel();

            newsView.pageNumber = pageNumber;
            newsView.pageSize = pageSize;




            return View(rezunlNews);
        }


        [HttpPost]
        public ActionResult ForDinAjax(string Source_Name, bool sortToDate, bool sortToSource, string pageNumber = "1", int pageSize = 10)
        {
            var startPage = 1;

            var maxPageSize = db.News.ToList().Count();

            var rezunlNews = Read(Source_Name, sortToDate, sortToSource, startPage, maxPageSize);


            if (Source_Name != "Все")
            {
                maxPageSize = db.News.Where(w => w.SourceName == Source_Name).ToList().Count();
            }

            var pageNumberInt = Convert.ToInt32(pageNumber);

            if ((Convert.ToInt32(Math.Round(((decimal)(rezunlNews.Count() / pageSize)), 0))) < pageNumberInt)
            {
                pageNumberInt = 1;
            }


            var rezults = Read(Source_Name, sortToDate, sortToSource, pageNumberInt, pageSize);



            return PartialView(rezults);
        }






        public ActionResult ForDinAjax(int pageNumber = 1, int pageSize = 10)
        {
            string Source_Name = "Все";
            bool sortToDate = false;
            bool sortToSource = false;



            var rezults = Read(Source_Name, sortToDate, sortToSource, pageNumber, pageSize);

            return PartialView(rezults);
        }

  




        ////////////////////////////////////////////////////////////////////////////




        [HttpPost]
        public ActionResult Index(string Source_Name, bool sortToDate, bool sortToSource, string pageNumber = "1", int pageSize = 10)
        {




            var startPage = 1;
            var maxPageSize = db.News.ToList().Count();

            if (Source_Name != "Все")
            {
                maxPageSize = db.News.Where(w=>w.SourceName == Source_Name).ToList().Count();
            }


            var pageNumberInt = Convert.ToInt32(pageNumber);


            var rezunlNews = Read(Source_Name, sortToDate, sortToSource, startPage, maxPageSize);


            if ((Convert.ToInt32(Math.Round(((decimal)(rezunlNews.Count() / pageSize)), 0)) ) < pageNumberInt)
            {
                pageNumberInt = 1;
            }

            ViewBag.pageNumber = GetPages(Source_Name, sortToDate, sortToSource, pageNumberInt, pageSize);


            ViewBag.Source_Name = GetSourse(pageNumberInt, pageSize);


            var rezults = Read(Source_Name, sortToDate, sortToSource, pageNumberInt, pageSize);


           


            return View(rezults);


        }








    }
}