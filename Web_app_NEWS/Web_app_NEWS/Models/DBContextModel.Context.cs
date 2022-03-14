using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Web_app_NEWS.Models;

namespace Web_app_NEWS.Models
{
    public class DBContextModel : DbContext
    {

        //Укажите свои данные для подключения к СУБД
        static string serverName = "Jupiter";
        static string User_ID = "Tester";
        static string password = "FidoTest978";


        static string EntityString = $"Data Source={serverName};Initial Catalog=DB_NEWS;user id={User_ID};password={password};Trusted_Connection=False;MultipleActiveResultSets=true;";


        public DBContextModel() : base(EntityString)
        {
        }

       




        public virtual DbSet<News> News { get; set; }





        public int SourceId { get; set; }
      
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }




}