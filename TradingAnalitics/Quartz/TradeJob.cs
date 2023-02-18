using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Serilog;
using Tinkoff.Trading.OpenApi.Models;
using Tinkoff.Trading.OpenApi.Network;

namespace TradingAnalitics.Quartz
{
    public class TradeJob : IJob
    {
        private string token = "t.Fg2Z6pXJXSe3I_JmSl3jTDGLThOYjlzk5xeIY4305xLCxvdGlZ9VgQ8tV3QTEkhg6IW2ahQTrkENTfVH3-p75w";


        public async Task Execute(IJobExecutionContext context)
        {
            SandboxConnection connection = ConnectionFactory.GetSandboxConnection(token);
            SandboxContext ctx = connection.Context;

            var instrumentList = await ctx.MarketStocksAsync();
            var nvda = instrumentList.Instruments.FirstOrDefault(e => e.Ticker.Contains("NVDA"));


            Calendar cal = CultureInfo.InvariantCulture.Calendar;
            int previousWeekNumb =cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday) - 1;
            var previousWeekMondayDate = DateHelper.GetFirstDateOfWeek(DateTime.Now.Year, previousWeekNumb, CultureInfo.CurrentCulture);
            var previousWeekSuturdayDate = previousWeekMondayDate.AddDays(5);


            var v0 = await ctx.MarketCandlesAsync(nvda.Figi, previousWeekMondayDate, previousWeekSuturdayDate, Tinkoff.Trading.OpenApi.Models.CandleInterval.Week);
            /*var acc = ctx.AccountsAsync();
            var pos = ctx.PortfolioAsync("SB1171823");
            MarketInstrumentList instrumentList = ctx.MarketStocksAsync();
            var appl = instrumentList.Instruments.FirstOrDefault(e => e.Ticker.Contains("AAPL"));
            MarketOrder order = new MarketOrder("BBG000B9XRY4", 1, OperationType.Buy, "SB1171823");
            var v = ctx.PlaceMarketOrderAsync(order);
            var pos2 = ctx.PortfolioAsync("SB1171823");*/

            Log.Logger.Information($"{v0.Candles[0].High}, {v0.Candles[0].Low}, {v0.Candles[0].Open}, {v0.Candles[0].Close}");

            await Task.CompletedTask;
            
        }
    }
}
