# Bing.QRCode
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://mit-license.org/)

Bing.QRCode是Bing应用框架的二维码操作核心库。
主要用于二维码相关功能操作。

## Nuget Packages
|Nuget|版本号|说明|
|---|---|---|
|Bing.QRCode|[![NuGet Badge](https://buildstats.info/nuget/Bing.QRCode?includePreReleases=true)](https://www.nuget.org/packages/Bing.Tools.QrCode)|
|Bing.QRCode.QRCoder|[![NuGet Badge](https://buildstats.info/nuget/Bing.QRCode.QRCoder?includePreReleases=true)](https://www.nuget.org/packages/Bing.Tools.QrCode.QRCoder)|
|Bing.QRCode.ZXing|[![NuGet Badge](https://buildstats.info/nuget/Bing.QRCode.ZXing?includePreReleases=true)](https://www.nuget.org/packages/Bing.Tools.QrCode.ZXing)|

## 实现功能
- 简单二维码生成
- 带Logo二维码生成
- 自定义颜色二维码生成

## 依赖类库
- System.Drawing.Common
- [QRCoder](https://github.com/codebude/QRCoder)
- [ZXing.Net](https://github.com/micjahn/ZXing.Net)
- [ZXing.Net.Bindings.ZKWeb.System.Drawing](https://github.com/micjahn/ZXing.Net)

## 作者

简玄冰

## 贡献与反馈

> 如果你在阅读或使用Bing中任意一个代码片断时发现Bug，或有更佳实现方式，请通知我们。

> 为了保持代码简单，目前很多功能只建立了基本结构，细节特性未进行迁移，在后续需要时进行添加，如果你发现某个类无法满足你的需求，请通知我们。

> 你可以通过github的Issue或Pull Request向我们提交问题和代码，如果你更喜欢使用QQ进行交流，请加入我们的交流QQ群。

> 对于你提交的代码，如果我们决定采纳，可能会进行相应重构，以统一代码风格。

> 对于热心的同学，将会把你的名字放到**贡献者**名单中。

## 免责声明
- 虽然我们对代码已经进行高度审查，并用于自己的项目中，但依然可能存在某些未知的BUG，如果你的生产系统蒙受损失，Bing 团队不会对此负责。
- 
- 出于成本的考虑，我们不会对已发布的API保持兼容，每当更新代码时，请注意该问题。

## 开源地址
[https://github.com/bing-framework/Bing.QRCode](https://github.com/bing-framework/Bing.QRCode)

## License

**MIT**

> 这意味着你可以在任意场景下使用 Bing 应用框架而不会有人找你要钱。

> Bing 会尽量引入开源免费的第三方技术框架，如有意外，还请自行了解。


## Demo
### 简单二维码
```
IQRCoderService service = new QRCoderQRCodeService();
service.Param(new QRCodeParam()
{
    Content = "Test Name is Bing.QRCode.QRCoder.Tests",
    Level = ErrorCorrectionLevel.Q,
    Size = 100    
});
service.ToBase64String();
```

### 带Logo二维码
```
IQRCoderService service = new QRCoderQRCodeService();
service.Param(new QRCodeParam()
{
    Content = "Test Name is Bing.QRCode.QRCoder.Tests",
    Level = ErrorCorrectionLevel.Q,
    Size = 100,
    Logo = $"{Directory.GetCurrentDirectory()}\\logo.jpg"
});
service.ToBase64String();
```

## 接口说明
### 常用参数
- Size：尺寸
- Level：容错级别
- Logo：Logo图片路径
- Content：二维码内容
- Foreground：前景色
- Background：背景色

### 接口方法
- `Param(QRCodeParam param)`：设置二维码参数
- `ToStream()`：输出流
- `ToBytes()`：输出字节数组
- `ToBase64String()`：输出Base64字符串
- `ToBase64String(Base64ImageType type)`：输出Base64字符串，并设置前缀
- `WriteToFile(string path)`：输出到文件

### 容错级别(`ErrorCorrectionLevel`)
- L：默认，可以纠正最大7%的错误
- M：可以纠正最大15%的错误
- Q：可以纠正最大25%的错误
- H：可以纠正最大30%的错误