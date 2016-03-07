using System;

namespace StockMarketSimulator.Utilities
{
    public class ExecuteScalar : DataBaseAccess
    {
        // property to hold the return value of the just executed scalar query
        public int result { get; set; }


        // constructor, takes 1 string as input
        // calls the base constructor to initialize variables
        public ExecuteScalar(string queryStr) : base(queryStr) { }

        
        public override int execute()
        {
            db.Open();
            result = Convert.ToInt32(cmd.ExecuteScalar());
            db.Close();
            return result;
        }

    }
}