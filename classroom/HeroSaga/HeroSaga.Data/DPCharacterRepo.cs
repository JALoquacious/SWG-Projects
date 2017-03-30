using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace HeroSaga.Data
{
    public class DPCharacterRepo : ICharacterRepo
    {
        private readonly string _connStr;

        public DPCharacterRepo()
        {
            _connStr = ConfigurationManager.ConnectionStrings["HeroSagaEntities"].ConnectionString;
        }
        public Character CreateCharacter(Character model)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connStr;
                var paramaters = new DynamicParameters();
                paramaters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                paramaters.Add("@Name", model.Name);
                paramaters.Add("@Desc", model.Desc);
                conn.Execute("Insert INTO Characters ([Name], [Desc]) Values (@Name,@Desc); Set @Id = scope_identity()", paramaters);
                model.ID = paramaters.Get<int>("@Id");
                return model;
            }
        }

        public void DeleteCharacter(int id)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connStr;
                var paramaters = new DynamicParameters();
                paramaters.Add("@Id", id);
                conn.Execute("Delete From  Characters where @id = @Id", paramaters);
            }
        }

        public Character GetCharacter(int id)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connStr;
                var paramaters = new DynamicParameters();
                paramaters.Add("@Id",id);
                return conn.Query<Character>("Select * From Characters Where Id = @Id",paramaters).FirstOrDefault();
            }
        }

        public IEnumerable<Character> GetCharacters()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connStr;
                return conn.Query<Character>("Select * From Characters");
            }
        }

        public void UpdateCharacter(Character model)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connStr;
                var paramaters = new DynamicParameters();
                paramaters.Add("@Id",model.ID);
                paramaters.Add("@Name", model.Name);
                paramaters.Add("@Desc", model.Desc);
                 conn.Execute("Update Characters Set [Name] = @Name, Desc = @Desc where @id = @Id", paramaters);
            }
        }
    }
}
