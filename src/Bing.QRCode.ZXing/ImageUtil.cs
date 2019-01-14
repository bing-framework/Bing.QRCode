using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.DrawingCore.Imaging;
using System.IO;

namespace Bing.QRCode.ZXing
{
    /// <summary>
    /// 图片操作
    /// </summary>
    internal static class ImageUtil
    {
        /// <summary>
        /// 图片缩放
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="n">缩放比例</param>
        /// <returns></returns>
        public static Image Zoom(Image image, double n)
        {
            // 计算缩略图宽高
            var newWidth = n * image.Width;
            var newHeight = n * image.Height;
            // 生成新图
            Image newImage = new Bitmap((int) newWidth, (int) newHeight);
            // 新建一个画板
            Graphics g = Graphics.FromImage(newImage);
            // 设置质量
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            // 设置背景色
            g.Clear(Color.Transparent);
            // 画图
            g.DrawImage(image, new Rectangle(0, 0, newImage.Width, newImage.Height),
                new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            g.Dispose();
            return newImage;
        }

        /// <summary>
        /// 合并图片
        /// 参考：http://www.cnblogs.com/zoro-zero/p/6225697.html
        /// </summary>
        /// <param name="qrImage">二维码图片</param>
        /// <param name="logoImage">Logo图片</param>
        /// <param name="n">缩放比例</param>
        /// <returns></returns>
        public static byte[] MergeImage(Bitmap qrImage, Bitmap logoImage, double n = 0.23)
        {
            int margin = 10;
            float dpix = qrImage.HorizontalResolution;
            float dpiy = qrImage.VerticalResolution;

            var newWidth = (10 * qrImage.Width - 46 * margin) * 1.0f / 46;
            var newLogoImg = Zoom(logoImage, newWidth / logoImage.Width);
            // 处理Logo
            int newImgWidth = newLogoImg.Width + margin;
            Bitmap logoBgImg = new Bitmap(newImgWidth, newImgWidth);
            logoBgImg.MakeTransparent();
            Graphics g = Graphics.FromImage(logoBgImg);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.Clear(Color.Transparent);
            Pen p = new Pen(new SolidBrush(Color.White));
            Rectangle rect = new Rectangle(0, 0, newImgWidth - 1, newImgWidth - 1);
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, 7))
            {
                g.DrawPath(p, path);
                g.FillPath(new SolidBrush(Color.White), path);
            }
            // 画Logo
            Bitmap img1 = new Bitmap(newLogoImg.Width, newLogoImg.Height);
            Graphics g1 = Graphics.FromImage(img1);
            g1.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g1.SmoothingMode = SmoothingMode.HighQuality;
            g1.Clear(Color.Transparent);
            Pen p1 = new Pen(new SolidBrush(Color.Gray));
            Rectangle rect1 = new Rectangle(0, 0, newLogoImg.Width - 1, newLogoImg.Height - 1);
            using (GraphicsPath path1 = CreateRoundedRectanglePath(rect1, 7))
            {
                g1.DrawPath(p1, path1);
                TextureBrush brush = new TextureBrush(newLogoImg);
                g1.FillPath(brush, path1);
            }
            g1.Dispose();

            PointF center = new PointF((newImgWidth - newLogoImg.Width) / 2, (newImgWidth - newLogoImg.Height) / 2);
            g.DrawImage(img1, center.X, center.Y, newLogoImg.Width, newLogoImg.Height);
            g.Dispose();

            Bitmap backgroundImg = new Bitmap(qrImage.Width, qrImage.Height);
            backgroundImg.MakeTransparent();
            backgroundImg.SetResolution(dpix, dpiy);
            logoBgImg.SetResolution(dpix, dpiy);

            Graphics g2 = Graphics.FromImage(backgroundImg);
            g2.Clear(Color.Transparent);
            g2.DrawImage(qrImage, 0, 0);
            PointF center2 = new PointF((qrImage.Width - logoBgImg.Width) / 2, (qrImage.Height - logoBgImg.Height) / 2);
            g2.DrawImage(logoBgImg, center2);
            g2.Dispose();

            using (var ms = new MemoryStream())
            {
                backgroundImg.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 创建圆角矩形
        /// </summary>
        /// <param name="rect">区域</param>
        /// <param name="cornerRadius">角度</param>
        /// <returns></returns>
        private static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270,
                90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right,
                rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2,
                cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);

            roundedRect.CloseFigure();

            return roundedRect;
        }
    }
}
