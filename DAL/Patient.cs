using Microsoft.Extensions.Options;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class Patient : BaseDAL, IPatientDAL
    {
        public Patient(IOptions<AppSettingModels> appSettings) : base(appSettings)
        {
        }

        public int AddPatient(Patients patient, decimal bmi)
        {
            int effectRow = -1;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction tran = connection.BeginTransaction();
                using (var command = new SqlCommand("uspPatientManage", connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    command.Transaction = tran;
                    command.Parameters.Add(new SqlParameter("@PatientId", patient.PatientID));
                    command.Parameters.Add(new SqlParameter("@FirstName", patient.FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", patient.LastName));
                    command.Parameters.Add(new SqlParameter("@Gender", patient.Gender));
                    command.Parameters.Add(new SqlParameter("@DOB", patient.DOB));
                    command.Parameters.Add(new SqlParameter("@HeightCms", patient.HeightCms));
                    command.Parameters.Add(new SqlParameter("@WeightKgs", patient.WeightKgs));
                    command.ExecuteNonQuery();

                    command.CommandText = "uspMedicationAdministration";
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@PatientId", patient.PatientID));
                    command.Parameters.Add(new SqlParameter("@BMI", bmi));
                    command.Parameters.Add(new SqlParameter("@MedicationId", patient.MedicationID));
                    effectRow = command.ExecuteNonQuery();
                    tran.Commit();
                }
            }
            return effectRow;
        }

    }
}

