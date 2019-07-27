using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mug.Service;
using Mug.Service.Interface;
using Mug.Dao;
using Mug.Service.UI;
using Mug.Attribute;

namespace Mug.Controllers
{
    [AuthenticateUser]
    public class ContactController : Controller
    {
        private IContactService ContactService = new ContactService();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        #region Search 大圖查詢
        [HttpPost]
        public ActionResult Search()
        {
            var a= DateTime.Now.ToString("yyyy-MM-dd");          
            var contact = ContactService.GetAll();
            var result = (from b in contact
                          select new Contact
                          {
                            Id=b.Id,
                            Name=b.Name,
                            Phone=b.Phone,
                            Email=b.Email,
                            Service=b.Service,
                            Time = DBNull.Value.Equals(b.CreateTime) ? "" :ToYMD(b.CreateTime),
                            Memo=b.Memo
                          }).ToList();
            int totalLen = Convert.ToInt16(result.Count());
            JQueryDataTableResponse<Contact> jqDataTableRs = JQueryDataTableHelper<Contact>.GetResponse(1, totalLen, totalLen, result.ToList());
            return Json(jqDataTableRs);
        }
        #endregion
        public static string ToYMD(DateTime? dt)
        {
            if (dt.HasValue)
                return dt.Value.ToString("yyyy-MM-dd");
            else
                return "";
        }
    }

}