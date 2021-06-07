using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public interface IPatientBLL
    {
        int AddPatient(List<Patients> patients);
        decimal BMICalculate(decimal height, decimal weight);
    }
}
