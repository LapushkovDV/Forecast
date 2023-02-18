using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinkoff.Trading.OpenApi.Models;
using Tinkoff.Trading.OpenApi.Network;

namespace TradingAnalitics
{

    /*
        Field           Type            Description
        figi            string          Deprecated Figi-идентификатор инструмента.Необходимо использовать instrument_id.
        quantity        int64           Количество лотов.
        price           Quotation       Цена за 1 инструмент.Для получения стоимости лота требуется умножить на лотность инструмента.Игнорируется для рыночных поручений.
        direction       OrderDirection  Направление операции.
        account_id      string          Номер счёта.
        order_type      OrderType       Тип заявки.
        order_id        string          Идентификатор запроса выставления поручения для целей идемпотентности. Максимальная длина 36 символов.
        instrument_id   string          Идентификатор инструмента, принимает значения Figi или Instrument_uid.
    */

    public class RequestMaker
    {
        private SandboxContext context;

        public List<Order> orders;

        public RequestMaker(SandboxContext _context)
        {
            context = _context;
        }


        public async void PostRequest()
        {
            MarketOrder order = new MarketOrder("figi", 1, OperationType.Buy, "brokerAccountId");

            var v = await context.PlaceMarketOrderAsync(order);

        }


        public async void GetOrders()
        {
            try
            {
                var acc = await context.AccountsAsync();

               var orderss = await context.OrdersAsync();

                string s = "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}