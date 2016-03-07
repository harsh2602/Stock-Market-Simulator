//
// BusinessTier:  business logic, acting as interface between UI and data store.
//

using System;
using System.Collections.Generic;
using StockMarketSimulator.Utilities;
using System.Data;


namespace BusinessTier
{

	//
	// Business:
	//
	public class Business
	{
		//
		// Fields:
		//
		private string _DBFile;
		private DataAccessTier.Data datatier;


		//
		// Constructor:
		//
		public Business(string connectionString)
		{
			//_DBFile = connectionString;

			datatier = new DataAccessTier.Data(connectionString);
		}

		public Business()
		{
			datatier = new DataAccessTier.Data(ConnectionString.ConnString);
		}
		//
		// TestConnection:
		//
		// Returns true if we can establish a connection to the database, false if not.
		//
		public bool TestConnection()
		{
			return datatier.TestConnection();
		}

		//
		// GetUser
		//
		// Retrieves User from the database
		//
		public StockUser GetUser(string userName, string password)
		{
			string sql = string.Format(Records.LoginVerificationSQL, userName, password);
			DataSet ds = datatier.ExecuteNonScalarQuery(sql);
			DataTable dt = ds.Tables["UserDetails"];

            int userID = 0;
            string fName = null;
            string lName = null;
            string email = null;
            double funds = 0.0;
            foreach( DataRow row in dt.Rows)
            {
                userID = Convert.ToInt32(row["UserID"].ToString());
                fName = row["FirstName"].ToString();
                lName = row["LastName"].ToString();
                email = row["Email"].ToString();
                funds = Convert.ToDouble(row["FUNDS"].ToString());
            }
            return new StockUser(userID, fName, lName, email, funds);
		}

		//
		// GetMovie:
		//
		// Retrieves Movie object based on MOVIE ID; returns null if movie is not
		// found.
		//
		public Movie GetMovie(int MovieID)
		{
			//
			// TODO!
			//

			string sql = string.Format("SELECT MovieName FROM Movies WHERE MovieID={0};", MovieID);
			object result = datatier.ExecuteScalarQuery(sql);

			if (result == null || result.ToString() == "")
				return null;
			return new Movie(MovieID, result.ToString());
		}


		//
		// GetMovie:
		//
		// Retrieves Movie object based on MOVIE NAME; returns null if movie is not
		// found.
		//
		public Movie GetMovie(string MovieName)
		{
		  //
		  // TODO!
		  //

		  string sql = string.Format("SELECT MovieID FROM Movies WHERE MovieName='{0}';", MovieName);
		  object result = datatier.ExecuteScalarQuery(sql);

		  if (result == null || result.ToString() == "")
			return null;
		  return new Movie(Convert.ToInt32(result.ToString()), MovieName);
		}


