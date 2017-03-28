using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using _03A_Movie_Catalog.Models;

namespace _03A_Movie_Catalog.Data
{
    public class MovieRepository
    {
        public IEnumerable<MovieListView> GetAllMovies()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["MovieCatalog"].ConnectionString;

                return cn.Query<MovieListView>("MovieSelectAll", commandType: CommandType.StoredProcedure);
            }
        }

        public Movie GetMovieById(int id)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["MovieCatalog"].ConnectionString;

                // create parameter object
                var parameters = new DynamicParameters();
                parameters.Add("@MovieId", id);

                return cn.Query<Movie>("MovieSelectById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public void MovieDelete(int id)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["MovieCatalog"].ConnectionString;

                // create parameter object
                var parameters = new DynamicParameters();
                parameters.Add("@MovieId", id);

                cn.Execute("MovieDelete", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void MovieInsert(Movie movie)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["MovieCatalog"].ConnectionString;

                // create parameter object
                var parameters = new DynamicParameters();

                // declare output parameter
                parameters.Add("@MovieId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Title", movie.Title);
                parameters.Add("@GenreId", movie.GenreId);
                parameters.Add("@RatingId", movie.RatingId);

                cn.Execute("MovieInsert", parameters, commandType: CommandType.StoredProcedure);

                movie.MovieId = parameters.Get<int>("@MovieId");
            }
        }

        public void MovieEdit(Movie movie)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["MovieCatalog"].ConnectionString;

                // create parameter object
                var parameters = new DynamicParameters();

                // declare output parameter
                parameters.Add("@MovieId", movie.MovieId);
                parameters.Add("@Title", movie.Title);
                parameters.Add("@GenreId", movie.GenreId);
                parameters.Add("@RatingId", movie.RatingId);

                cn.Execute("MovieUpdate", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Genre> GetGenres()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["MovieCatalog"].ConnectionString;

                return cn.Query<Genre>("GenreSelectAll", commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Rating> GetRatings()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["MovieCatalog"].ConnectionString;

                return cn.Query<Rating>("RatingSelectAll", commandType: CommandType.StoredProcedure);
            }
        }
    }
}


                