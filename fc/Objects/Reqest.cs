using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using fc;
using Newtonsoft.Json;


namespace fc
{

    /// <summary>
    /// Класс для генерации запросов по инструментам
    /// Можно будет хранить историю запросов, но вроде нахер не надо
    /// </summary>
    public class Request
    {
        public int countAdded;
     
        public string CreateUrl(string a) //метод генерации строки, возможно придется ещё больше распарсить.
        {
            string url = @"https://api-invest.tinkoff.ru/smartfeed-public/v1/feed/api/instruments/" + a + "/forecasts?id_kind=ticker&appName=invest_terminal&sessionId=Y2W85l0SPs5XKKT2dkRwcZcRTQ9FX6Kd.m1-prod-api149";
            return url;      
        }
        

        public void Reqest() { //формирую подключение 

            using (MyDbContext db = new MyDbContext()) 
            {

                var v = db.Instruments;

                var v2 = db.Instruments.Where(x => x.Name == "ГК Мать и дитя").FirstOrDefault();

                foreach (var a in db.Instruments.ToList())
                using (var webClient = new WebClient())
                {
                    var request = WebRequest.Create(CreateUrl(a.Ticker));


                    var response = request.GetResponse(); //as HttpWebResponse;
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        string line;

                        if ((line = stream.ReadLine()) != null)
                        {
                            Forecast forecast = new Forecast();
                            forecast.ConvertingJson(line);
                                
                        }
                    }
                }
            }

            //+Console.WriteLine("Добавлено " + 6 + " запросов");
            Console.WriteLine("никуя нет... \r\n...жмакай, че ждешь?");
            
        }
        
    }
}
