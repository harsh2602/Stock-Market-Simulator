using System;
using System.Data.SqlClient;


namespace StockMarketSimulator.Utilities
{
    interface iSqlQuery
    {
        int Execute();
    }

    /*
    Abstract Superclass DataBaseAccess
    Utilizes the Singleton Design pattern to mantain one instance of
    SQLConnection, and one instance of a SQLCommands
    */
    public abstract class DataBaseAccess : iSqlQuery
    {
        protected string cmdString { get; set; }

        // Constructor, takes as input 1 string
        public DataBaseAccess(string queryStr)
        {
            cmd.CommandText = queryStr;
        }

        // Singleton Design Pattern
        private SqlConnection db_;
        protected SqlConnection db
        {
            get
            {
                if (db_ == null)
                {
                    db_ = new SqlConnection(ConnectionString.ConnString);
                }
                return db_;
            }
        }

        // Singleton Design Pattern
        private SqlCommand cmd_;
        protected SqlCommand cmd
        {
            get
            {
                if (cmd_ == null)
                {
                    cmd_ = new SqlCommand();
                    cmd_.Connection = db;
                }
                return cmd_;
            }
        }

        public abstract int Execute();
    }

    public class DB_ScalarQuery : DataBaseAccess
    {
        // property to hold the return value of the just executed scalar query
        public int result { get; set; }

        public override int Execute()
        {
            db.Open();
            result = Convert.ToInt32( cmd.ExecuteScalar() );
            db.Close();
            return result;
        }

        // constructor, takes 1 string as input
        // calls the base constructor to initialize variables
        public DB_ScalarQuery(string queryStr) : base(queryStr) { }

    }

    public class DB_NonQuery : DataBaseAccess
    {
        public DB_NonQuery(string queryStr) : base(queryStr) { }

        public override int Execute()
        {
            db.Open();
            try {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return -1;
            }
            return 0;
            
        }
    }
}