using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mug.Service;
using Mug.Service.Interface;
using Mug.Dao;
using Mug.Service.UI;
using Mug.Service.Job;

namespace Mug_Front.Controllers
{
    public class ContactController : Controller
    {
        private IContactService ContactService = new ContactService();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(FormCollection form,string Opt)
        {
            var Cont = ContactService.GetAll().ToList();
            var result = (from r in Cont
                         select new Contact
                          {
                              Id = r.Id,
                          }).OrderByDescending(x => x.Id); ;
            int MaxId = 1;
            if (result.Count() != 0)
            {
                MaxId = int.Parse(result.First().Id.ToString());
                MaxId = MaxId + 1;
            }
            Contact _Contact = new Contact();
            _Contact.Id = MaxId;
            _Contact.Name = form["Name"];
            _Contact.Phone = form["Tel"];
            _Contact.Service = form["Service"];
            _Contact.Email = form["Email"];
            _Contact.Memo = form["Memo"];
            _Contact.CreateTime = DateTime.Now;
            var Message = ContactService.Create(_Contact);
            EmailClient email = new EmailClient();
            email.sendEmail(_Contact);

            if (Message.Success == true)
            {
                return Json(new { Status = "0", Message = "成功" });
            }
            return Json(new { Status = "0", Message = "成功" });
        }
    }
}