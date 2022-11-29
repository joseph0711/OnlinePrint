using System.Collections.Generic;
using System.Web.Mvc;
using OnlinePrint.Service;
using OnlinePrint.Models;
using System;

namespace OnlinePrint.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult homepage()
        {
            if (User.Identity.IsAuthenticated && (string)Session["account"] != null)
            {
                HomepageDBService homepageDBService = new HomepageDBService();
                ViewBag.content = homepageDBService.GetHomepageContent();

                var token = Guid.NewGuid().ToString();
                Session[token] = DateTime.Now.ToString("HHmmss");
                ViewBag.Token = token;

                return View();
            }
            else
            {
                return RedirectToAction("login", "login");
            }
        }

        #region 申請紀錄
        public ActionResult currenthistory()
        {
            FormDBService formModel = new FormDBService();
            List<FormModels> formData = formModel.GetFormData("current");
            ViewBag.formData = formData;

            ViewBag.username = MembersDBService.member.name;

            if (User.Identity.IsAuthenticated && (string)Session["role"] == "User")
            {
                var token = Guid.NewGuid().ToString();
                Session[token] = DateTime.Now.ToString("HHmmss");
                ViewBag.Token = token;
                return View();
            }
            else
            {
                return RedirectToAction("login", "login");
            }
        }
        #endregion

        #region 歷史紀錄
        public ActionResult pasthistory()
        {
            FormDBService formModel = new FormDBService();
            List<FormModels> formData = formModel.GetFormData("past");
            ViewBag.formData = formData;

            ViewBag.username = MembersDBService.member.name;

            if (User.Identity.IsAuthenticated && (string)Session["role"] == "User")
            {
                var token = Guid.NewGuid().ToString();
                Session[token] = DateTime.Now.ToString("HHmmss");
                ViewBag.Token = token;
                return View();
            }
            else
            {
                return RedirectToAction("login", "login");
            }
        }
        #endregion

        [HttpPost]
        public JsonResult heartbeat(string token)
        {
            //讀取Session，避免逾時
            var chk = Session[token] as string;
            string content;

            if (string.IsNullOrEmpty(chk))
            {
                content = "Session lost";
                return Json(content);
            }
            content = "OK";
            return Json(content);
        }
    }
}