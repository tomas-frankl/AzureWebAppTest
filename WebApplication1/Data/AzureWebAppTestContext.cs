using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using MySql.Data.EntityFramework;
using System.Linq;
using System.Web;

namespace AzureWebAppTest.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AzureWebAppTestContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public AzureWebAppTestContext() : base("name=MYSQLCONNSTR_localdb")
        {
            var log = $"ConnectionString: {this.Database.Connection.ConnectionString}";
            Console.WriteLine(log);
            Trace.WriteLine(log);
        }

        public System.Data.Entity.DbSet<AzureWebAppTest.Models.Poi> Pois { get; set; }
    }
}
