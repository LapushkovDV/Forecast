using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using fc;

using System.Data;
using System.Runtime.InteropServices;


namespace fc
{
    class Program
    {
        //public SqlConnection sqlConnection = null;//


        static void Main(string[] args)
        {
            //int num = 0;
            //// устанавливаем метод обратного вызова
            //TimerCallback tm = new TimerCallback(Count);
            //// создаем таймер
            //Timer timer = new Timer(tm, num, 0, 2000);

            //a: //точка перехода

            Instrument instrument = new Instrument();

            instrument.GetInstrument();

            Request reqest = new Request();

            reqest.Reqest();

            //goto a; //Переход
            //в точку перехода
            
            //TrialDailyPrice tdp = new TrialDailyPrice();
            
            //tdp.GetOrderBook();

//            Forecast forecast = new Forecast();

            Console.ReadKey();
                       
            //response.convertingJson("sds");
            
        }

        //public void CreateConnection (object sender, EventArgs e)
        //{
          
            
        //    sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["tnkDB"].ConnectionString);

        //    sqlConnection.Open();

        //    if (sqlConnection.State == ConnectionState.Open)
        //    {
        //        Console.WriteLine("Подключились к БД...");
        //    }
            
        //}

    }
}
