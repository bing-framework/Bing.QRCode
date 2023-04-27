using System.Drawing;
using Bing.QRCode.Enums;

namespace Bing.QRCode.Core;

/// <summary>
/// 二维码参数
/// </summary>
// ReSharper disable once InconsistentNaming
public class QRCodeParam
{
    /// <summary>
    /// 尺寸
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// 容错级别
    /// </summary>
    public ErrorCorrectionLevel Level { get; set; }

    /// <summary>
    /// 前景色
    /// </summary>
    public Color Foreground { get; set; }

    /// <summary>
    /// 背景色
    /// </summary>
    public Color Background { get; set; }

    /// <summary>
    /// LOGO图片路径
    /// </summary>
    public string Logo { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 绘制边框
    /// </summary>
    public bool DrawBorder { get; set; }

    /// <summary>
    /// 初始化一个<see cref="QRCodeParam"/>类型的实例
    /// </summary>
    public QRCodeParam()
    {
        Size = 10;
        Level = ErrorCorrectionLevel.L;
        Logo = string.Empty;
        Foreground = Color.Black;
        Background = Color.White;
        Content = string.Empty;
        DrawBorder = false;
    }
}
