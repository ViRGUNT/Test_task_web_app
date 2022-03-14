using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel;

namespace Web_app_NEWS.Models
{
    public class NewsViewModel
    {
        
        public int ID { get; set; }
        [DisplayName("Дата публикации")]
        public DateTime Publication_Date { get; set; }
        [DisplayName("Идентификатор GUID")]
        public string Guid { get; set; }
        [DisplayName("Адрес источника")]
        public string SourseUrl { get; set; }
        [DisplayName("Источник")]
        public string SourceName { get; set; }
        [DisplayName("Название новости")]
        public string News_title { get; set; }
        [DisplayName("Описание новости")]
        public string News_description { get; set; }

        

        [DisplayName("Сортировать по дате")]
        public string sortToDate_Lable { get; set; }

        [DisplayName("Сортировать по источнику")]
        public string sortToSource_Lable { get; set; }








        ///////////////////




        public string Source_Name { get; set; }
        public bool sortToDate { get; set; }
        public bool sortToSource { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }


    }
}