using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mug.HtmlHelper
{
    public class ImageHelper
    {
        public byte[] FileToByte(HttpPostedFileBase file)
        {
            byte[] bytes = null;
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                bytes = memoryStream.ToArray();
                return bytes;
            }
            return bytes;
        }
        /// 將 Byte 陣列轉換為 Image。
        /// </summary>
        /// <param name="Buffer">Byte 陣列。</param>        
        public static Image BufferToImage(byte[] Buffer)
        {
            if (Buffer == null || Buffer.Length == 0) { return null; }
            byte[] data = null;
            Image oImage = null;
            Bitmap oBitmap = null;
            //建立副本
            data = (byte[])Buffer.Clone();
            try
            {
                MemoryStream oMemoryStream = new MemoryStream(Buffer);
                //設定資料流位置
                oMemoryStream.Position = 0;
                oImage = System.Drawing.Image.FromStream(oMemoryStream);
                //建立副本
                oBitmap = new Bitmap(oImage);
            }
            catch
            {
                throw;
            }
            //return oImage;
            return oBitmap;
        }

        public  string GetGuid()
        {
            string guid = System.Guid.NewGuid().ToString("N");
            byte[] bt = System.Text.UnicodeEncoding.Unicode.GetBytes(guid);//產生64 byte
            string generateGuid = System.Text.UnicodeEncoding.Unicode.GetString(bt);
            return generateGuid;

        }
    }
}
