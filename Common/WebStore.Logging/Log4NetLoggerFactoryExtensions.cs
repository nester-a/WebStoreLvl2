using Microsoft.Extensions.Logging;
using System.Reflection;

namespace WebStore.Logging;

public static class Log4NetLoggerFactoryExtensions
{
    private static string CheckFilePath(string filePath)
    {
        if(filePath is not { Length: > 0 })
            throw new ArgumentException("Не указан путь к файлу конфигурации", nameof(filePath));

        if (Path.IsPathRooted(filePath)) 
            return filePath;

        var assembly = Assembly.GetEntryAssembly();
        var dir = Path.GetDirectoryName(assembly!.Location);
        return Path.Combine(dir, filePath);
    }
    public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder, string ConfigurationFile = "log4net.config")
    {
        builder.AddProvider(new Log4NetLoggerProvider(CheckFilePath(ConfigurationFile)));
        return builder;
    }
}
