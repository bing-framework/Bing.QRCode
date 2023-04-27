using System;
using System.Drawing;
using Bing.QRCode.Abstractions;
using Bing.QRCode.Core;
using Bing.QRCode.Enums;
using Xunit;
using Xunit.Abstractions;

namespace Bing.QRCode.ZXing.Tests
{
    /// <summary>
    /// 基于ZXing.NET实现的二维码服务 测试
    /// </summary>
    public class ZXingQRCodeServiceTest : TestBase
    {
        /// <summary>
        /// 二维码服务
        /// </summary>
        private IQRCodeService _service;

        /// <summary>
        /// 内容
        /// </summary>
        private const string Content = "Test Name is Bing.QRCode.ZXing.Tests";

        /// <summary>
        /// 初始化
        /// </summary>
        public ZXingQRCodeServiceTest(ITestOutputHelper output) : base(output)
        {
            _service = new ZXingQRCodeService();
        }

        /// <summary>
        /// 测试 - 输出基础二维码Base64字符串
        /// </summary>
        [Fact]
        public void Test_Output_BaseCode_Base64()
        {
            _service.Param(new QRCodeParam
            {
                Content = Content,
                Level = ErrorCorrectionLevel.Q,
                Size = 100
            });
            var result = _service.ToBase64String();
            Output.WriteLine(result);
        }

        /// <summary>
        /// 测试 - 输出基础二维码Base64字符串并带前缀
        /// </summary>
        [Fact]
        public void Test_Output_BaseCode_Base64_Prefix()
        {
            _service.Param(new QRCodeParam
            {
                Content = Content,
                Level = ErrorCorrectionLevel.Q,
                Size = 100
            });
            var result = _service.ToBase64String(Base64ImageType.Jpeg);
            Output.WriteLine(result);
        }

        /// <summary>
        /// 测试 - 输出带Logo二维码Base64字符串
        /// </summary>
        [Fact]
        public void Test_Output_LogoCode_Base64()
        {
            _service.Param(new QRCodeParam
            {
                Content = Content,
                Level = ErrorCorrectionLevel.Q,
                Size = 100,
                Logo = GetTestFilePath("logo.jpg")
            });
            var result = _service.ToBase64String();
            Output.WriteLine(result);
        }

        /// <summary>
        /// 测试 - 输出带Logo二维码Base64字符串并带前缀
        /// </summary>
        [Fact]
        public void Test_Output_LogoCode_Base64_Prefix()
        {
            _service.Param(new QRCodeParam
            {
                Content = Content,
                Level = ErrorCorrectionLevel.Q,
                Size = 100,
                Logo = GetTestFilePath("logo.jpg")
            });
            var result = _service.ToBase64String(Base64ImageType.Jpeg);
            Output.WriteLine(result);
        }

        /// <summary>
        /// 测试 - 输出基础二维码文件
        /// </summary>
        [Fact]
        public void Test_Output_BaseCode_File()
        {
            _service.Param(new QRCodeParam
            {
                Content = Content,
                Level = ErrorCorrectionLevel.Q,
                Size = 100
            });
            var result = _service.WriteToFile($"D:\\zxing_qrcode_{DateTime.Now:yyyyMMddHHmmss}.png");
            Output.WriteLine(result);
        }

        /// <summary>
        /// 测试 - 输出带Logo二维码文件
        /// </summary>
        [Fact]
        public void Test_Output_LogoCode_File()
        {
            _service.Param(new QRCodeParam
            {
                Content = Content,
                Level = ErrorCorrectionLevel.Q,
                Size = 1000,
                Logo = GetTestFilePath("logo.jpg"),
                Background = Color.Black,
                Foreground = Color.Yellow
            });
            var result = _service.WriteToFile($"D:\\zxing_qrcode_logo_{DateTime.Now:yyyyMMddHHmmss}.png");
            Output.WriteLine(result);
        }
    }
}
