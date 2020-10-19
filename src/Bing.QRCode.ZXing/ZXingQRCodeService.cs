using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using Bing.QRCode.Abstractions;
using Bing.QRCode.Core;
using Bing.QRCode.Enums;
using ZXing;
using ZXing.QrCode;
using ZXing.ZKWeb.Rendering;
using ZQI = global::ZXing.QrCode.Internal;

namespace Bing.QRCode.ZXing
{
    /// <summary>
    /// ZXing.Net二维码服务
    /// </summary>
    public class ZXingQRCodeService : QRCodeServiceBase, IQRCodeService
    {
        /// <summary>
        /// ZXing.Net容错级别
        /// </summary>
        private ZQI.ErrorCorrectionLevel _level;

        /// <summary>
        /// 处理容错级别
        /// </summary>
        /// <param name="level">容错级别</param>
        protected override void HandlerCorrectionLevel(ErrorCorrectionLevel level)
        {
            switch (level)
            {
                case ErrorCorrectionLevel.L:
                    _level = ZQI.ErrorCorrectionLevel.L;
                    break;
                case ErrorCorrectionLevel.M:
                    _level = ZQI.ErrorCorrectionLevel.M;
                    break;
                case ErrorCorrectionLevel.Q:
                    _level = ZQI.ErrorCorrectionLevel.Q;
                    break;
                case ErrorCorrectionLevel.H:
                    _level = ZQI.ErrorCorrectionLevel.H;
                    break;
                default:
                    throw new NotImplementedException("未知容错级别");
            }
        }

        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="param">二维码参数</param>
        protected override byte[] Create(QRCodeParam param)
        {
            using (var bitmap=GetBitmap(param))
            {
                if (string.IsNullOrWhiteSpace(param.Logo))
                {
                    using (var ms=new MemoryStream())
                    {
                        bitmap.Save(ms,ImageFormat.Png);
                        return ms.ToArray();
                    }
                }

                return ImageUtil.MergeImage(bitmap, new Bitmap(param.Logo));
            }
        }

        /// <summary>
        /// 获取二维码图片
        /// </summary>
        /// <param name="param">二维码参数</param>
        private Bitmap GetBitmap(QRCodeParam param)
        {
            BarcodeWriter<Bitmap> bitmapBarcodeWriter = new BarcodeWriter<Bitmap>()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    CharacterSet = "UTF-8",
                    ErrorCorrection = _level,
                    Margin = 2,
                    Width = param.Size,
                    Height = param.Size,
                },
                Renderer = new BitmapRenderer
                {
                    Foreground = Color.FromName(param.Foreground.Name),
                    Background = Color.FromName(param.Background.Name)
                }
            };

            return bitmapBarcodeWriter.Write(param.Content);
        }
    }
}
