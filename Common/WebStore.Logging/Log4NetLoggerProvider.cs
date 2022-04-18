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

    public void Dispose()
    {
        var oldLoggers = loggers.Values.ToArray();
        loggers.Clear();
        foreach (var log in oldLoggers)
        {
            log.Dispose();
        }
    }
}
