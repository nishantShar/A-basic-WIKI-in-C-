using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication11.Models
{
    public class MarkDownPageModel
    {
        public MarkDownPageModel(string header, string footer, string content, int pageId, int version)
        {
            Header = header;
            Footer = footer;
            Content = content;
            PageId = pageId;
            Version = version;
        }
        private string _header;
        private string _footer;
        private string _content;
        private int _pageId;
        private int _version;

        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
            }
        }
        public string Footer
        {
            get
            {
                return _footer;
            }
            set
            {
                _footer = value;
            }
        }

        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }
        public int PageId
        {
            get
            {
                return _pageId;
            }
            set
            {
                _pageId = value;
            }
        }
        public int Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
            }
        }
    }
}