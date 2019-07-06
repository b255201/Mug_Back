using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mug_Front.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            var a = new customer
            {
                name = "aaa",
                dat=new List<test>()
                    {
                      new test
                      {
                          Id="aa",
                          Phone="bb"
                      }
                    }
                ,
                add="jiojijio"
            };
            var js = JsonConvert.SerializeObject(a);

            return View();
        }
    }

    public class customer
    {
      //  public string[] data { get; set; }
        public string name { get; set; }
        public List<test> dat{ get; set; }
        public string add { get; set; }
        }
    public class test
    {
        public string Id { get; set; }
        public string Phone { get; set; }
    }
}