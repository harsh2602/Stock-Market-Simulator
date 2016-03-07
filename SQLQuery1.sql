select top 10 PurchasedStocks.UserID, FirstName, LastName, Funds, NumStocks as TotalStocks
from UserDetails Inner Join PurchasedStocks
On PurchasedStocks.UserID = UserDetails.UserID 
order by TotalStocks Desc;



select top 10 PurchasedStocks.UserID, UserDetails.FirstName, UserDetails.LastName, UserDetails.Funds, SUM(PurchasedStocks.NumStocks) as TotalStocks
from UserDetails Inner Join PurchasedStocks
On PurchasedStocks.UserID = UserDetails.UserID
group by PurchasedStocks.UserID, UserDetails.FirstName, UserDetails.LastName, UserDetails.Funds
order by TotalStocks Desc;