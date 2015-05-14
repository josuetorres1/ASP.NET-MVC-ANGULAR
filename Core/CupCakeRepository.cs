using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Yangaroo.Core.Models;
using Yangaroo.Core.Repositories;

namespace Yangaroo.Core
{
    public class CupCakeRepository : ICupCakeRepository
    {
        private readonly string _connectionString;

	    public CupCakeRepository(string connectionString)
	    {
		    _connectionString = connectionString;
	    }

        public IEnumerable<CupCake> GetAll()
        {
            using (new DatabaseHelper(_connectionString))
            {
                var result = DatabaseHelper.GetDataTable("GetAll");

                var dataRows = result.Rows.Cast<DataRow>();

                IList<CupCake> list = (new CupCake[] {}).ToList();
                
                var enumerable = dataRows as IList<DataRow> ?? dataRows.ToList();

                foreach (var r in enumerable)
                {
                    list.Add(new CupCake
                    {
                        Name = r.ItemArray[0].ToString(),
                        Price = decimal.Parse(r.ItemArray[1].ToString()),
                        DateTimeCreated = Convert.ToDateTime(r.ItemArray[2].ToString()),
                        DateTimeLastModified = Convert.ToDateTime(r.ItemArray[3].ToString()),
                        Id = int.Parse(r.ItemArray[4].ToString()),
                        Ingredients = null,
                        IngredientsOutput = BuildArrayIngredients(r.ItemArray[5].ToString())
                    });
                }

                return list;
            }
        }

        private static IEnumerable<string> BuildArrayIngredients(string array)
        {
            return !array.Equals(".") ? array.Split(new[] {'.'}).ToArray() : null;
        }

        public void Create(CupCake cupcake)
        {
            using (var dbHelper = new DatabaseHelper(_connectionString))
            {
                dbHelper.ExecuteNonQuery(false,
                                         "CreateCupCake",
                                         new SqlParameter("@id", cupcake.Id),
                                         new SqlParameter("@name", cupcake.Name),
                                         new SqlParameter("@price", cupcake.Price),
                                         new SqlParameter("@ingredients", cupcake.Ingredients));
            }
        }

        public void Update(CupCake cupcake)
        {
            using (var dbHelper = new DatabaseHelper(_connectionString))
            {
                dbHelper.ExecuteNonQuery(false,
                                         "UpdateCupCake",
                                         new SqlParameter("@Id", cupcake.Id),
                                         new SqlParameter("@name", cupcake.Name),
                                         new SqlParameter("@price", cupcake.Price));
            }
        }

        public void Delete(CupCake cupcake)
        {
            using (var dbHelper = new DatabaseHelper(_connectionString))
            {
                dbHelper.ExecuteNonQuery(false,
                                         "DeleteCupCake",
                                         new SqlParameter("@Id", cupcake.Id));
            }
        }
    }

    public static class SqlDataReaderExtensions
    {
        public static T Field<T>(this SqlDataReader reader, string columnName)
        {
            var obj = reader[columnName];

            if (obj is DBNull)
            {
                if (!Equals(null, default(T)))
                {
                    throw new InvalidCastException(string.Format("unable to assign null to type '{0}'", typeof(T).FullName));
                }

                obj = null;
            }

            return (T)obj;
        }
    }
}
