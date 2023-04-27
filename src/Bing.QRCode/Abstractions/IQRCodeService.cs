using System.IO;
using Bing.QRCode.Core;
using Bing.QRCode.Enums;

namespace Bing.QRCode.Abstractions;

/// <summary>
/// 二维码服务
/// </summary>
// ReSharper disable once InconsistentNaming
public interface IQRCodeService
{
    /// <summary>
    /// 设置二维码参数
    /// </summary>
    /// <param name="param">二维码参数</param>
    IQRCodeService Param(QRCodeParam param);

    /// <summary>
    /// 转换成流
    /// </summary>
    Stream ToStream();

    /// <summary>
    /// 转换成字节数组
    /// </summary>
    byte[] ToBytes();

    /// <summary>
    /// 转换成Base64字符串
    /// </summary>
    string ToBase64String();

    /// <summary>
    /// 转换成Base64字符串，并附带前缀
    /// </summary>
    /// <param name="type">图片类型</param>
    string ToBase64String(Base64ImageType type);

    /// <summary>
    /// 写入到文件
    /// </summary>
    /// <param name="path">文件路径</param>
    string WriteToFile(string path);
}