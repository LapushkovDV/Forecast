using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using fc.Objects;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.InteropServices;

namespace fc
{


    public class Response
    {

        DateTime responseDateTime; //когда запросил прогноз



        /// <summary>
        /// строка ниже должна быть возвращаемым значением ХТТП запроса в котором передается токен и номер инструмента
        /// https://api-invest.tinkoff.ru/smartfeed-public/v1/feed/api/instruments/SPR/forecasts?id_kind=ticker&appName=invest_terminal&sessionId=FPEK0XO86HYrzrD8OgIcTlTBPgWqsxU8.ds-prod-api65
        /// </summary>
        /// 
        public void ConnectionToDBForResponses() 
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["tnkDB"].ConnectionString);

            sqlConnection.Open();

            if (sqlConnection.State == ConnectionState.Open)
            {
                Console.WriteLine("Подключились к БД...");
            }
        }

        /// <summary>
        /// Генерируем строку SQL запроса для инсерта в БД нового прогноза
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p"></param>
        /// <returns></returns>
  //       string CreatingSQLCommand(Forecasts.Targets t, Forecasts.Consensus c) 
  //      {
  //          string createdRequest = "insert into Forecasts (id," +
  //"Consensus_ticker,          Consensus_recommendation,       Consensus_current_price,    Consensus_currency," +
  //"Consensus_consensus,       Consensus_min_target,           Consensus_max_target,       Consensus_price_change," +
  //"Consensus_price_change_rel, Targets_ticker,                 Targets_analyst,            Targets_company," +
  //"Targets_recommendation,    Targets_recommendation_date,    Targets_current_price,      Targets_currency," +
  //"Targets_target_price,      Targets_price_change,           Targets_price_change_rel,   Targets_logo_name," +
  //"Targets_logo_base_color,   Targets_show_name)" + "values (NEWID(), \'" +
  //c.ticker + "\', N\'" + c.recommendation + "\' , " + c.current_price + ", \'" + c.currency + "\', " +
  //c.consensus + ", " + c.min_target + ", " + c.max_target + ", " + c.price_change + ", " +
  //c.price_change_rel + ", \'" + t.ticker + "\', \'" + t.analyst + "\', \'" + t.company + "\', N\'" +
  //t.recommendation + "\', \'" + t.recommendation_date + "\', " + t.current_price + ", \'" + t.currency + "\', " +
  //t.target_price + ", " + t.price_change + ", " + t.price_change_rel + ", \'" + t.logo_name + "\', \'" +
  //t.logo_base_color + "\', \'" + t.show_name + "\')"; 

  //          return createdRequest;
  //      }
    public void convertingJson(string a) 
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["tnkDB"].ConnectionString);

            sqlConnection.Open();

            if (sqlConnection.State == ConnectionState.Open)
            {
                Console.WriteLine("Подключились к БД...");
            }

            Forecasts.Obj asd = JsonConvert.DeserializeObject<Forecasts.Obj>(a);

                foreach (Forecasts.Targets target in asd.payload.targets)
                {

                    Console.WriteLine(target.target_price);

                    /// <summary>
                    /// 1)смотрим есть ли уже в БД такая запись и если нет делаем инсерт, если есть пропускаем ВСТАВКУ
                    /// 2)делаем контрольный SELECT по записи что бы убедиться, что запись заинсертилась, делаем ОК в лог(надо создать)
                    /// 3)пока идей нет
                    /// </summary>
                  

                        new SqlCommand(CreatingSQLCommand(target, asd.payload.consensus), sqlConnection);
                        //Console.WriteLine(query.ExecuteNonQuery());
                   
                    Console.ReadKey();
                }
                
            
            //var sdfds = JsonConvert.DeserializeObject<Forecasts.Obj>(a);


            
            Console.ReadKey();
        }


        
    }

   
}
