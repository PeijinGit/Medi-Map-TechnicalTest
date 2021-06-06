using Business;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using Utility;

namespace TechnicalTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Patient : ControllerBase
    {
        public readonly IPatientBLL patientBLL;
        public readonly IErrorBLL errorBLL;

        public Patient(IPatientBLL patientBLL, IErrorBLL errorBLL)
        {
            this.patientBLL = patientBLL;
            this.errorBLL = errorBLL;
        }

        [HttpPost]
        public ResponseModel Post([FromBody] List<PatientDetails> value)
        {
            int addResult = 0;
            if (value.Count != 0)
            {
                List<Models.Patients> patients = ModelTransfer.VoToDto(value);
                List<Models.Patients> validatedPatients = new List<Patients>();
                PatientValidator validator = new PatientValidator();
                patients.ForEach(patient =>
                {
                    try
                    {
                        validator.ValidateAndThrow(patient);
                        validatedPatients.Add(patient);
                    }
                    catch (Exception e)
                    {
                        errorBLL.RecordErrLog(e.ToString());
                    }

                });
                addResult = patientBLL.AddPatient(validatedPatients);
                HttpContext.Response.StatusCode = 200;
                return new ResponseModel { Status = 235, ResponseMsg = String.Format("Received {0}, Added {1} ", value.Count, addResult) };
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
                return new ResponseModel { Status = -1, ResponseMsg = "No data received" };
            }
        }
    }
}
