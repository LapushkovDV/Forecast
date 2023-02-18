using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fc;
using fc.Objects;
using Quartz;
using Quartz.Impl;
using Serilog;
using Tinkoff.Trading.OpenApi.Models;
using Tinkoff.Trading.OpenApi.Network;
using Order = fc.Objects.Order;

namespace TradingAnalitics.Quartz
{
    public class TradeJob : IJob
    {
        private string token = "t.Fg2Z6pXJXSe3I_JmSl3jTDGLThOYjlzk5xeIY4305xLCxvdGlZ9VgQ8tV3QTEkhg6IW2ahQTrkENTfVH3-p75w";


        public async Task Execute(IJobExecutionContext context)
        {
            SandboxConnection connection = ConnectionFactory.GetSandboxConnection(token);
            SandboxContext ctx = connection.Context;

            var acc = await ctx.AccountsAsync();

            var instrumentList = await ctx.MarketStocksAsync();
            var instrument = instrumentList.Instruments.FirstOrDefault(e => e.Ticker.Contains("NVDA"));


            Calendar cal = CultureInfo.InvariantCulture.Calendar;
            int previousWeekNumb =cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday) - 1;
            var previousWeekMondayDate = DateHelper.GetFirstDateOfWeek(DateTime.Now.Year, previousWeekNumb, CultureInfo.CurrentCulture);
            var previousWeekSuturdayDate = previousWeekMondayDate.AddDays(5);

            if (instrument != null)
            {

                var v0 = await ctx.MarketCandlesAsync(instrument.Figi, previousWeekMondayDate, previousWeekSuturdayDate,
                    Tinkoff.Trading.OpenApi.Models.CandleInterval.Week);
                /*var acc = ctx.AccountsAsync();
                var pos = ctx.PortfolioAsync("SB1171823");
                MarketInstrumentList instrumentList = ctx.MarketStocksAsync();
                var appl = instrumentList.Instruments.FirstOrDefault(e => e.Ticker.Contains("AAPL"));
                MarketOrder order = new MarketOrder("BBG000B9XRY4", 1, OperationType.Buy, "SB1171823");
                var v = ctx.PlaceMarketOrderAsync(order);
                var pos2 = ctx.PortfolioAsync("SB1171823");*/

                Log.Logger.Information(
                    $"{v0.Candles[0].High}, {v0.Candles[0].Low}, {v0.Candles[0].Open}, {v0.Candles[0].Close}");

                // Дальше проверка, можно ли покупать
                bool canMakeBuyOrder = true;

                // Если можно покупать, проверка не купили ли уже

                if (canMakeBuyOrder)
                {
                    using (MyDbContext db = new MyDbContext())
                    {
                        var orders = db.Orders.ToList();

                        if (orders.Any(w => w.Figi == instrument.Figi))
                        {
                            Log.Logger.Information($"Has already bought {instrument.Name}");
                        }
                        else
                        {
                            MarketOrder order = new MarketOrder(instrument.Figi, 1, OperationType.Buy,
                                acc?.FirstOrDefault()?.BrokerAccountId);
                            var orderResult = ctx.PlaceMarketOrderAsync(order);
                            var newOrder = new Order
                                {Figi = instrument.Figi, Price = orderResult.Result.Commission.Value.ToString()};
                            orders.Add(newOrder);
                            await db.SaveChangesAsync();
                            Log.Logger.Information(
                                $"{orderResult.Result.OrderId} {orderResult.Result.Commission.Value} {orderResult.Result.Status}");
                        }
                    }
                }
            }
            else
            {
                await Task.FromException(new Exception("Хуета с инструментом"));
            }

            await Task.CompletedTask;
            
        }
    }
}
