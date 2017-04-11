using CarDealership.DAL.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CarDealership.DAL.Factories
{
    public class ContactRepositoryADO : IContactRepository
    {
        public void Insert(Contact contact)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("ContactInsert", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var param = new SqlParameter("@ContactId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Name", contact.Name);
                cmd.Parameters.AddWithValue("@Message", contact.Message);

                if (string.IsNullOrEmpty(contact.Phone))
                    cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Phone", contact.Phone);

                if (string.IsNullOrEmpty(contact.Email))
                    cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Email", contact.Email);

                cn.Open();

                cmd.ExecuteNonQuery();

                contact.ContactId = (int)param.Value;
            }
        }
    }
}