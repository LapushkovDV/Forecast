using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAnalitics
{
    public class DateTimeRange
    {
        DateTime beginDate;
        DateTime endDate;

        public DateTime BeginDate
        {
            get { return beginDate; }
            set { beginDate = value; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
    }
}
