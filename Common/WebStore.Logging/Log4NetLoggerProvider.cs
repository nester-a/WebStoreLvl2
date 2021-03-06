using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Xml;

namespace WebStore.Logging;

public class Log4NetLoggerProvider : ILoggerProvider
{
    private readonly string configurationFile;
    private readonly ConcurrentDictionary<string, Log4NetLogger> loggers = new();

    public Log4NetLoggerProvider(string ConfigurationFile)
    {
        configurationFile = ConfigurationFile;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return loggers.GetOrAdd(categoryName, static (category, config) =>
        {
            var xml = new XmlDocument();
            xml.Load(config);
            return new Log4NetLogger(category, xml["log4net"]!);
        }, configurationFile);
    }

    //public ILogger CreateLogger(string Category) =>
    //    loggers.GetOrAdd(Category, static (category, configuration_file) =>
    //    {
    //        var xml = new XmlDocument();
    //        xml.Load(configuration_file);
    //        return new Log4NetLogger(category, xml["log4net"]!);
    //    }, configurationFile);

    public void Dispose()
    {
        loggers.Clear();
    }
}
