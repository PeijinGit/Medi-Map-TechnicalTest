using DAL;
using Models;
using System;
using System.Collections.Generic;

namespace Business
{
    public class Patient : IPatientBLL
    {
        public readonly IPatientDAL patientDAL;
        public readonly IErrorBLL errorBLL;

        public Patient(IPatientDAL patientDAL, IErrorBLL errorBLL)
        {
            this.patientDAL = patientDAL;
            this.errorBLL = errorBLL;
        }

        public int AddPatient(List<Models.Patients> patients)
        {
            int addResult = 0;
            if (patients != null)
            {
                foreach (var patient in patients)
                {
                    decimal bmi = BMICalculate(patient.HeightCms, patient.WeightKgs);
                    try
                    {
                        if (patientDAL.AddPatient(patient, bmi) != -1)
                            addResult++;
                    }
                    catch (Exception e)
                    {
                        errorBLL.RecordErrLog(e.Message);
                    }
                }
            }
            else
            {
                Models.InputValueException inputExp = new InputValueException("No patients Received");
                errorBLL.RecordErrLog(inputExp.Message);
                throw inputExp;
            }
            return addResult;
        }

        public decimal BMICalculate(decimal height, decimal weight)
        {

            if (height > 0 && weight > 0)
            {
                decimal heigheM = height / 100;
                decimal bmi = Decimal.Round((weight / (heigheM * heigheM)), 1);
                return bmi;
            }
            else
            {
                Models.InputValueException inputExp = new InputValueException("Height and weight shoutld be greater than 0");
                errorBLL.RecordErrLog(inputExp.Message);
                throw inputExp;
            }

        }
    }
}
