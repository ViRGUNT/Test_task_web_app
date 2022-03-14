using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using Web_app_NEWS.Models;

namespace Web_app_NEWS.Models
{
    [Table(Name = "News")]
    public class News
    {

        [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "ID")]
        public int ID { get; set; }

        [Column(Name = "Дата публикации")]
        public DateTime Publication_Date { get; set; }

        [Column(Name = "Guid")]
        public string Guid { get; set; }

        [Column(Name = "SourseUrl")]
        public string SourseUrl { get; set; }

        [Column(Name = "Источник")]
        public string SourceName { get; set; }

        [Column(Name = "Название новости")]
        public string News_title { get; set; }

        [Column(Name = "Описание новости")]
        public string News_description { get; set; }
    }
}
