using System.Drawing;
using System.IO;
using Bing.QRCode.Abstractions;
using Bing.QRCode.Enums;

namespace Bing.QRCode.Core
{
    /// <summary>
    /// 二维码服务基类
    /// </summary>
    public abstract class QRCodeServiceBase : IQRCodeService
    {
        public abstract IQRCodeService Size(int size);
        public abstract IQRCodeService Correction(ErrorCorrectionLevel level);
        public abstract IQRCodeService Logo(string logoPath);
        public abstract IQRCodeService Foreground(Color color);
        public abstract IQRCodeService Background(Color color);
        public abstract IQRCodeService Content(string content);
        public abstract Stream ToStream();
        public abstract byte[] ToBytes();
        public abstract string ToBase64String();
    }
}
