using System;
using System.Data;
using System.Data.SqlClient;

namespace CFMStats.Classes
{
    public class StoredProc
    {
        public string DataConnectionString { get; set; }

        public bool IsSqlCommand { get; set; }

        public string Name { get; set; }

        public SqlCommand ParameterSet { get; set; }

        /// <summary>
        ///     use this for updates/deletes/inserts
        /// </summary>
        public static bool NonQuery(StoredProc sp)
        {
            var isSuccessful = false;

            try
            {
                using (var conn = new SqlConnection(sp.DataConnectionString))
                {
                    using (var command = new SqlCommand(sp.Name, conn))
                    {
                        conn.Open();

                        if (sp.IsSqlCommand == false)
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            foreach (SqlParameter item in sp.ParameterSet.Parameters)
                            {
                                command.Parameters.AddWithValue(item.ParameterName, item.Value);
                            }
                        }

                        var da = new SqlDataAdapter(command);

                        command.ExecuteNonQuery();

                        isSuccessful = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error {ex.Message}");

                isSuccessful = false;
            }

            return isSuccessful;
        }

        public static int ReturnInteger(StoredProc sp)
        {
            var id = 0;

            using (var conn = new SqlConnection(sp.DataConnectionString))
            {
                using (var command = new SqlCommand(sp.Name, conn))
                {
                    conn.Open();

                    if (sp.IsSqlCommand == false)
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        foreach (SqlParameter item in sp.ParameterSet.Parameters)
                        {
                            command.Parameters.AddWithValue(item.ParameterName, item.Value).Direction = item.Direction;
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }

            return id;
        }

        /// <summary>
        ///     Pass the stored proc object to retrieve data
        /// </summary>
        public static DataSet ShowMeTheData(StoredProc SP)
        {
            var myData = new DataSet();

            try
            {
                using (var conn = new SqlConnection(SP.DataConnectionString))
                {
                    using (var command = new SqlCommand(SP.Name, conn))
                    {
                        conn.Open();

                        if (!SP.IsSqlCommand)
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            foreach (SqlParameter item in SP.ParameterSet.Parameters)
                            {
                                command.Parameters.AddWithValue(item.ParameterName, item.Value);
                            }
                        }

                        var da = new SqlDataAdapter(command);

                        da.Fill(myData);
                    }
                }
            }
            catch (Exception ex)
            {
                var dt = new DataTable();
                dt.Columns.Add("kjdStatus", typeof(string));
                dt.Columns.Add("kjdMessage", typeof(string));
                dt.Rows.Add("Error", ex.Message);

                myData.Tables.Add(dt);
            }

            return myData;
        }
    }
}