using DVDLibrary.Data.Interfaces;
using System;
using System.Collections.Generic;
using DVDLibrary.Models;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace DVDLibrary.Data.Repositories
{
    public class ADORepository : IDvdRepository
    {
        private static List<Dvd> ExecuteStoredProcedure(string sproc, SqlParameter[] paramArray)
        {
            List<Dvd> dvds = new List<Dvd>();

            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand(sproc, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(paramArray);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var dvd = new Dvd();

                        dvd.Id = (int) dr["Id"];
                        dvd.Title = dr["Title"].ToString();

                        if (dr["ReleaseYear"] != DBNull.Value)
                            dvd.ReleaseYear = (int) dr["ReleaseYear"];

                        if (dr["Director"] != DBNull.Value)
                            dvd.Director = dr["Director"].ToString();

                        if (dr["Rating"] != DBNull.Value)
                            dvd.Rating = dr["Rating"].ToString();

                        if (dr["Notes"] != DBNull.Value)
                            dvd.Notes = dr["Notes"].ToString();

                        dvds.Add(dvd);
                    }
                }
            }
            return dvds;
        }

        public void AddDvd(Dvd dvd)
        {
            var paramArray = new SqlParameter[6];

            paramArray[0] = new SqlParameter("@Id", SqlDbType.Int           ) { Direction = ParameterDirection.Output };
            paramArray[1] = new SqlParameter("@Title", SqlDbType.NVarChar   ) { Value = dvd.Title       };
            paramArray[2] = new SqlParameter("@ReleaseYear", SqlDbType.Int  ) { Value = dvd.ReleaseYear };
            paramArray[3] = new SqlParameter("@Director", SqlDbType.NVarChar) { Value = dvd.Director    };
            paramArray[4] = new SqlParameter("@Rating", SqlDbType.Char      ) { Value = dvd.Rating      };
            paramArray[5] = new SqlParameter("@Notes", SqlDbType.NVarChar   ) { Value = dvd.Notes       };

            ExecuteStoredProcedure("DvdInsert", paramArray);
        }

        public void DeleteDvd(int id)
        {
            var param = new SqlParameter("@Id", SqlDbType.Int) { Value = id };
            ExecuteStoredProcedure("DvdDelete", new SqlParameter[] { param });
        }

        public List<Dvd> GetAllDvds()
        {
            return ExecuteStoredProcedure("DvdsSelectAll", new SqlParameter[0]);
        }

        public Dvd GetDvdById(int id)
        {
            var param = new SqlParameter("@Id", SqlDbType.Int) { Value = id };
            return ExecuteStoredProcedure("DvdSelectById", new SqlParameter[] { param }).FirstOrDefault();
        }

        public List<Dvd> GetDvdsByDirector(string director)
        {
            var param = new SqlParameter("@Director", SqlDbType.NVarChar) { Value = director };
            return ExecuteStoredProcedure("DvdsSelectByDirector", new SqlParameter[] { param });
        }

        public List<Dvd> GetDvdsByRating(string rating)
        {
            var param = new SqlParameter("@Rating", SqlDbType.NVarChar) { Value = rating };
            return ExecuteStoredProcedure("DvdsSelectByRating", new SqlParameter[] { param });
        }

        public List<Dvd> GetDvdsByReleaseYear(int releaseYear)
        {
            var param = new SqlParameter("@ReleaseYear", SqlDbType.NVarChar) { Value = releaseYear };
            return ExecuteStoredProcedure("DvdsSelectByReleaseYear", new SqlParameter[] { param });
        }

        public List<Dvd> GetDvdsByTitle(string title)
        {
            var param = new SqlParameter("@Title", SqlDbType.NVarChar) { Value = title };
            return ExecuteStoredProcedure("DvdsSelectByTitle", new SqlParameter[] { param });
        }

        public void UpdateDvd(Dvd dvd)
        {
            var paramArray = new SqlParameter[6];

            paramArray[0] = new SqlParameter("@Id", SqlDbType.Int           ) { Value = dvd.Id          };
            paramArray[1] = new SqlParameter("@Title", SqlDbType.NVarChar   ) { Value = dvd.Title       };
            paramArray[2] = new SqlParameter("@ReleaseYear", SqlDbType.Int  ) { Value = dvd.ReleaseYear };
            paramArray[3] = new SqlParameter("@Director", SqlDbType.NVarChar) { Value = dvd.Director    };
            paramArray[4] = new SqlParameter("@Rating", SqlDbType.NVarChar  ) { Value = dvd.Rating      };
            paramArray[5] = new SqlParameter("@Notes", SqlDbType.NVarChar   ) { Value = dvd.Notes       };

            ExecuteStoredProcedure("DvdUpdate", paramArray);
        }
    }
}
