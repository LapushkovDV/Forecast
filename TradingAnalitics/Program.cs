using System;
using System.IO;
using fc;
using Serilog;
using Topshelf;

namespace TradingAnalitics
{
    public class Program
    {
        public static void Main()
        {
            string token = "t.Fg2Z6pXJXSe3I_JmSl3jTDGLThOYjlzk5xeIY4305xLCxvdGlZ9VgQ8tV3QTEkhg6IW2ahQTrkENTfVH3-p75w";

            /* SandboxConnection connection; connection = ConnectionFactory.GetSandboxConnection(token);
             SandboxContext context = connection.Context;

             var v0 = await context.MarketCandlesAsync("BBG0113JGQF0", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-2), Tinkoff.Trading.OpenApi.Models.CandleInterval.Day);



             var acc = await context.AccountsAsync();

             var pos = await context.PortfolioAsync("SB1171823");

             MarketInstrumentList instrumentList = await context.MarketStocksAsync();

             var appl = instrumentList.Instruments.FirstOrDefault(e => e.Ticker.Contains("AAPL"));

             MarketOrder order = new MarketOrder("BBG000B9XRY4", 1, OperationType.Buy, "SB1171823");

             var v = await context.PlaceMarketOrderAsync(order);

             var pos2 = await context.PortfolioAsync("SB1171823");
             */
            var logPath = @"C:\Log Files\Trd.txt";

            var rc = HostFactory.Run(x =>
            {
                x.Service<ScheduleService>(s =>
                {
                    s.ConstructUsing(name => new ScheduleService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.Console()
                    .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                x.UseSerilog(Log.Logger);

                x.SetDescription("Topshelf with Quartz and Serilog");
                x.SetDisplayName("Topshelf with Quartz");
                x.SetServiceName("Topshelf-Quartz");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
