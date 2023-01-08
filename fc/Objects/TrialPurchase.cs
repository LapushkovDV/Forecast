using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tinkoff.Trading.OpenApi;
using Tinkoff.Trading.OpenApi.Models;
using Tinkoff.Trading.OpenApi.Network;

namespace fc
{   /// <summary>
/// fgdfgdf
/// </summary>
    public class TrialPurchase
    {

        public int Id { get; set; }
        public DateTime Raw_dateTime { get; set; }
        public DateTime Recomendation_date { get; set; }
        public int Forecast_ID { get; set; }
        string Company_name { get; set; }
        string Ticker { get; set; }
        string buyPrice { get; set;}
        int    count { get; set; }



        public TrialPurchase(int Forecast_Id, string buyPrice)
        {
            using (MyDbContext db = new MyDbContext())
            {
                this.Forecast_ID = Forecast_Id;
                this.Raw_dateTime = DateTime.Now;
                this.buyPrice = buyPrice;
                this.count = 1;

                db.TrialPurchases.Add(this);
                db.SaveChanges();
            }
        }



        public bool CheckingTrialPurchaseExistenseInDatabase(string a, DateTime b)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var forecast = db.TrialPurchases.FirstOrDefault(acc => acc.Company_name == a && acc.Recomendation_date == b);

                if (forecast == null)
                {
                    return true;
                }
                else
                    return false;
            }
        }


    }

    public class TrialDailyPrice
    {
        public int Id { get; set; }
        public DateTime Raw_dateTime { get; set; }
        public int TrialPurcase_Id { get; set; }


        public TrialDailyPrice(int TrialPurcase_Id)
        {
            this.TrialPurcase_Id = TrialPurcase_Id;


        }



        private SandboxConnection connection;

        private SandboxContext context;

        private string token = "t.Fg2Z6pXJXSe3I_JmSl3jTDGLThOYjlzk5xeIY4305xLCxvdGlZ9VgQ8tV3QTEkhg6IW2ahQTrkENTfVH3-p75w";

        private Portfolio portfolio;

        public TrialDailyPrice()
        {
            connection = ConnectionFactory.GetSandboxConnection(token);
            context = connection.Context;
        }

        public async void GetOrderBook()
        {


            for (int i = 0; i < 50; i++)
            {
                Orderbook mob = await context.MarketOrderbookAsync("BBG000B9XRY4", 10);

                foreach (var item in mob.Asks)
                {
                    Console.WriteLine("item.Price" + item.Price + " " + item.Quantity + " " + mob.LastPrice);
                }



                foreach (var item in mob.Bids)
                {
                    Console.WriteLine("item.Price" + item.Price + " " + item.Quantity + " " + mob.LastPrice);
                }

                await Task.Delay(100);
            }

        }

        private void GetInstrumentInfoButton_Click(object sender, RoutedEventArgs e)
        {
            GetOrderBook();
        }


        private async void SumBidAsk(Orderbook orderbook)
        {
            orderbook = await context.MarketOrderbookAsync("BBG000B9XRY4", 10);

            int sAsk = 0;
            int sBid = 0;

            foreach (var item in orderbook.Asks)
            {
                sAsk = sAsk + item.Quantity;
                Console.WriteLine(sAsk);
            }
            Console.WriteLine("Quant for sell =" + sAsk);

            foreach (var item in orderbook.Bids)
            {
                sBid = sBid + item.Quantity;
                Console.WriteLine(sBid);
            }
            Console.WriteLine("Quant for sell =" + sBid);

        }

    }
}
