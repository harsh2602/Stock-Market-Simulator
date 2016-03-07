namespace StockMarketSimulator.Utilities
{
    // Connection String
    // Static. defined in 1 place and referenced everywhere else
    public class ConnectionString
    {
        private static string connString_;
        public static string ConnString
        {
            get
            {
                if (connString_ == null)
                {
                    connString_ = string.Format(
                        //Enrique's Connection String
                        @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Enrique\Desktop\StockMarketSimulator\TradingSimDatabase.mdf';Integrated Security=True;Connect Timeout=30"

                        //Gabe's Connection String
                        //@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Gabe\Desktop\repo\Code\Solution\StockMarketSimulator\TradingSimDatabase.mdf';Integrated Security=True;Connect Timeout=30"
                        //@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\Gabe\Desktop\repo\Code\GabeSolution\TradingSimDatabase.mdf'; Integrated Security = True; Connect Timeout = 30"

                        //Harsh's Connection String
                       // @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='I:\ASP.NET TUTORIAL\STOCKMARKETSIMULATOR\TRADINGSIMDATABASE.MDF'; Integrated Security = True; Connect Timeout = 30"

                    );
                }
                return connString_;
            }

        }

    }

}