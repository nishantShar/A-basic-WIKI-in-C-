using DataAccesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication11.Models;

namespace WebApplication11
{
    public class Utility
    {
        public HtmlPageModel getHtmlPageContent(Page_Content pageContent)
        {
            return new HtmlPageModel(pageContent.header, pageContent.footer, pageContent.content, pageContent.pageId, pageContent.version);
        }
        public MarkDownPageModel getMarkDownPageContent(Page_Content pageContent)
        {
            return new MarkDownPageModel(pageContent.header, pageContent.footer, pageContent.content, pageContent.pageId, pageContent.version);
        }
    }
}