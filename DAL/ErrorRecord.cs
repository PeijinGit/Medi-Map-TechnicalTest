using Microsoft.Extensions.Options;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class ErrorRecord : BaseDAL, IErrorDAL
    {
        public ErrorRecord(IOptions<AppSettingModels> appSettings) : base(appSettings){}

        public void RecordErrLog(string msg)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("uspLogError", connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@ErrorMessage", msg));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

