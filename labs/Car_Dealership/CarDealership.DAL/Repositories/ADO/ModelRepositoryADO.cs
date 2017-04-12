using CarDealership.DAL.Interfaces;
using CarDealership.Models.Tables;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;
using CarDealership.Models.Queries;

namespace CarDealership.DAL.Repositories.ADO
{
    public class ModelRepositoryADO : IModelRepository
    {
        public IEnumerable<Model> GetAll()
        {
            var models = new List<Model>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("ModelsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Model row   = new Model();
                        row.ModelId = (int)dr["ModelId"];
                        row.MakeId  = (int)dr["MakeId"];
                        row.UserId  = dr["UserId"].ToString();
                        row.Name    = dr["Name"].ToString();

                        models.Add(row);
                    }
                }
            }
            return models;
        }

        public Model GetById(int modelId)
        {
            Model model = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("ModelSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ModelId", modelId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        model         = new Model();
                        model.ModelId = (int)dr["ModelId"];
                        model.MakeId  = (int)dr["MakeId"];
                        model.Year    = (int)dr["Year"];
                        model.UserId  = dr["UserId"].ToString();
                        model.Name    = dr["Name"].ToString();
                    }
                }
            }
            return model;
        }

        public IEnumerable<Model> GetByMakeId(int makeId)
        {
            var models = new List<Model>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("ModelSelectByMakeId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MakeId", makeId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Model row   = new Model();
                        row.ModelId = (int)dr["ModelId"];
                        row.MakeId  = (int)dr["MakeId"];
                        row.Year    = (int)dr["Year"];
                        row.UserId  = dr["UserId"].ToString();
                        row.Name    = dr["Name"].ToString();

                        models.Add(row);
                    }
                }
            }
            return models;
        }

        public IEnumerable<ModelUserQueryRow> GetModelUserTable()
        {
            var models = new List<ModelUserQueryRow>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("ModelAddView", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var row   = new ModelUserQueryRow();
                        row.Make  = dr["MakeName"].ToString();
                        row.Model = dr["ModelName"].ToString();
                        row.User  = dr["Email"].ToString();

                        if (dr["DateAdded"] != DBNull.Value)
                            row.DateAdded = DateTime.Parse(dr["DateAdded"].ToString());

                        models.Add(row);
                    }
                }
            }
            return models;
        }

        public void Insert(Model model)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelInsert", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter param = new SqlParameter("@ModelId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@MakeId", model.MakeId);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@Year", model.Year);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@DateAdded", model.DateAdded);

                cn.Open();

                cmd.ExecuteNonQuery();

                model.ModelId = (int)param.Value;
            }
        }
    }
}
