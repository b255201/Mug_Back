using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mug.Models
{
    using System;
    using System.Collections.Generic;

    public partial class ArticleViewModel
    {
        public int Blog_id { get; set; }
        public string Image { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<bool> Enable { get; set; }
        public string Categore { get; set; }
        public string Category_Opt { get; set; }
        public int Post_Id { get; set; }
        public string Title { get; set; }
        public string Sub_Title { get; set; }
        public string Contents { get; set; }
        public int Id { get; set; }
    }
}