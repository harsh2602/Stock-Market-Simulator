//
// BusinessTier objects:  these classes define the objects serving as data 
// transfer between UI and business tier.  These objects carry the data that
// is normally displayed in the presentation tier.  The classes defined here:
//
//    Movie
//    Review
//    User
//    MovieDetail
//    UserDetail
//
// NOTE: the presentation tier should not be creating instances of these objects,
// but instead calling the BusinessTier logic to obtain these objects.  You can 
// create instances of these objects if you want, but doing so has no impact on
// the underlying data store --- to change the data store, you have to call the
// BusinessTier logic.
//

using System;
using System.Reflection;
using System.Collections.Generic;

namespace BusinessTier
{
    public class StockUser
    {
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public double funds { get; set; }

        public List<Company> favorites { get; set; }
        public List<Transaction> transactions { get; set; }
        public List<Stock> stocksOwned { get; set; }

        public StockUser(int ID, string FirstName, string LastName, string Email, double Funds)
        {
            this.ID = ID;
            firstName = FirstName;
            lastName = LastName;
            email = Email;
            funds = Funds;
        }
        
        public void AddToFavorites(Company cmp)
        {
            if(favorites == null)
            {
                favorites = new List<Company>();
            }
            favorites.Add(cmp);

        }
        public void AddToTransaction(Transaction transaction)
        {
            if (transactions == null)
            {
                transactions = new List<Transaction>();
            }
            transactions.Add(transaction);

        }

        public void UpdateStocksOwned(Transaction transaction)
        {
            Type purchase = typeof(Purchase);
            Type sale = typeof(Sale);
            if (stocksOwned == null)
            {
                
                if (transaction.GetType().Equals(sale))
                {
                    // error
                }
                else if (transaction.GetType().Equals(purchase))
                {
                    stocksOwned = new List<Stock>();
                    var newStock = new Stock(transaction.company, transaction.numberOfStocks);
                    stocksOwned.Add(newStock);
                }
            }
            else
            {
                foreach (var s in stocksOwned)
                {
                    if (s.company == transaction.company)
                    {
                        if (transaction.GetType().Equals(purchase))
                        {
                            s.total += transaction.numberOfStocks;
                        }
                        else if (transaction.GetType().Equals(sale))
                        {
                            s.total -= transaction.numberOfStocks;
                        }
                        break;
                    }
                }
            }
            
        }

    }

    

    public abstract class Transaction
    {
        public Company company { get; set; }
        public int numberOfStocks { get; set; }
        protected double price { get; set; }

        public Transaction(Company c, int num, double p)
        {
            company = c;
            numberOfStocks = num;
            price = p;
        }
        public abstract double Total();
    }

    public class Purchase : Transaction
    {
        public Purchase(Company c, int num, double p) : base(c, num, p) { }
        public override double Total()
        {
            return -1 * numberOfStocks * price;
        }
    }

    public class Sale : Transaction
    {
        public Sale(Company c, int num, double p) : base(c, num, p) { }
        public override double Total()
        {
            return price * numberOfStocks;
        }
    }

    public class Stock
    {
        public Company company { get; set; }
        public int total { get; set; }

        public Stock(Company company, int total)
        {
            this.company = company;
            this.total = total;
        }
    }

    public class Company
    {
        public string companyName { get; set; }
        public string companyCode { get; set; }

        public Company (string name, string code)
        {
            companyCode = code;
            companyName = name;
        }
    }


    //
    // Movie:
    //
    public class Movie
    {
        public readonly int MovieID;
        public readonly string MovieName;

        public Movie(int movieId, string movieName)
        {
            MovieID = movieId;
            MovieName = movieName;
        }

    }//class


  //
  // Review:
  //
  public class Review
  {
    public readonly int ReviewID;
    public readonly int MovieID;
    public readonly int UserID;
    public readonly int Rating;

    public Review(int reviewId, int movieId, int userId, int rating)
    {
      ReviewID = reviewId;
      MovieID = movieId;
      UserID = userId;
      Rating = rating;
    }
  }//class


  //
  // User:
  //
  public class User
  {
    public readonly int UserID;

    public User(int userId)
    {
      UserID = userId;
    }
  }

 
  //
  // MovieDetail:
  //
  // Given a movie object, returns details about this movie --- reviews, average 
  // rating given, etc.  
  //
  // NOTE: the reviews are returned in order by rating (descending 5, 4, 3, ...),
  // with secondary sort based on user id (ascending).
  //
  public class MovieDetail
  {
    public readonly Movie movie;
    public readonly double AvgRating;
    public readonly int NumReviews;
    public readonly IReadOnlyList<Review> Reviews;

    public MovieDetail(Movie m, double avgRating, int numReviews, IReadOnlyList<Review> reviews)
    {
      movie = m;
      AvgRating = avgRating;
      NumReviews = numReviews;
      Reviews = reviews;
    }
  }


  //
  // UserDetail:
  //
  // Given a user object, returns details about this user --- reviews, average 
  // rating given, etc.
  //
  // NOTE: the reviews are returned in order by rating (descending 5, 4, 3, ...),
  // with secondary sort based on movie id (ascending).
  //
  public class UserDetail
  {
    public readonly User user;
    public readonly double AvgRating;
    public readonly int NumReviews;
    public readonly IReadOnlyList<Review> Reviews;

    public UserDetail(User u, double avgRating, int numReviews, IReadOnlyList<Review> reviews)
    {
      user = u;
      AvgRating = avgRating;
      NumReviews = numReviews;
      Reviews = reviews;
    }
  }

}//namespace
