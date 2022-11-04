using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using Antlr.Runtime.Tree;

namespace AzureWebAppTest.Models
{
    public enum Category
    {
        Peaks,
        Settlements,
        Historical,
        Transmitters,
        ViewTowers
    }

    //[TableName("poi")]
    public class Poi
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string CountryCode { get; set; }
    }
}