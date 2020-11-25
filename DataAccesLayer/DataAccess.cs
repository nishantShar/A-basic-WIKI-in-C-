using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApplication11.Models;

namespace DataAccesLayer
{
    public class DataAccess
    {
        #region Methods

        public Boolean IsAuthenticatedUser(string username, string password)
        {
            using (CrossLinkDBEntities db = new CrossLinkDBEntities())
            {
                foreach (var user in db.User_Details)
                {
                    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && user.userId.Equals(username) && user.password.Equals(password))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public List<Page_Container_DTO> getAllPage()
        {
            using (CrossLinkDBEntities crossLinkContext = new CrossLinkDBEntities())
            {
                return (from pc in crossLinkContext.Page_Content
                            join ver in crossLinkContext.Page_Latest_Version
                            on pc.pageId equals ver.pageId
                            where pc.version == ver.latestVersion
                            select new Page_Container_DTO
                            {
                                header = pc.header,
                                footer = pc.footer,
                                pageId = pc.pageId,
                                version = ver.latestVersion,
                                content = pc.content,
                                id = pc.id
                            }).ToList();
            }
        }

        public Page_Content getPage(int pageNo,int version)
        {
            using (CrossLinkDBEntities crossLinkContext = new CrossLinkDBEntities())
            {
                return crossLinkContext.Page_Content.Where(x => x.pageId == pageNo).Where(x=>x.version==version).FirstOrDefault();
            }
        }

        public int  UpdatePageContent(string pageId, string headerContent, string bodyContent)
        {
            using (CrossLinkDBEntities crossLinkContext = new CrossLinkDBEntities())
            {
                var pagevalue = int.Parse(pageId);
                var version = crossLinkContext.Page_Latest_Version.Where(x => x.pageId.Equals(pagevalue)).FirstOrDefault();
                var page = new Page_Content(headerContent.Replace("\n", String.Empty).Trim(), bodyContent, pagevalue, version.latestVersion + 1);
                int latest=version.latestVersion = version.latestVersion + 1;
                crossLinkContext.Page_Content.Add(page);
                crossLinkContext.SaveChanges();
                return latest;
            }
        }

        public void AddNewPage(string headerContent, string bodyContent, bool isOwnedPage,string userName)
        {
            using (CrossLinkDBEntities crossLinkContext = new CrossLinkDBEntities())
            {
                var pageList = new List<Page_Content>();
                var pageContent = new Page_Content(headerContent, bodyContent, getLastPageId() + 1,1);
                pageList.Add(pageContent);

                var Versions = new List<Page_Latest_Version>();
                var latestVersion = new Page_Latest_Version(getLastPageId() + 1,1);
                Versions.Add(latestVersion);

                var newPage = new Page_Details(isOwnedPage, getLastPageId() + 1, pageList, userName, Versions);

                crossLinkContext.Page_Details.Add(newPage);
                crossLinkContext.SaveChanges();
            }
        }

        public int getLastPageId()
        {
            using (CrossLinkDBEntities crossLinkContext = new CrossLinkDBEntities())
            {
                return crossLinkContext.Page_Details.OrderByDescending(x => x.pageId).Select(x => x.pageId).FirstOrDefault();
            }
        }

        public string getPageOwnerId(int pageId)
        {
            using (CrossLinkDBEntities crossLinkContext = new CrossLinkDBEntities())
            {
                return crossLinkContext.Page_Details.Where(x=>x.pageId==pageId).Select(x=>x.owner).FirstOrDefault();
            }
        }

        public bool? isOwned(int pageId)
        {
            using (CrossLinkDBEntities crossLinkContext = new CrossLinkDBEntities())
            {
                return crossLinkContext.Page_Details.Where(x => x.pageId == pageId).Select(x => x.isOwnedPage).FirstOrDefault();
            }
        }

        public bool? isAdmin(string username)
        {
            using (CrossLinkDBEntities cle = new CrossLinkDBEntities())
            {
                return cle.User_Details.Where(x => x.userId == username).Select(x => x.isadmin).FirstOrDefault();
            }
        }

        public Page_Content RevertLatestChanges(string pageNo, string version)
        {
            using (CrossLinkDBEntities cle = new CrossLinkDBEntities())
            {
                int pageValue = int.Parse(pageNo);
                int pageVersion = int.Parse(version);
                var versionDetails = cle.Page_Latest_Version.Where(x => x.pageId == pageValue).FirstOrDefault();
                versionDetails.latestVersion = versionDetails.latestVersion - 1;
                var latestVersion = cle.Page_Content.Where(x => x.pageId == pageValue).Where(x => x.version == pageVersion).FirstOrDefault();
                cle.Page_Content.Remove(latestVersion);
                cle.SaveChanges();
                return getPage(pageValue, pageVersion - 1);
            }
        }
     
        #endregion  
    }
}
