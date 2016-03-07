create table TheRealPurchasesTable(
	UserID int foreign key references Users(UserID),
	CompanyCode nvarchar(64),
	TotalStocks int not null, 
	CurrentPrice float
);