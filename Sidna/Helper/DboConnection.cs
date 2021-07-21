using Sidna.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sidna.Helper
{
    public class DboConnection
    {
        public async Task<Result<IEnumerable<T>>> Read<T>(string query)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SqlConnection(Property.ConnectionString))
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable tbl1 = new DataTable();
                        adapter.Fill(tbl1);
                        var tblList = tbl1.ToList<T>();
                        return Result<IEnumerable<T>>.Successful(data: tblList);
                    }
                }
                catch (Exception ex)
                {
                    return Result<IEnumerable<T>>.Failure(error: true, message: ex.ToString());
                }
            });
        }

        public async Task<Result> Write(string query,List<SqlParameter> sqlParameter = null)
        {
            //return Result.Successful();

            return await Task.Run(() =>
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(Property.ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.CommandTimeout = 9999;
                            if (sqlParameter != null)
                                foreach (var p in sqlParameter)
                                    command.Parameters.Add(p);
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                            return Result.Successful();

                        }
                    }
                }
                catch (Exception ex) //error occurred
                {
                    return Result.Failure(error: true, message: ex.ToString());
                }

            });
        }
        public async Task<Result> spWrite(string spName, List<SqlParameter> sqlParameter = null)
        {
            //return Result.Successful();

            return await Task.Run(() =>
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(Property.ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand(spName, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 9999;
                            if (sqlParameter != null)
                                foreach (var p in sqlParameter)
                                    command.Parameters.Add(p);
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                            return Result.Successful();

                        }
                    }
                }
                catch (Exception ex) //error occurred
                {
                    return Result.Failure(error: true, message: ex.ToString());
                }

            });
        }


    }
}
