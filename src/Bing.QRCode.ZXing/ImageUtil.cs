using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Bing.QRCode.ZXing;

/// <summary>
/// 图片操作
/// </summary>
internal static class ImageUtil
{
    /// <summary>
    /// 将 <see cref="System.Drawing.Color"/> 转换为 <see cref="SixLabors.ImageSharp.Color"/>
    /// </summary>
    /// <param name="color">颜色</param>
    public static SixLabors.ImageSharp.Color ToImageSharpColor(this System.Drawing.Color color)
    {
        return SixLabors.ImageSharp.Color.FromRgba(color.R, color.G, color.B, color.A);
    }

    /// <summary>
    /// 图片缩放
    /// </summary>
    /// <param name="image">图片</param>
    /// <param name="n">缩放比例</param>
    internal static Image Zoom(this Image image, double n)
    {
        // 计算缩略图宽高
        var newWidth = (int)(n * image.Width);
        var newHeight = (int)(n * image.Height);
        image.Mutate(x =>
        {
            x.Resize(new Size(newWidth, newHeight));
        });
        return image;
    }

    /// <summary>
    /// 合并图片
    /// </summary>
    /// <param name="qrImage">二维码图片</param>
    /// <param name="logoImage">Logo图片</param>
    /// <param name="n">缩放比例</param>
    /// <returns></returns>
    public static Image MergeImage(Image qrImage, Image logoImage, double n = 0.23)
    {
        int margin = 10;

        var newWidth = (10 * qrImage.Width - 46 * margin) * 1.0f / 46;
        logoImage.Zoom(newWidth / logoImage.Width);
        // 设置圆角
        logoImage.RoundCorners(7);
        qrImage.RoundCorners(7);
        // 合并图片
        qrImage.Mutate(x =>
        {
            x.DrawImage(logoImage, new Point((qrImage.Width - logoImage.Width) / 2, (qrImage.Height - logoImage.Height) / 2), 1);
        });
        return qrImage;
    }

    /// <summary>
    /// 设置圆角
    /// </summary>
    /// <param name="image">图片</param>
    /// <param name="cornerRadius">角度</param>
    internal static Image RoundCorners(this Image image, int cornerRadius)
    {
        image.Mutate(x => x.RoundCorners(cornerRadius));
        return image;
    }

    /// <summary>
    /// 设置圆角处理
    /// </summary>
    /// <param name="context">图片处理上下文</param>
    /// <param name="cornerRadius">角度</param>
    internal static IImageProcessingContext RoundCorners(this IImageProcessingContext context, int cornerRadius)
    {
        var size = context.GetCurrentSize();
        var corners = BuildCorners(size.Width, size.Height, cornerRadius);
        context.SetGraphicsOptions(new GraphicsOptions() { AlphaCompositionMode = PixelAlphaCompositionMode.DestOut });
        return context.Fill(SixLabors.ImageSharp.Color.Red, corners);
    }

    /// <summary>
    /// 构建圆角
    /// </summary>
    /// <param name="imageWidth">图片宽度</param>
    /// <param name="imageHeight">图片高度</param>
    /// <param name="cornerRadius">角度</param>
    private static IPathCollection BuildCorners(int imageWidth, int imageHeight, float cornerRadius)
    {
        var rect = new RectangularPolygon(-0.5f, -0.5f, cornerRadius, cornerRadius);

        var cornerTopLeft = rect.Clip(new EllipsePolygon(cornerRadius - 0.5f, cornerRadius - 0.5f, cornerRadius));

        var rightPos = imageWidth - cornerTopLeft.Bounds.Width + 1;
        var bottomPos = imageHeight - cornerTopLeft.Bounds.Height + 1;

        var cornerTopRight = cornerTopLeft.RotateDegree(90).Translate(rightPos, 0);
        var cornerBottomLeft = cornerTopLeft.RotateDegree(-90).Translate(0, bottomPos);
        var cornerBottomRight = cornerTopLeft.RotateDegree(180).Translate(rightPos, bottomPos);

        return new PathCollection(cornerTopLeft, cornerBottomLeft, cornerTopRight, cornerBottomRight);
    }
}
