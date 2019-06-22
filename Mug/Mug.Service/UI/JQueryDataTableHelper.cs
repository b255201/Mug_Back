using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mug.Service.UI
{
    public class JQueryDataTableHelper<T>
    {
        /// <summary>
        /// 取得前端 JQueryDataTable UI 需求物件
        /// </summary>
        /// <param name="request">HttpRequest 物件</param>
        /// <returns>JQueryDataTable 物件</returns>
        public static JQueryDataTableRequest GetRequest(HttpRequestBase request)
        {
            #region jquery datatable 會 POST 下列資料

            #endregion

            int draw = int.Parse(request.Params["draw"]);
            int start = int.Parse(request.Params["start"]);
            int length = int.Parse(request.Params["length"]);

            var result = from c in request.Params.AllKeys
                         where c.StartsWith("columns[") && c.EndsWith("][data]")
                         select request.Params[c];


            List<string> columns = result.ToList();

            string orderBy = columns[int.Parse(request.Params["order[0][column]"])];
            string orderDir = request.Params["order[0][dir]"];
            //string searchBy = request.Params["search[value]"];
            //string searchReg = request.Params["search[regex]"];

            return new JQueryDataTableRequest() { draw = draw, start = start, length = length, orderBy = orderBy, orderDir = orderDir };
        }

        /// <summary>
        /// 取得前端 JQueryDataTable UI 回應物件
        /// </summary>
        /// <param name="draw"></param>
        /// <param name="recordsTotal">總筆數</param>
        /// <param name="recordsFiltered">篩選筆數</param>
        /// <param name="data">泛型資料</param>
        /// <returns></returns>
        public static JQueryDataTableResponse<T> GetResponse(int draw, int recordsTotal, int recordsFiltered, List<T> data)
        {
            return new JQueryDataTableResponse<T>() { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsFiltered, data = data };
        }
    }

    /// <summary>
    /// 前端 JQueryDataTable UI 需求物件
    /// </summary>
    public class JQueryDataTableRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// 開始筆數
        /// </summary>
        public int start { get; set; }

        /// <summary>
        /// 長度
        /// </summary>
        public int length { get; set; }

        /// <summary>
        /// 排序欄位ID
        /// </summary>
        public string orderBy { get; set; }

        /// <summary>
        /// 升降冪
        /// </summary>
        public string orderDir { get; set; }
    }

    /// <summary>
    /// 前端 JQueryDataTable UI 回應物件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JQueryDataTableResponse<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// 總筆數
        /// </summary>
        public int recordsTotal { get; set; }

        /// <summary>
        /// 篩選筆數
        /// </summary>
        public int recordsFiltered { get; set; }

        /// <summary>
        /// 泛型資料
        /// </summary>
        public List<T> data { get; set; }
    }
}