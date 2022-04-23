using log4net;
using log4net.Config;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Reflection;
using System.Xml;

namespace WebStore.Logging;

public class Log4NetLogger : ILogger
{
    private readonly ILog _log;

    public Log4NetLogger(string category, XmlElement configuration)
    {
        var loggerRepos = LogManager
            .CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));

        _log = LogManager.GetLogger(loggerRepos.Name, category);

        XmlConfigurator.Configure(configuration);
    }

    public IDisposable BeginScope<TState>(TState state) => null!;


    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.None => false,
            LogLevel.Trace => _log.IsDebugEnabled,
            LogLevel.Debug => _log.IsDebugEnabled,
            LogLevel.Information => _log.IsInfoEnabled,
            LogLevel.Warning => _log.IsWarnEnabled,
            LogLevel.Error => _log.IsErrorEnabled,
            LogLevel.Critical => _log.IsFatalEnabled,
            _ => throw new InvalidEnumArgumentException(nameof(logLevel), (int)logLevel, typeof(LogLevel)),
        };
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (formatter is null) throw new ArgumentNullException(nameof(formatter));

        if (!IsEnabled(logLevel)) return;

        var logString = formatter(state, exception);

        if (string.IsNullOrWhiteSpace(logString) && exception is null)
            return;

        switch (logLevel)
        {
            default:
                throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            case LogLevel.Trace:
            case LogLevel.Debug:
                _log.Debug(logString);
                break;
            case LogLevel.Information:
                _log.Info(logString);
                break;
            case LogLevel.Warning:
                _log.Warn(logString);
                break;
            case LogLevel.Error:
                _log.Error(logString);
                break;
            case LogLevel.Critical:
                _log.Fatal(logString);
                break;
            case LogLevel.None:
                break;
        }
    }
}
