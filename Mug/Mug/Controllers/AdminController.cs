using Mug.Dao;
using Mug.Models;
using Mug.Service;
using Mug.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Mug.Controllers
{
    public class AdminController : Controller
    {
        private MugFactoryEntities db = new MugFactoryEntities();
        private DbSet<Employee> dbset = null;
        // GET: Admin
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel emp)
        {
            if (ModelState.IsValid)
            {
                var loginEmp = CheckAccPwd(emp.Account, emp.Password);
                if (loginEmp == null)
                {
                    ViewBag.WrongLogin = "帳號密碼有誤，請重新輸入";
                    return View(emp);
                }
                else
                {
                    //send sessionid cookie to user
                    Response.Cookies.Add(new HttpCookie("JSESSIONID", HttpContext.Session.SessionID) { HttpOnly = true });
                    //set session key value pair
                    Session["EmpId"] = loginEmp.EmpId;

                    //返回登入前頁面
                    if (TempData["PageAfterLogin"] != null)
                    {
                        return Redirect(TempData["PageAfterLogin"].ToString());
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(emp);
        }

        //驗證登入者帳號密碼
        public Employee CheckAccPwd(string acc, string pwd)
        {
            var loginEmp = db.Employee.FirstOrDefault(e => e.Account == acc);
            if (loginEmp == null) return null;
 
            //varifypassword
            bool pwdcorrect = ValidatePassword(pwd, loginEmp.Salt, loginEmp.Password);
            if (!pwdcorrect) return null;
   
            return loginEmp;
        }
        public static bool ValidatePassword(string strPwd, byte[] saltFromDb, byte[] hashedPwdFromDb)
        {
            SHA384Managed alog = new SHA384Managed();
            byte[] bytesPwd = Encoding.Unicode.GetBytes(strPwd);
            byte[] bytesHashedPwd = alog.ComputeHash(bytesPwd.Concat(saltFromDb).ToArray());
            bool isValid = bytesHashedPwd.SequenceEqual(hashedPwdFromDb);

            return isValid;
        }


        public ActionResult Logout()
        {
            //登出
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
    }
}