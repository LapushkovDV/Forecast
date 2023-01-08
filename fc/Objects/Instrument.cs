using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinkoff.Trading.OpenApi;
using Tinkoff.Trading.OpenApi.Models;
using Tinkoff.Trading.OpenApi.Network;

namespace fc
{
    
    /// <summary>
    /// Этот класс для инструментов. Храню название, .... 
    /// здесь ещё можно добавить текущую аналитическую информацию 
    /// типа когда получен прогноз по инструменту делаем фиктивную 
    /// закупку и смотрим потенциальный рост в течение одной недели(для этого  
    /// </summary>
    public class Instrument
    {
        public int Id { get; set; }
        public DateTime Raw_dateTime { get; set; }
        public string Figi { get; set; }
        public string Ticker { get; set; }
        public string Isin { get; set; }
        public decimal MinPriceIncrement { get; set; }
        public int Lot { get; set; }
        public Currency Currency { get; set; }
        public string Name { get; set; }
        public InstrumentType Type { get; set; }
        
        private SandboxConnection connection;
        
        private SandboxContext context;
                                
        private string token = "t.Fg2Z6pXJXSe3I_JmSl3jTDGLThOYjlzk5xeIY4305xLCxvdGlZ9VgQ8tV3QTEkhg6IW2ahQTrkENTfVH3-p75w";
        
        private Portfolio portfolio;
        
        
        
        public Instrument()
        {
            connection = ConnectionFactory.GetSandboxConnection(token);
            context = connection.Context;
        }


        bool CheckingInstrumentExistenseInDatabase(string a, string b)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var instrument = db.Instruments.FirstOrDefault(p => p.Ticker == a && p.Figi == b);

                if (instrument == null)
                {
                    return true;
                }
                else
                    return false;
            }
        }
        public async void GetInstrument()
        {
            using (MyDbContext db = new MyDbContext()) 
            {
                MarketInstrumentList instrumentList = await context.MarketStocksAsync();
                int n = 1;
                foreach (var item in instrumentList.Instruments.Where(e => e.Ticker.Contains("")))
                {
                    if (CheckingInstrumentExistenseInDatabase(item.Ticker, item.Figi))
                    {
                        Instrument instrument = new Instrument()
                        {

                            Raw_dateTime = DateTime.Now,
                            Figi = item.Figi,
                            Ticker = item.Ticker,
                            Isin = item.Isin,
                            MinPriceIncrement = item.MinPriceIncrement,
                            Lot = item.Lot,
                            Currency = item.Currency,
                            Name = item.Name,
                            Type = item.Type
                        };

                        db.Instruments.Add(instrument);
                        db.SaveChanges();
                        Console.WriteLine($@"{n}. В БД добавлен инструмент TICKER: {item.Ticker}, FIGI: {item.Figi} ");
                        n++;
                        
                    }
                    else

                    {
                        //если прогноз уже есть в Бд выводим в консоль. 
                        Console.WriteLine($@"{n}. Инструмент с TICKER: {item.Ticker}, FIGI {item.Figi} уже есть в БД ");
                        n++;
                    }

                   // Console.WriteLine(n + ": " + item.Ticker + " " + item.Name + " " + item.Currency + " " + item.Figi);
                   // n++;
                }
            }
            
        }
    }
}
    

    


