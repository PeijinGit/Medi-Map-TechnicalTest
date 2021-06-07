using System;
using Xunit;
using System.Collections.Generic;
using Moq;
using Models;
using Business;
using DAL;

namespace Tests
{
    public class PatientTest
    {
        [Fact]
        public void BMICalculate_InputHeightAndWeight_ReturnBMI()
        {
            //Arrange
            var iPatientDAL = new Mock<IPatientDAL>();
            var iErrorBLL = new Mock<IErrorBLL>();
            var patient = new Business.Patient(iPatientDAL.Object, iErrorBLL.Object);

            //Action
            var result = patient.BMICalculate(150, 60);

            //Assert
            Assert.Equal<decimal>(26.7m, result);
        }

        [Theory]
        [InlineData(170, 0)]
        [InlineData(0, 60)]
        [InlineData(0, 0)]
        [InlineData(-1, 0)]
        [InlineData(-1, -1)]
        public void BMICalculate_InputInvalidValue_ThrowException(int height, int weight)
        {
            //Arrange
            var iPatientDAL = new Mock<IPatientDAL>();
            var iErrorBLL = new Mock<IErrorBLL>();
            var patient = new Business.Patient(iPatientDAL.Object, iErrorBLL.Object);

            //Action
            Action act = () => patient.BMICalculate(height, weight);

            //Assert
            Assert.Throws<InputValueException>(act);
        }


        [Fact]
        public void AddPatient_InputPatientList_ReturnAddedCount()
        {
            //Arrange
            var mock = new Mock<IPatientDAL>();
            var mock2 = new Mock<IErrorBLL>();
            int expected = 2;
            List<Patients> testPatients = new List<Patients>()
            {
                new Patients{
                    PatientID=1,
                    FirstName ="FirstName",
                    LastName="LastName",
                    Gender="Gender",
                    DOB=DateTime.Now,
                    HeightCms=180,
                    WeightKgs=70,
                    MedicationID=1,
                    MedicationName="Test Medication"
                },
                new Patients{
                    PatientID=2,
                    FirstName ="FirstName",
                    LastName="LastName",
                    Gender="Gender",
                    DOB=DateTime.Now,
                    HeightCms=182,
                    WeightKgs=65,
                    MedicationID=1,
                    MedicationName="Test Medication2"
                },

            };
            mock.Setup(foo => foo.AddPatient(It.IsAny<Patients>(), It.IsAny<Decimal>())).Returns(1);
            IPatientBLL patientBLL = new Business.Patient(mock.Object, mock2.Object);

            //Action
            int actual = patientBLL.AddPatient(testPatients);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null)]
        public void AddPatient_InputNullPatientList_ThrowException(List<Models.Patients> patients)
        {
            //Arrange
            var iPatientDALMock = new Mock<IPatientDAL>();
            var iErrorBLLMock = new Mock<IErrorBLL>();
            iPatientDALMock.Setup(foo => foo.AddPatient(It.IsAny<Patients>(), It.IsAny<Decimal>())).Returns(-1);
            iErrorBLLMock.Setup(foo => foo.RecordErrLog(It.IsAny<string>()));

            //Action
            IPatientBLL patientBLL = new Business.Patient(iPatientDALMock.Object, iErrorBLLMock.Object);
            Action act = () => patientBLL.AddPatient(patients);

            //Assert
            Assert.Throws<InputValueException>(act);
        }

    }
}
