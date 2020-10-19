using System;
using System.Drawing.Imaging;
using System.IO;
using Bing.QRCode.Abstractions;
using Bing.QRCode.Core;
using Bing.QRCode.Enums;
using QRCoder;
using QC= global::QRCoder;

namespace Bing.QRCode.QRCoder
{
    /// <summary>
    /// QRCoder二维码服务
    /// </summary>
    public class QRCoderQRCodeService : QRCodeServiceBase, IQRCodeService
    {
        /// <summary>
        /// QRCoder容错级别
        /// </summary>
        private QRCodeGenerator.ECCLevel _level;

        /// <summary>
        /// 二维码生成器
        /// </summary>
        private readonly QRCodeGenerator _generator;

        /// <summary>
        /// 初始化一个<see cref="QRCoderQRCodeService"/>类型的实例
        /// </summary>
        public QRCoderQRCodeService()
        {
            _level = QRCodeGenerator.ECCLevel.L;
            _generator = new QRCodeGenerator();
        }

        /// <summary>
        /// 处理容错级别
        /// </summary>
        /// <param name="level">容错级别</param>
        protected override void HandlerCorrectionLevel(ErrorCorrectionLevel level)
        {
            switch (level)
            {
                case ErrorCorrectionLevel.L:
                    _level = QRCodeGenerator.ECCLevel.L;
                    break;
                case ErrorCorrectionLevel.M:
                    _level = QRCodeGenerator.ECCLevel.M;
                    break;
                case ErrorCorrectionLevel.Q:
                    _level = QRCodeGenerator.ECCLevel.Q;
                    break;
                case ErrorCorrectionLevel.H:
                    _level = QRCodeGenerator.ECCLevel.H;
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
            QRCodeData data = _generator.CreateQrCode(param.Content, _level);
            QC.QRCode qrcode = new QC.QRCode(data);
            using (var bitmap = qrcode.GetGraphic(param.Size, param.Foreground, param.Background, GetLogo(),iconBorderWidth:20))
            {
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
    }
}
