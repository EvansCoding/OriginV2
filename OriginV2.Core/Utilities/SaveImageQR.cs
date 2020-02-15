using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;
namespace OriginV2.Core.Utilities
{
    public class SaveImageQR
    {
        private static SaveImageQR instance;

        public static SaveImageQR Instance
        {
            get
            {
                if (instance == null) return instance = new SaveImageQR();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        public Image createQRCode(string code, string name, HttpServerUtilityBase Server)
        {
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            EncodingOptions encodingOptions = new EncodingOptions() { Width = 600, Height = 600, Margin = 1, PureBarcode = false };
            encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            barcodeWriter.Renderer = new BitmapRenderer();
            barcodeWriter.Options = encodingOptions;
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            Bitmap bitmap = barcodeWriter.Write(code);
            return uploadImage(bitmap, name, Server);
        }

        public Image uploadImage(Bitmap file, string name, HttpServerUtilityBase Server)
        {
            string path = "~/Media/Uploads";
            string repath = "~/Media/Uploads";
            bool exist = System.IO.Directory.Exists(Server.MapPath(path));
            if (!exist) System.IO.Directory.CreateDirectory(Server.MapPath(path));

            path += "/QRCode";
            repath += "/QRCode";
            exist = System.IO.Directory.Exists(Server.MapPath(path));
            if (!exist) System.IO.Directory.CreateDirectory(Server.MapPath(path));

            var fileName = name;
            var pt = Path.Combine(path, fileName + ".png");
            file.Save(Server.MapPath(pt), ImageFormat.Png);
            return file;
        }
    }
}
