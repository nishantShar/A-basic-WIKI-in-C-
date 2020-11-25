using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication11.Models
{
    public class Page_Container_DTO
    {
        public string header { get; set; }
        public string footer { get; set; }
        public string content { get; set; }
        public int pageId { get; set; }
        public int version { get; set; }
        public int id { get; set; }
    }
}