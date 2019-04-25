using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Lab2.Web.Database
{
    public class DbSet<T> where T : new()
    {
        private readonly SqlConnection connection;
        private readonly string name;

        public DbSet(SqlConnection connection, string name)
        {
            this.connection = connection;
            this.name = name;
        }

        #region Public methods
        public IList<T> Get()
        {
            var entities = new List<T>();
            string sqlCommand = $"SELECT * FROM {name}";

            try
            {
                connection.Open();
                using (var command = new SqlCommand(sqlCommand, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var entity = GetEntity(reader);
                        entities.Add(entity);
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return entities;
        }

        public int Add(T entity)
        {
            string fields = string.Join(",", GetPropertiesForEntity(entity));
            string values = string.Join(",", GetValuesForEntity(entity));
            string sqlCommand = $"INSERT INTO {name} ({fields}) VALUES ({values})";

            try
            {
                connection.Open();
                using (var command = new SqlCommand(sqlCommand, connection))
                {
                    int result = command.ExecuteNonQuery();
                    return result;
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public int Update(T entity)
        {
            var fields = GetPropertiesForEntity(entity).ToList();
            var values = GetValuesForEntity(entity).ToList();
            string setFilelds = string.Join(",", Merge(fields, values).Select(keyValue => $"{keyValue.Key}={keyValue.Value}"));
            string idField = GetIdPropertyName(entity);
            Guid idValue = GetIdPropertyValue(entity);
            string sqlCommand = $"UPDATE {name} SET {setFilelds} WHERE {idField}='{idValue.ToString()}'";

            try
            {
                connection.Open();
                using (var command = new SqlCommand(sqlCommand, connection))
                {
                    int result = command.ExecuteNonQuery();
                    return result;
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public int Delete(T entity)
        {
            string idField = GetIdPropertyName(entity);
            Guid idValue = GetIdPropertyValue(entity);
            string sqlCommand = $"DELETE FROM {name} WHERE {idField}='{idValue.ToString()}'";

            try
            {
                connection.Open();
                using (var command = new SqlCommand(sqlCommand, connection))
                {
                    int result = command.ExecuteNonQuery();
                    return result;
                }
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion
        #region Private methods
        private T GetEntity(SqlDataReader reader)
        {
            var entity = new T();
            var type = entity.GetType();
            var properties = type.GetProperties().Where(x => x.CanWrite);

            foreach(var property in properties)
            {
                var value = GetValueForProperty(reader, property.Name);
                property.SetValue(entity, value);
            }

            return entity;
        }

        private object GetValueForProperty(SqlDataReader reader, string propertyName)
        {
            var index = Enumerable.Range(0, reader.FieldCount)
                .Select(i => (i, name: reader.GetName(i)))
                .FirstOrDefault(x => string.Equals(x.name, propertyName));

            return reader.GetValue(index.i);
        }

        private IEnumerable<string> GetPropertiesForEntity(T entity)
        {
            return entity.GetType()
                .GetProperties()
                .Where(x => x.CanWrite)
                .Select(x => x.Name);
        }

        private IEnumerable<string> GetValuesForEntity(T entity)
        {
            return entity.GetType()
                .GetProperties()
                .Where(x => x.CanWrite)
                .Select(x => x.GetValue(entity))
                .Select(ConvertValueToSql);
        }

        private string ConvertValueToSql(object value)
        {
            switch(value)
            {
                case DateTime dateTimeValue:
                    return $"'{dateTimeValue.ToString()}'";
                case string stringValue:
                    return $"'{stringValue}'";
                case Guid guidValue:
                    return $"'{guidValue}'";
                case null:
                    return "'NULL'";
                default:
                    return value.ToString();
            }
        }

        private string GetIdPropertyName(T entity)
        {
            return entity.GetType()
                .GetProperties()
                .FirstOrDefault(x => x.PropertyType == typeof(Guid))
                .Name;
        }

        private Guid GetIdPropertyValue(T entity)
        {
            return (Guid)entity.GetType()
                .GetProperties()
                .FirstOrDefault(x => x.PropertyType == typeof(Guid))
                .GetValue(entity);
        }

        private Dictionary<TKey, TValue> Merge<TKey, TValue>(IList<TKey> keys, IList<TValue> values)
        {
            var dic = new Dictionary<TKey, TValue>();

            for(int i = 0; i < keys.Count; i++)
            {
                dic.Add(keys[i], values[i]);
            }

            return dic;
        }
        #endregion
    }
}