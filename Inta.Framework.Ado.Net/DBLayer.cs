using Inta.Framework.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Ado.Net
{
    public class DBLayer
    {
        private static string _connectionString = string.Empty;
        private static object lockControl = new object();

        public DBLayer(string connectionString)
        {
            lock (lockControl)
            {
                _connectionString = connectionString;
            }
        }
        public ReturnObject<bool> ExecuteNoneQuery(string commandText, System.Data.CommandType commandType, List<SqlParameter> Parameters = null)
        {
            ReturnObject<bool> returnObject = new ReturnObject<bool>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                try
                {
                    if (connection == null)
                        return new ReturnObject<bool> { Data = false, ResultType = MessageType.Error };

                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    SqlCommand cmd = new SqlCommand(commandText, connection);
                    cmd.CommandType = commandType;

                    if (Parameters != null)
                    {
                        foreach (var parameter in Parameters)
                            cmd.Parameters.Add(parameter);
                    }

                    int result = cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    connection.Dispose();
                    connection.Close();

                    if (result > 0)
                        return new ReturnObject<bool> { Data = true, ResultType = MessageType.Success };
                    else
                    {
                        connection.Dispose();
                        connection.Close();
                        return new ReturnObject<bool> { Data = false, ResultType = MessageType.Error };
                    }
                }
                catch (Exception ex)
                {
                    connection.Dispose();
                    connection.Close();
                    return new ReturnErrorObject<bool> { Data = false, ResultType = MessageType.Error, Exception = ex };
                }
            }
        }
        public ReturnObject<object> ExecuteScalar(string commandText, System.Data.CommandType commandType, List<SqlParameter> Parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                try
                {
                    if (connection == null)
                        return new ReturnObject<object> { Data = false, ResultType = MessageType.Error };

                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    SqlCommand cmd = new SqlCommand(commandText, connection);
                    cmd.CommandType = commandType;

                    if (Parameters != null)
                    {
                        foreach (SqlParameter parameter in Parameters)
                            cmd.Parameters.Add(parameter);
                    }

                    object result = cmd.ExecuteScalar();

                    cmd.Dispose();
                    connection.Dispose();
                    connection.Close();
                    return new ReturnObject<object> { Data = result, ResultType = MessageType.Success };
                }
                catch (Exception ex)
                {
                    connection.Dispose();
                    connection.Close();
                    return new ReturnErrorObject<object> { Data = false, ResultType = MessageType.Error, Exception = ex };
                }
            }
        }
        public ReturnObject<List<TEntity>> Find<TEntity>(string commandText, System.Data.CommandType commandType, List<SqlParameter> Parameters = null) where TEntity : class, new()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection == null)
                        return new ReturnObject<List<TEntity>> { Data = null, ResultType = MessageType.Error };

                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    SqlCommand cmd = new SqlCommand(commandText, connection);
                    cmd.CommandType = commandType;

                    if (Parameters != null)
                    {
                        foreach (SqlParameter parameter in Parameters)
                            cmd.Parameters.Add(parameter);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    List<TEntity> result = new List<TEntity>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TEntity entity = new TEntity();
                        foreach (var item in entity.GetType().GetProperties())
                        {
                            string columnName = item.Name;
                            var attr = item.GetCustomAttribute<DatabaseColumn>();
                            if (attr != null && attr.Name != String.Empty)
                                columnName = attr.Name;
                            if (item.PropertyType == dt.Rows[i][columnName].GetType() || dt.Rows[i][columnName] == DBNull.Value)
                                item.SetValue(entity, dt.Rows[i][columnName] != DBNull.Value ? dt.Rows[i][columnName] : null);
                            else
                            {
                                connection.Dispose();
                                connection.Close();
                                return new ReturnErrorObject<List<TEntity>> { Data = result, ResultType = MessageType.Success, Exception = new Exception("Property tipi hatası oluştu. " + item.Name + " property tipi " + item.PropertyType.Name + " fakat " + columnName + " db objesi tipi " + dt.Rows[i][columnName].GetType().Name + " ") };
                            }
                        }
                        result.Add(entity);
                    }

                    cmd.Dispose();
                    connection.Dispose();
                    connection.Close();
                    return new ReturnObject<List<TEntity>> { Data = result, ResultType = MessageType.Success };

                }
                catch (Exception ex)
                {
                    connection.Dispose();
                    connection.Close();
                    return new ReturnErrorObject<List<TEntity>> { Data = null, ResultType = MessageType.Error, Exception = ex };
                }
            }
        }
        public ReturnObject<DataTable> Find(string commandText, System.Data.CommandType commandType, List<SqlParameter> Parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection == null)
                        return new ReturnObject<DataTable> { Data = null, ResultType = MessageType.Error };

                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    SqlCommand cmd = new SqlCommand(commandText, connection);
                    cmd.CommandType = commandType;

                    if (Parameters != null)
                    {
                        foreach (SqlParameter parameter in Parameters)
                            cmd.Parameters.Add(parameter);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cmd.Dispose();
                    connection.Dispose();
                    connection.Close();
                    return new ReturnObject<DataTable> { Data = dt, ResultType = MessageType.Success };

                }
                catch (Exception ex)
                {
                    connection.Dispose();
                    connection.Close();
                    return new ReturnErrorObject<DataTable> { Data = null, ResultType = MessageType.Error, Exception = ex };
                }
            }
        }
        public ReturnObject<TEntity> Get<TEntity>(string commandText, System.Data.CommandType commandType, List<SqlParameter> Parameters = null) where TEntity : class, new()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                try
                {
                    if (connection == null)
                        return new ReturnObject<TEntity> { Data = null, ResultType = MessageType.Error };

                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    SqlCommand cmd = new SqlCommand(commandText, connection);
                    cmd.CommandType = commandType;

                    if (Parameters != null)
                    {
                        foreach (SqlParameter parameter in Parameters)
                            cmd.Parameters.Add(parameter);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    TEntity result = new TEntity();
                    foreach (PropertyInfo item in result.GetType().GetProperties())
                    {
                        string columnName = item.Name;
                        var attr = item.GetCustomAttribute<DatabaseColumn>();
                        if (attr != null && attr.Name != String.Empty)
                            columnName = attr.Name;

                        if (item.PropertyType == dt.Rows[0][columnName].GetType() || dt.Rows[0][columnName] == DBNull.Value)
                            item.SetValue(result, dt.Rows[0][columnName] != DBNull.Value ? dt.Rows[0][columnName] : null);
                        else
                        {
                            connection.Dispose();
                            connection.Close();
                            return new ReturnErrorObject<TEntity> { Data = result, ResultType = MessageType.Success, Exception = new Exception("Property tipi hatası oluştu. " + item.Name + " property tipi " + item.PropertyType.GetType().Name + " fakat " + columnName + " db objesi tipi " + dt.Rows[0][columnName].GetType().Name + " ") };
                        }
                    }

                    cmd.Dispose();
                    connection.Dispose();
                    connection.Close();
                    return new ReturnObject<TEntity> { Data = result, ResultType = MessageType.Success };

                }
                catch (Exception ex)
                {
                    connection.Dispose();
                    connection.Close();
                    return new ReturnErrorObject<TEntity> { Data = null, ResultType = MessageType.Error, Exception = ex };
                }
            }
        }
        public ReturnObject<DataTable> Get(string commandText, System.Data.CommandType commandType, List<SqlParameter> Parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                try
                {
                    if (connection == null)
                        return new ReturnObject<DataTable> { Data = null, ResultType = MessageType.Error };

                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    SqlCommand cmd = new SqlCommand(commandText, connection);
                    cmd.CommandType = commandType;

                    if (Parameters != null)
                    {
                        foreach (SqlParameter parameter in Parameters)
                            cmd.Parameters.Add(parameter);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cmd.Dispose();
                    connection.Dispose();
                    connection.Close();
                    return new ReturnObject<DataTable> { Data = dt, ResultType = MessageType.Success };

                }
                catch (Exception ex)
                {
                    connection.Dispose();
                    connection.Close();
                    return new ReturnErrorObject<DataTable> { Data = null, ResultType = MessageType.Error, Exception = ex };
                }
            }
        }
    }
}