using System;
using System.Data.Entity;

namespace fc
{
    public class MyDbContext : DbContext

    {
        public MyDbContext() : base("DdConnectionString")
        {
        }
        public DbSet<Forecast> Forecasts { get; set; }

        public DbSet<Instrument> Instruments { get; set; }

        public DbSet<TrialPurchase> TrialPurchases { get; set; }

       // public DbSet<TrialDailyPrice> TrialDailyPrices { get; set; }
        
    
    }
}
