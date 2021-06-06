using Models;
using System;
using System.Collections.Generic;

namespace Utility
{
    public class ModelTransfer
    {
        public static List<Patients> VoToDto(List<PatientDetails> patientDetails)
        {
            List<Patients> patients = new List<Patients>();
            patientDetails.ForEach(x =>
            {
                int.TryParse(x.PatientID, out int id);
                decimal.TryParse(x.HeightCms, out decimal height);
                decimal.TryParse(x.WeightKgs, out decimal weight);
                DateTime.TryParse(x.DOB, out DateTime dob);
                int.TryParse(x.MedicationID, out int medId);
                patients.Add(new Patients
                {
                    PatientID = id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Gender = x.Gender,
                    DOB = dob,
                    HeightCms = height,
                    WeightKgs = weight,
                    MedicationID = medId,
                    MedicationName = x.MedicationName
                });
            });
            return patients;
        }
    }
}
