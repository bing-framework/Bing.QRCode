using System.Drawing;
using System.IO;
using Bing.QRCode.Abstractions;
using Bing.QRCode.Core;
using Bing.QRCode.Enums;
using Xunit;
using Xunit.Abstractions;

namespace Bing.QRCode.ZXing.Tests
{
    public class ZXingQRCodeServiceTest : TestBase
    {
        private IQRCodeService _service;

        private const string Content = "Test Name is Bing.QRCode.ZXing.Tests";

        private string _logo = $"{Directory.GetCurrentDirectory()}\\logo.jpg";

        private const string OutputPath = "D:\\qrcode_zxing.png";

        public ZXingQRCodeServiceTest(ITestOutputHelper output) : base(output)
        {
            _service = new ZXingQRCodeService();
        }

        /// <summary>
        /// 测试输出基础二维码Base64字符串
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
        /// 测试输出基础二维码Base64字符串并带前缀
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
        /// 测试输出带Logo二维码Base64字符串
        /// </summary>
        [Fact]
        public void Test_Output_LogoCode_Base64()
        {
            _service.Param(new QRCodeParam
            {
                Content = Content,
                Level = ErrorCorrectionLevel.Q,
                Size = 100,
                Logo = _logo
            });
            var result = _service.ToBase64String();
            Output.WriteLine(result);
        }

        /// <summary>
        /// 测试输出带Logo二维码Base64字符串并带前缀
        /// </summary>
        [Fact]
        public void Test_Output_LogoCode_Base64_Prefix()
        {
            _service.Param(new QRCodeParam
            {
                Content = Content,
                Level = ErrorCorrectionLevel.Q,
                Size = 100,
                Logo = _logo
            });
            var result = _service.ToBase64String(Base64ImageType.Jpeg);
            Output.WriteLine(result);
        }

        /// <summary>
        /// 测试输出基础二维码文件
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
            var result = _service.WriteToFile(OutputPath);
            Output.WriteLine(result);
        }

        /// <summary>
        /// 测试输出带Logo二维码文件
        /// </summary>
        [Fact]
        public void Test_Output_LogoCode_File()
        {
            _service.Param(new QRCodeParam
            {
                Content = Content,
                Level = ErrorCorrectionLevel.Q,
                Size = 1000,
                Logo = _logo,
                Background = Color.Black,
                Foreground = Color.Yellow
            });
            var result = _service.WriteToFile(OutputPath);
            Output.WriteLine(result);
        }
    }
}
