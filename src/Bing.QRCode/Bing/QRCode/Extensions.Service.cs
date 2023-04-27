using Bing.QRCode.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.QRCode;

public static partial class Extensions
{
    /// <summary>
    /// 注册二维码服务
    /// </summary>
    /// <typeparam name="TService">二维码服务</typeparam>
    /// <param name="services">服务集合</param>
    // ReSharper disable once InconsistentNaming
    public static void AddQRCodeService<TService>(this IServiceCollection services) where TService : class, IQRCodeService => services.AddScoped<IQRCodeService, TService>();
}