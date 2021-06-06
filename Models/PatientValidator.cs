using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class PatientValidator : AbstractValidator<Patients>
    {
        public PatientValidator()
        {
            RuleFor(patient => patient.PatientID).NotNull();
            RuleFor(patient => patient.FirstName).NotNull();
            RuleFor(patient => patient.LastName).NotNull();
            RuleFor(patient => patient.Gender).NotNull();
            RuleFor(patient => patient.DOB).NotNull();
            RuleFor(patient => patient.HeightCms).NotNull().GreaterThan(0);
            RuleFor(patient => patient.WeightKgs).NotNull().GreaterThan(0);
            RuleFor(patient => patient.MedicationID).NotNull();
            RuleFor(patient => patient.MedicationName).NotNull();
        }
    }
}
