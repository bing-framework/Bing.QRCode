using System;
using System.IO;
using Bing.QRCode.Abstractions;
using Bing.QRCode.Core;
using Bing.QRCode.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using ZXing;
using ZXing.ImageSharp.Rendering;
using ZXing.QrCode;
using ZQI = global::ZXing.QrCode.Internal;

namespace Bing.QRCode.ZXing;

/// <summary>
/// ZXing.Net二维码服务
/// </summary>
// ReSharper disable once InconsistentNaming
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
        using var bitmap = GetBitmap(param);
        if (!string.IsNullOrWhiteSpace(param.Logo)) 
            ImageUtil.MergeImage(bitmap, Image.Load(param.Logo));
        using var ms = new MemoryStream();
        bitmap.SaveAsPng(ms);
        return ms.ToArray();
    }

    /// <summary>
    /// 获取二维码图片
    /// </summary>
    /// <param name="param">二维码参数</param>
    private Image GetBitmap(QRCodeParam param)
    {
        var bitmapBarcodeWriter = new BarcodeWriter<Image<Rgba32>>
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                CharacterSet = "UTF-8",
                ErrorCorrection = _level,
                Margin = param.DrawBorder ? 2 : 0,
                Width = param.Size,
                Height = param.Size,
            },
            Renderer = new ImageSharpRenderer<Rgba32>
            {
                Foreground = param.Foreground.ToImageSharpColor(),
                Background = param.Background.ToImageSharpColor(),
            }
        };
        return bitmapBarcodeWriter.Write(param.Content);
    }
}
