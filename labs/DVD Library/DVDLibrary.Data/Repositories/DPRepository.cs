using Dapper;
using DVDLibrary.Data.Interfaces;
using DVDLibrary.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DVDLibrary.Data.Repositories
{
    public class DPRepository : IDvdRepository
    {
        public void AddDvd(Dvd dvd)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                
                parameters.Add("@Id"            , dbType: DbType.Int32
                                                , direction: ParameterDirection.Output);
                parameters.Add("@Title"         , dvd.Title);
                parameters.Add("@ReleaseYear"   , dvd.ReleaseYear);
                parameters.Add("@Director"      , dvd.Director);
                parameters.Add("@Rating"        , dvd.Rating);
                parameters.Add("@Notes"         , dvd.Notes);

                cn.Execute("DvdInsert", parameters, commandType: CommandType.StoredProcedure);

                dvd.Id = parameters.Get<int>("@Id");
            }
        }

        public void DeleteDvd(int id)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@ID", id);

                conn.Execute("DvdDelete", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Dvd> DvdsSelectByCategory<T>(string category, T given)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@" + category, given);

                return conn.Query<Dvd>(
                    "DvdsSelectBy" + category,
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
        }

        public List<Dvd> GetAllDvds()
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = Settings.GetConnectionString();

                return conn.Query<Dvd>(
                    "DvdsSelectAll",
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
        }

        public Dvd GetDvdById(int id)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                return conn.Query<Dvd>(
                    "DvdSelectById",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();
            }
        }

        public List<Dvd> GetDvdsByDirector(string director)
        {
            return DvdsSelectByCategory("Director", director);
        }

        public List<Dvd> GetDvdsByRating(string rating)
        {
            return DvdsSelectByCategory("Rating", rating);
        }

        public List<Dvd> GetDvdsByReleaseYear(int releaseYear)
        {
            return DvdsSelectByCategory("ReleaseYear", releaseYear);
        }

        public List<Dvd> GetDvdsByTitle(string title)
        {
            return DvdsSelectByCategory("Title", title);
        }

        public void UpdateDvd(Dvd dvd)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();

                parameters.Add("@Id"         , dvd.Id);
                parameters.Add("@Title"      , dvd.Title);
                parameters.Add("@ReleaseYear", dvd.ReleaseYear);
                parameters.Add("@Director"   , dvd.Director);
                parameters.Add("@Rating"     , dvd.Rating);
                parameters.Add("@Notes"      , dvd.Notes);

                cn.Execute("DvdUpdate", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
