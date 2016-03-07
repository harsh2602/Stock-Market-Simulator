namespace StockMarketSimulator.Utilities
{
	public class Records
	{
		public static int CreateNewRecord(string s1, string s2, string s3, string s4, string s5, int n)
		{
			var newLoginDataQuery = string.Format(newUserQuery, s1, s2, s3, s4, s5, n);

			iSqlQuery createNewRecord = new DB_ScalarQuery(newLoginDataQuery);
			return createNewRecord.Execute();
		}

		public static int CurrentBalance(string userName)
		{
			string currentBal_Query = string.Format(fundsQuery, userName);
			iSqlQuery currentBalance = new DB_ScalarQuery(currentBal_Query);
			return currentBalance.Execute();
		}

		public static int CheckCredentials(string username, string password)
		{
			string query = string.Format(checkCredentials, username, password);
			iSqlQuery checkCredQuery = new DB_ScalarQuery(query);
			return checkCredQuery.Execute();
		}

		public static int PurchaseStocks(int numberOfStocks, 
										double stockUnitPrice, 
										string userName,
										string email,
										string companyCode)
		{
			int availableFunds = CurrentBalance(userName);
			double moneyTryingToSpend = numberOfStocks * stockUnitPrice;

			if(moneyTryingToSpend > availableFunds)
			{
				return -2; // not enough funds
			}
			string query = string.Format(purchaseStocksQuery,
										(availableFunds - moneyTryingToSpend),
										userName,
										email,
										companyCode,
										numberOfStocks,
										stockUnitPrice);

			iSqlQuery purchaseStock_Query = new DB_NonQuery(query);
            return purchaseStock_Query.Execute();
		}

        public static int AddToFavorites(string username, string text)
        {
            string query = string.Format(AddToFavoritesQuery, username, text);
            iSqlQuery addToFavorites = new DB_NonQuery(query);
            return addToFavorites.Execute();
        }

		private static string checkCredentials = @"
			Begin
			declare @Count int;
			declare @ReturnCode int;
			select @Count = COUNT(UserName)
			from Users where UserName= '{0}' and Pwd = '{1}';
			If @Count = 1
			Begin
				Set @ReturnCode = 1
			End
			Else
			Begin
				Set @ReturnCode = -1
			End
			Select @ReturnCode as ReturnValue
			End
			";

        public static string LoginVerificationSQL = @"
            SELECT DISTINCT UD.*
            FROM UserDetails AS UD
            WHERE UD.UserID  = (
                SELECT U.UserId
                FROM Users as U
                WHERE (U.UserName = '{0}' AND U.Pwd = '{1}'))";

        private static string newUserQuery = @"
			Begin
			declare @Count int;
			declare @ReturnCode int;
			select @Count = COUNT(UserName)
			from Users where UserName= '{1}';
			If @Count > 0
			Begin
				Set @ReturnCode = -1
			End
			Else
			Begin
				Set @ReturnCode = 1
			insert into Users(Pwd, UserName) values('{0}', '{1}');
			declare @userID int;
			select @userID = UserID from Users where UserName = '{1}';
			insert into UserDetails(UserID, FirstName, LastName, Email, Funds) values(@userID, '{2}', '{3}', '{4}', {5})
			End
			Select @ReturnCode as ReturnValue
			End
			";

		private static string fundsQuery = @"
			Begin
			declare @Funds int;
			select @Funds = UserDetails.Funds 
			from UserDetails 
			where UserID = (
				select Users.UserID 
				from Users 
				where Users.UserName ='{0}'
			);
			Select @Funds as ReturnValue
			End
			";

		private static string purchaseStocksQuery = @"
			update UserDetails set Funds = { 0 } 
			where UserID = (select Users.UserID from Users where UserName ='{1}');
			declare @user as int;
			select @user = Users.UserID 
			from Users 
			where UserName = '{2}';
			insert into PurchasedStocks(UserID, CompanyCode, NumStocks, Price, _Timestamp)
			values(@user, '{3}', { 4}, {5}, GETDATE());";


        private static string AddToFavoritesQuery = @"
                declare @UserID int
                select @UserID = UserID from Users where UserName = '{0}'
                insert into Favorites(UserID, CompanyCode) values (@UserID, '{1}')
                ";
	}
}