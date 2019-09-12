using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QRCoder;
using System.IO;
using System.Drawing;

namespace MVC_QR.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult QR()
        {
            return View();
        }
        [HttpPost]
        public ActionResult QR(string txtQRCode)
        {
            ViewBag.txtQRCode = txtQRCode;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(txtQRCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Height = 15;
            imgBarCode.Width = 15;
            using (Bitmap bitMap = qrCode.GetGraphic(15))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ViewBag.imageBytes = ms.ToArray();
                }
            }
            return View();
        }
    }
}