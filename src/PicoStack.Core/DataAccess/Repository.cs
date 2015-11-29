using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace PicoStack.Core.DataAccess
{
    public class Repository
    {
        private readonly string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<T> Get<T>()
        {
            var entityType = typeof(T);

            var properties = entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var propertyList = string.Join(", ", properties.Select(x => $"[{x.Name}]"));

            var query = $"select {propertyList} from [{entityType.Name}]";

            var result = new List<T>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    using (var reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var entity = (T)Activator.CreateInstance(entityType);

                            foreach (var propertyInfo in properties)
                            {
                                propertyInfo.SetValue(entity, reader[propertyInfo.Name], null);
                            }

                            result.Add(entity);
                        }
                    }
                }

                connection.Close();
            }

            return result;
        }
    }
}