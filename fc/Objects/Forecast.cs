using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using fc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Runtime.InteropServices;

namespace fc
{
    
    public class Forecast
    {
        public int RowsAdded = 0;
        public int Id { get; set;}
        public DateTime Raw_dateTime { get; set; }
        public string Ticker { get; set; }
        public string Forecast_analyst { get; set; }
        public string Forecast_company { get; set; }
        public string Forecast_recommendation { get; set; }
        public DateTime Forecast_recommendation_date { get; set; }
        public string Forecast_current_price { get; set; }
        public string Forecast_currency { get; set; }
        public string Forecast_target_price { get; set; }
        public string Forecast_price_change { get; set; }
        public string Forecast_price_change_rel { get; set; }
        public string Forecast_show_name { get; set; }
        public string Consens { get; set; }
        public string Consensus_min_target { get; set; }
        public string Consensus_max_target { get; set; }
        public string Consensus_price_change { get; set; }
        public string Consensus_price_change_rel { get; set; }

        bool CheckingForecastExistenseInDatabase(string a, DateTime b)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var forecast = db.Forecasts.FirstOrDefault(acc => acc.Forecast_company == a && acc.Forecast_recommendation_date == b);

                if (forecast == null)
                {
                    return true;
                }
                else
                    return false;
            }
        }
        
        public void ConvertingJson(string a) //async
        {
            Obj asd = JsonConvert.DeserializeObject<Obj>(a);
            if (asd.Status == "Ok")
            {
                using (MyDbContext context = new MyDbContext())
                { // создаем подключение к БД

                    try
                    {
                        //int n = 0;

                        foreach (Targets target in asd.Payload.Targets)
                        {
                            Console.WriteLine($@"Проверяем {target.Ticker} от {target.Recommendation_date}");
                            //Console.WriteLine($"{DateTime.Now} прогноз {target.Ticker}");  
                            //n++;    
                            //await Task.Run(() =>
                            //{
                                //Здесь сделать проверку есть ли уже такая запись в БД
                                if (CheckingForecastExistenseInDatabase(target.Company, DateTime.Parse(target.Recommendation_date)))
                                {
                                    Forecast forecast = new Forecast()
                                    {

                                        Raw_dateTime = DateTime.Now,
                                        Ticker = target.Ticker,
                                        Forecast_analyst = target.Analyst,
                                        Forecast_company = target.Company,
                                        Forecast_recommendation = target.Recommendation,
                                        Forecast_recommendation_date = DateTime.Parse(target.Recommendation_date),
                                        Forecast_current_price = target.Current_price,
                                        Forecast_currency = target.Currency,
                                        Forecast_target_price = target.Target_price,
                                        Forecast_price_change = target.Price_change,
                                        Forecast_price_change_rel = target.Price_change_rel,
                                        Forecast_show_name = target.Show_name,
                                        Consens = asd.Payload.Consensus.consensus,
                                        Consensus_min_target = asd.Payload.Consensus.Min_target,
                                        Consensus_max_target = asd.Payload.Consensus.Max_target,
                                        Consensus_price_change = asd.Payload.Consensus.Price_change,
                                        Consensus_price_change_rel = asd.Payload.Consensus.Price_change_rel

                                    };

                                    context.Forecasts.Add(forecast);
                                    context.SaveChanges();


                                //{
                                //TrialPurchase tp = new TrialPurchase(forecast.Id, Forecast_current_price);
                                //}


                                RowsAdded++;
                                Console.WriteLine($@"В БД добавлен прогноз по {target.Ticker} от {target.Recommendation_date}");
                                Console.WriteLine(RowsAdded.ToString());
                                
                                }
                                else

                                {
                                    //если прогноз уже есть в Бдвыводим в консоль. 
                                    //Console.WriteLine($@"Прогноз по {target.Ticker} от {target.Recommendation_date} уже есть в БД ");
                                }

                            //});

                        }
                        

                    }
                    catch (Exception)
                    {

                        Console.WriteLine("Хуйня какая-то");
                    }
                    //добавляем новый прогноз


                }
            }
            

        }
    }





        public class Obj
        {
            [JsonProperty("status")]
            public string Status { get; set; }
            [JsonProperty("trackingId")]
            public string TrackingId { get; set; }
            [JsonProperty("payload")]
            public Payload Payload { get; set; }
        }

        public class Payload
        {
           public Targets[] Targets { get; set; }

            public Consensus Consensus { get; set; }

        }

        public class Targets
        {
            public string Ticker { get; set; }
            public string Analyst { get; set; }
            public string Company { get; set; }
            public string Recommendation { get; set; }
            public string Recommendation_date { get; set; }
            public string Current_price { get; set; }
            public string Currency { get; set; }
            public string Target_price { get; set; }
            public string Price_change { get; set; }
            public string Price_change_rel { get; set; }
            public string Logo_name { get; set; }
            public string Logo_base_color { get; set; }
            public string Show_name { get; set; }
        }

        public class Consensus
        {
            public string Ticker { get; set; }
            public string Recommendation { get; set; }
            public string Current_price { get; set; }
            public string Currency { get; set; }
            public string consensus { get; set; }
            public string Min_target { get; set; }
            public string Max_target { get; set; }
            public string Price_change { get; set; }
            public string Price_change_rel { get; set; }

        }
 
}
