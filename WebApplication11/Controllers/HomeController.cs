using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication11.Models;

namespace WebApplication11.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        #region Intialization
        //Creating instance for data access layer
        DataAccesLayer.DataAccess dataAccess = new DataAccesLayer.DataAccess();
        Utility utility = new Utility();

        #endregion

        #region Methods

        public ActionResult LoginView()
        {
            return View("Login");
        }

        public ActionResult Login(LoginModel loginModel)
        {
            ViewBag.Error = null;
            if (dataAccess.IsAuthenticatedUser(loginModel.UserId, loginModel.Password))
            {
                Session["username"] = loginModel.UserId;
                Session["isLoggedIn"] = true;
                Session["isAdmin"] = dataAccess.isAdmin(loginModel.UserId);
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Wrong Username/Password";
            return View("Login");
        }

        public ActionResult Logout()
        {
            Session["isLoggedIn"] = null;
            Session["username"] = null;
            return View("Login");
        }

        [Filters.AuthorizeUser]
        public ActionResult Index()
        {
            ViewBag.PageList = dataAccess.getAllPage();
            return View("Index");
        }

        [Filters.AuthorizeUser]
        [HttpGet]
        public ActionResult ViewPage(int pageNo,int version)
        {   
            ViewBag.pageViewModel = utility.getHtmlPageContent(dataAccess.getPage(pageNo, version));
            ViewBag.UserName= dataAccess.getPageOwnerId(pageNo);
            ViewBag.isOwned= dataAccess.isOwned(pageNo);
            return View("ViewPage");
        }

        [Filters.AuthorizeUser]
        public ActionResult EditPage(int pageNo,int versionNo)
        {
            ViewBag.markDownPageViewModel = utility.getMarkDownPageContent(dataAccess.getPage(pageNo, versionNo));
            return View("EditPage");
        }

        [Filters.AuthorizeUser]
        [ValidateInput(false)]
        public ActionResult SaveChanges(string pageId, string headerContent, string bodyContent)
        {
            var latesversion= dataAccess.UpdatePageContent(pageId, headerContent, bodyContent);
            ViewBag.pageViewModel = utility.getHtmlPageContent(dataAccess.getPage(int.Parse(pageId), latesversion));
            return View("ViewPage");
        }

        [Filters.AuthorizeUser]
        public ActionResult AddNewPage()
        {
            return View("AddNewPage");
        }

        [Filters.AuthorizeUser]
        public ActionResult NewPage(string headerContent, string bodyContent,bool isOwned)
        {
            dataAccess.AddNewPage(headerContent, bodyContent, isOwned, Session["username"].ToString());
            ViewBag.PageList = dataAccess.getAllPage();
            return View("Index");
        }

        [Filters.AuthorizeUser]
        [HttpGet]
        public ActionResult RevertLatestChanges(string pageNo, string versionNo)
        {
            ViewBag.pageViewModel = utility.getHtmlPageContent(dataAccess.RevertLatestChanges(pageNo, versionNo));
            /*ViewBag.UserName = dataAccess.getPageOwnerId(int.Parse(pageNo));
            ViewBag.isOwned = dataAccess.isOwned(int.Parse(pageNo));
            ViewBag.Reverted = true;*/
            return Json(new
            {
                pageNo = pageNo,
                version = int.Parse(versionNo)-1
            });
            //return RedirectToAction("ViewPage", "Home", new { pageNo = pageNo, version= int.Parse(versionNo)-1 });
            //return RedirectToAction("ViewPage", "Index");
            //return View("ViewPage");
        }
        #endregion
    }
}