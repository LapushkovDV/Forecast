using System;

namespace fc.Objects
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Raw_dateTime { get; set; }
        public string Figi { get; set; }
        public string Name { get; set; }

        public string Price { get; set; }
       
    }
}
