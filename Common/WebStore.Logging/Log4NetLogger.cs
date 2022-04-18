using log4net;
using log4net.Config;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Xml;

namespace WebStore.Logging;

public class Log4NetLogger : ILogger, IDisposable
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

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        throw new NotImplementedException();
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        throw new NotImplementedException();
    }
}
