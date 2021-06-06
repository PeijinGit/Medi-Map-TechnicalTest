using Models;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IPatientDAL
    {
        int AddPatient(Patients patient, decimal bmi);

    }
}
