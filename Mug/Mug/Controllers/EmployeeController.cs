using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mug.Service;
using Mug.Service.Interface;
using Mug.Dao;
using Mug.Service.UI;
using Mug.Models;
using System.Text;
using System.Security.Cryptography;
using Mug.Attribute;

namespace Mug.Controllers
{
    [AuthenticateUser]
    public class EmployeeController : Controller
    {
      //  public MugFactoryEntities db = new MugFactoryEntities();
        private IEmployeePageService EmployeePageService = new EmployeePageService();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        #region Search 員工查詢
        [HttpPost]
        public ActionResult Search()
        {
            var Emp = EmployeePageService.GetAll();       
            var result = (from emp in Emp
                          select new Employee
                          {
                              Name = emp.Name,
                              Account = emp.Account
                          }).ToList();

            int totalLen = Convert.ToInt16(result.Count());
            JQueryDataTableResponse<Employee> jqDataTableRs = JQueryDataTableHelper<Employee>.GetResponse(1, totalLen, totalLen, result.ToList());
            return Json(jqDataTableRs);
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(string Account)
        {
            try
            {
                Employee _emp = EmployeePageService.GetByAccountID(Account);
                EmployeePageService.Delete(int.Parse(_emp.EmpId.ToString()));
                return Json(new { Status = "0", StatusDesc = "刪除成功" });
            }
            catch (Exception err)
            {

                return Json(new { Status = "2", StatusDesc = "刪除失敗,請洽管理人員" + err.Message });
            }

        }
        #endregion

        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(FormCollection form)
        {
           // string permissionResult = ua.checkPermission(Session, "Insert", "Employee");
            string Name = form["Name"];
            string Account = form["Account"];
            string Password = form["Password"];
            string PasswordConfirm = form["PasswordConfirm"];
            if (Name.Trim() == "")
            {
                return Json(new { Status = "1", Message = "請輸入姓名，請檢查" });
            }
            if (Account.Trim() == "")
            {
                return Json(new { Status = "1", Message = "請輸入帳號，請檢查" });
            }
            if (Password.Trim() == "" || PasswordConfirm.Trim() == "")
            {
                return Json(new { Status = "1", Message = "請輸入密碼，請檢查" });
            }
            if (Password != PasswordConfirm)
            {
                return Json(new { Status = "1", Message = "密碼驗證錯誤，請檢查" });
            }

            var q = EmployeePageService.GetAll().OrderByDescending(x => x.EmpId); 
      

            var MaxId = 0;
            if (q.Count() != 0)
            {
                MaxId = int.Parse(q.FirstOrDefault().EmpId.ToString());
            }
            int i = MaxId + 1;

            var count = q.Where(x => x.Account == Account).Count();
            if (count >= 1)
            {
                return Json(new { Status = "1", Message = "此帳號已有人使用，請檢查" });
            }
            ViewBag.now = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            //parse pwd
            Employee e = new Employee();
            var result = HashPassword(Password);
            e.EmpId = i;
            e.Name = Name;
            e.Account = Account;
            e.Password = result.HashedPwd;
            e.Salt = result.Salt;
            e.CreateTime = DateTime.Now;
           var chkInsert= EmployeePageService.Create(e);
     
            return Json(new { Status = "0" });
        }
        public static PwdSaltPair HashPassword(string strPwd, int saltsize = 20)
        {
            PwdSaltPair result = new PwdSaltPair();
            //把密碼轉成byte[]
            byte[] bytesPwd = Encoding.Unicode.GetBytes(strPwd);
            //製作salt
            byte[] bytesSalt = new byte[saltsize];
            SHA384Managed alog = new SHA384Managed();
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytesSalt);
            //把密碼+salt喇一喇
            byte[] bytesHashedPwd = alog.ComputeHash(bytesPwd.Concat(bytesSalt).ToArray());
            result.HashedPwd = bytesHashedPwd;
            result.Salt = bytesSalt;
            return result;
        }
        public class PwdSaltPair
        {
            public byte[] HashedPwd { get; set; }
            public byte[] Salt { get; set; }
        }
    }
}