	//
	// AddMovie:
	//
	// Adds the movie, returning a Movie object containing the name and the 
	// movie's id.  If the add failed, null is returned.
	//
	public Movie AddMovie(string MovieName)
	{
		//
		// TODO!
		//

		string sql = string.Format(@"
				INSERT INTO Movies(MovieName) Values('{0}');
				SELECT MovieID FROM Movies WHERE MovieID = SCOPE_IDENTITY();", MovieName);
		object result = datatier.ExecuteScalarQuery(sql);

		 if (result == null)
			return null;

		return new Movie(Convert.ToInt32(result.ToString()), MovieName);
	}


		//
		// AddReview:
		//
		// Adds review based on MOVIE ID, returning a Review object containing
		// the review, review's id, etc.  If the add failed, null is returned.
		//
		public Review AddReview(int MovieID, int UserID, int Rating)
		{
			//
			// TODO!
			//

			string sql = string.Format(@"
				INSERT INTO Reviews(MovieID, UserID, Rating) Values({0}, {1}, {2});
				SELECT ReviewID FROM Reviews WHERE ReviewID = SCOPE_IDENTITY();",
				MovieID, UserID, Rating);
			object result = datatier.ExecuteScalarQuery(sql);

			if (result == null)
				return null;

			return new Review(System.Convert.ToInt32(result.ToString()), MovieID, UserID, Rating);
		}


		//
		// GetMovieDetail:
		//
		// Given a MOVIE ID, returns detailed information about this movie --- all
		// the reviews, the total number of reviews, average rating, etc.  If the 
		// movie cannot be found, null is returned.
		//
		public MovieDetail GetMovieDetail(int MovieID)
		{
			//
			// TODO!
			//

			Movie movie = GetMovie(MovieID);
			if (movie == null)
				return null;

	  

			// All reviews
			int numReviews = 0;
			List<Review> reviews = new List<Review>();

			string sql = string.Format(@"
				SELECT ReviewID, UserID, Rating 
				FROM Reviews 
				WHERE MovieID={0}
				ORDER BY Rating Desc, UserID ASC;", MovieID);
			DataSet ds = datatier.ExecuteNonScalarQuery(sql);

			// Average rating
			sql = string.Format(@"
				SELECT ROUND(AVG(CAST(Rating AS Float)), 4) AS AvgRating 
				FROM Reviews
						INNER JOIN Movies ON Reviews.MovieID = Movies.MovieID
						WHERE Movies.MovieName='{0}';", (movie.MovieName).Replace("'", "''"));
			object result = datatier.ExecuteScalarQuery(sql);
			double avgRating = 0;
			if (result == null || result.ToString() == "")
				avgRating = 0.0;
		
			else
				avgRating = System.Convert.ToDouble(result.ToString());


			// If data empty, return empty results
			if (ds == null)
				return new MovieDetail(movie, avgRating, numReviews, reviews.AsReadOnly());

			DataTable dt = ds.Tables["TABLE"];

			// Creates list
			foreach (DataRow row in dt.Rows)
			{
				int reviewid = System.Convert.ToInt32((row["ReviewID"]).ToString());
				int userid = System.Convert.ToInt32((row["UserID"]).ToString());
				int rating = System.Convert.ToInt32((row["Rating"]).ToString());
				reviews.Add(new Review(reviewid, MovieID, userid, rating));
				numReviews++;
			}

			return new MovieDetail(movie, avgRating, numReviews, reviews.AsReadOnly());
		}


	//
	// GetUserDetail:
	//
	// Given a USER ID, returns detailed information about this user --- all
	// the reviews submitted by this user, the total number of reviews, average 
	// rating given, etc.  If the user cannot be found, null is returned.
	//
	public UserDetail GetUserDetail(int UserID)
	{
		//
		// TODO!
		//

	  User user = new User(UserID);
	  double avgRating = 0;
	  int numReviews = 0;
	  List<Review> reviews = new List<Review>();

	  // Rating counts
	  string sql = string.Format(@"SELECT COUNT(Rating) FROM Reviews WHERE UserID={0};", UserID);
	  object result = datatier.ExecuteScalarQuery(sql);

	  if (result == null)
		return null;

	  numReviews = System.Convert.ToInt32(result.ToString());

	  // User average rating
	  sql = string.Format(@"SELECT AVG(Rating) FROM Reviews WHERE UserID={0}", UserID);
	  result = datatier.ExecuteScalarQuery(sql);
	  avgRating = System.Convert.ToInt32(result.ToString());

	  sql = string.Format(@"SELECT ReviewID, MovieID, Rating FROM Reviews WHERE UserID={0}", UserID);
	  DataSet ds = datatier.ExecuteNonScalarQuery(sql);

	  // If data empty, return empty results
	  if (ds == null)
		return new UserDetail(user, avgRating, numReviews, reviews.AsReadOnly());

	  // Creates list
	  DataTable dt = ds.Tables["TABLE"];
	  foreach (DataRow row in dt.Rows)
	  {
		int reviewid = System.Convert.ToInt32((row["ReviewID"]).ToString());
		int movieid = System.Convert.ToInt32((row["MovieID"]).ToString());
		int rating = System.Convert.ToInt32((row["Rating"]).ToString());
		reviews.Add(new Review(reviewid, movieid, UserID, rating));
	  }

	  return new UserDetail(user, avgRating, numReviews, reviews.AsReadOnly());
	}


	//
	// GetTopMoviesByAvgRating:
	//
	// Returns the top N movies in descending order by average rating.  If two
	// movies have the same rating, the movies are presented in ascending order
	// by name.  If N < 1, an EMPTY LIST is returned.
	//
	public IReadOnlyList<Movie> GetTopMoviesByAvgRating(int N)
	{
	  
	  //
	  // TODO!
	  //

	  List<Movie> movies = new List<Movie>();
	  string sql = string.Format(@"
				SELECT TOP {0} Movies.MovieName, Movies.MovieID 
				FROM Movies
				INNER JOIN 
				(
					SELECT MovieID, ROUND(AVG(CAST(Rating AS Float)), 4) as AvgRating 
					FROM Reviews
					GROUP BY MovieID
				) g
				ON g.MovieID = Movies.MovieID
				ORDER BY g.AvgRating DESC, Movies.MovieName Asc;", N);
	  DataSet ds = datatier.ExecuteNonScalarQuery(sql);

	  // If data empty, return empty results
	  if (ds == null)
		return movies;

	  // Creates list
	  DataTable dt = ds.Tables["TABLE"];
	  foreach (DataRow row in dt.Rows)
	  {
		string movieName = (row["MovieName"]).ToString();
		int movieID = System.Convert.ToInt32((row["MovieID"]).ToString());
		movies.Add(new Movie(movieID, movieName));
	  }

	  return movies;
	}


	//
	// GetTopMoviesByNumReviews
	//
	// Returns the top N movies in descending order by number of reviews.  If two
	// movies have the same number of reviews, the movies are presented in ascending
	// order by name.  If N < 1, an EMPTY LIST is returned.
	//
	public IReadOnlyList<Movie> GetTopMoviesByNumReviews(int N)
	{
	  //
	  // TODO!
	  //

	  List<Movie> movies = new List<Movie>();
	  string sql = string.Format(@"
				SELECT TOP {0} Movies.MovieName, Movies.MovieID 
				FROM Movies
				INNER JOIN
				(
					SELECT MovieID, COUNT(*) as RatingCount 
					FROM Reviews
					GROUP BY MovieID
				) g
				ON g.MovieID = Movies.MovieID
				ORDER BY g.RatingCount DESC, Movies.MovieName Asc;", N);

	  DataSet ds = datatier.ExecuteNonScalarQuery(sql);

	  // If data empty, return empty results
	  if (ds == null)
		return movies;

	  // Creates list
	  DataTable dt = ds.Tables["TABLE"];
	  foreach (DataRow row in dt.Rows)
	  {
		string movieName = (row["MovieName"]).ToString();
		int movieID = System.Convert.ToInt32((row["MovieID"]).ToString());
		movies.Add(new Movie(movieID, movieName));
	  }

	  return movies;
	}


	//
	// GetTopUsersByNumReviews
	//
	// Returns the top N users in descending order by number of reviews.  If two
	// users have the same number of reviews, the users are presented in ascending
	// order by user id.  If N < 1, an EMPTY LIST is returned.
	//
	public IReadOnlyList<User> GetTopUsersByNumReviews(int N)
	{
	  
	  //
	  // TODO!
	  //

	  List<User> users = new List<User>();
	  string sql = string.Format(@"
				SELECT TOP {0} UserID, COUNT(*) AS RatingCount
				FROM Reviews
				GROUP BY UserID
				ORDER BY RatingCount DESC, UserID Asc;", N);
	  DataSet ds = datatier.ExecuteNonScalarQuery(sql);

	  // If data empty, return empty results
	  if (ds == null)
		return users;

	  // Create the reviews list
	  DataTable dt = ds.Tables["TABLE"];
	  foreach (DataRow row in dt.Rows)
	  {
		int userid = System.Convert.ToInt32((row["UserID"]).ToString());
		users.Add(new User(userid));
	  }

	  return users;
	}


	//
	// GetAllMovies
	//
	// Returns a list of all movies in the database in alphabetical order
	//
	public IReadOnlyList<Movie> GetAllMovies()
	{
	  List<Movie> movies = new List<Movie>();
	  string sql = string.Format(@"SELECT * FROM Movies order by MovieName");
	  DataSet ds = datatier.ExecuteNonScalarQuery(sql);

	  // If data empty, return empty results
	  if (ds == null)
		return null;

	  // Creates list
	  DataTable dt = ds.Tables["TABLE"];
	  foreach (DataRow row in dt.Rows)
	  {
		string movieName = (row["MovieName"]).ToString();
		int movieID = System.Convert.ToInt32((row["MovieID"]).ToString());
		movies.Add(new Movie(movieID, movieName));
	  }

	  return movies;
	}

  }//class

}//namespace
