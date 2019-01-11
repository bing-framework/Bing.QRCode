using System.Drawing;
using System.IO;
using Bing.QRCode.Enums;

namespace Bing.QRCode.Abstractions
{
    /// <summary>
    /// 二维码服务
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface IQRCodeService
    {
        /// <summary>
        /// 设置二维码尺寸
        /// </summary>
        /// <param name="size">二维码尺寸</param>
        /// <returns></returns>
        IQRCodeService Size(int size);

        /// <summary>
        /// 设置容错级别
        /// </summary>
        /// <param name="level">容错级别</param>
        /// <returns></returns>
        IQRCodeService Correction(ErrorCorrectionLevel level);

        /// <summary>
        /// 设置Logo
        /// </summary>
        /// <param name="logoPath">Logo文件路径</param>
        /// <returns></returns>
        IQRCodeService Logo(string logoPath);

        /// <summary>
        /// 设置前景色
        /// </summary>
        /// <param name="color">前景色</param>
        /// <returns></returns>
        IQRCodeService Foreground(Color color);

        /// <summary>
        /// 设置背景色
        /// </summary>
        /// <param name="color">背景色</param>
        /// <returns></returns>
        IQRCodeService Background(Color color);

        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        IQRCodeService Content(string content);

        /// <summary>
        /// 转换成流
        /// </summary>
        /// <returns></returns>
        Stream ToStream();

        /// <summary>
        /// 转换成字节数组
        /// </summary>
        /// <returns></returns>
        byte[] ToBytes();

        /// <summary>
        /// 转换成Base64字符串
        /// </summary>
        /// <returns></returns>
        string ToBase64String();
    }
}
