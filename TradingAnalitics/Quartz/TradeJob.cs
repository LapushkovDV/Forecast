using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Tinkoff.Trading.OpenApi.Models;
using Tinkoff.Trading.OpenApi.Network;

namespace TradingAnalitics.Quartz
{
    public class TradeJob : IJob
    {
        private string token = "t.Fg2Z6pXJXSe3I_JmSl3jTDGLThOYjlzk5xeIY4305xLCxvdGlZ9VgQ8tV3QTEkhg6IW2ahQTrkENTfVH3-p75w";


        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"{DateTime.Now} execute begin");

            SandboxConnection connection = ConnectionFactory.GetSandboxConnection(token);
            SandboxContext ctx = connection.Context;

            var v0 = await ctx.MarketCandlesAsync("BBG0113JGQF0", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-2), Tinkoff.Trading.OpenApi.Models.CandleInterval.Day);
            /*var acc = ctx.AccountsAsync();
            var pos = ctx.PortfolioAsync("SB1171823");
            MarketInstrumentList instrumentList = ctx.MarketStocksAsync();
            var appl = instrumentList.Instruments.FirstOrDefault(e => e.Ticker.Contains("AAPL"));
            MarketOrder order = new MarketOrder("BBG000B9XRY4", 1, OperationType.Buy, "SB1171823");
            var v = ctx.PlaceMarketOrderAsync(order);
            var pos2 = ctx.PortfolioAsync("SB1171823");*/
            //Console.WriteLine($"{v0.Candles[0].High}");

            Console.WriteLine($"{v0.Candles[0].High}");

            await Task.CompletedTask;
            
        }

    }
}
