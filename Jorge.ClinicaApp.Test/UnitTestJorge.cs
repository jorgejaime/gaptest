using Jorge.ClinicaApp.Infrastructure.Messaging;
using Jorge.ClinicaApp.Infrastructure.UnitOfWork;
using Jorge.ClinicaApp.Model.DomainModels;
using Jorge.ClinicaApp.Model.Repositories;
using Jorge.ClinicaApp.Repository;
using Jorge.ClinicaApp.Services;
using Jorge.ClinicaApp.Services.Implementatios;
using Jorge.ClinicaApp.Services.Interfaces;
using Jorge.ClinicaApp.Services.Messaging.Appointment;
using Jorge.ClinicaApp.Services.VieModels.Appointment;
using Jorge.ClinicaApp.Services.VieModels.Patient;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jorge.ClinicaApp.Test
{
    [TestClass]
    public class UnitTestJorge
    {

        public UnitTestJorge()
        {
           
        }


        [TestMethod]
        public void TesGetAllPatients()
        {

            AutoMapperBootStrapper.ConfigureAutoMapper();

            var mockRepo = new Mock<IPatientRepository>();
            var mockUO = new Mock<IUnitOfWork>();
            var mockLog = new Mock<ILogger<PatientService>>();

            mockRepo.Setup(p => p.GetAll(null)).Returns(new List<Patient> { new Patient (), new Patient() });
     

            IPatientService servicce = new PatientService(mockRepo.Object, mockUO.Object, mockLog.Object);
            var result = servicce.GetAll(new ContractRequest<BaseRequest>());
            Assert.AreNotEqual(null, result.Data);
            Assert.AreEqual(true, result.Data.Any());
        }


        [TestMethod]
        public void TesAddAppointmentExtemporanea()
        {
            AutoMapperBootStrapper.ConfigureAutoMapper();

            var mockRepoPatient = new Mock<IPatientRepository>();
            var mockRepoAppointment = new Mock<IAppointmentRepository>();
            var mockUO = new Mock<IUnitOfWork>();
            var mockLog = new Mock<ILogger<AppointmentService>>();


          
            mockRepoAppointment.Setup(p => p.Add(new Appointment()));


            IAppointmentService servicce = new AppointmentService(mockRepoAppointment.Object, mockUO.Object, mockLog.Object, mockRepoPatient.Object);
            var result = servicce.Add(new ContractRequest<AddUpdateAppointmentRequest> {
                Data = new AddUpdateAppointmentRequest
                {
                    Appointment = new AppointmentView
                    {
                        AppointmentDate = DateTime.Now,
                        AppointmentTypeId = 1,
                        PatientId = 1

                    }
                }
            });
            Assert.AreEqual(true, result.ErrorMessages.Any());
            Assert.AreEqual("Las citas se deben agendar con mínimo 24 horas de antelación.", result.ErrorMessages[0]);
            


        }

        [TestMethod]
        public void TesAddAppointmentSameDate()
        {
            AutoMapperBootStrapper.ConfigureAutoMapper();

            var mockRepoPatient = new Mock<IPatientRepository>();
            var mockRepoAppointment = new Mock<IAppointmentRepository>();
            var mockUO = new Mock<IUnitOfWork>();
            var mockLog = new Mock<ILogger<AppointmentService>>();


            var appointment = new Appointment
            {
                AppointmentDate = DateTime.Now,
                PatientId = 1

            };

            mockRepoAppointment.Setup(a => a.IsAppointmentInSameDay(appointment)).Returns(true);
            mockRepoAppointment.Setup(p => p.Add(appointment));


            IAppointmentService servicce = new AppointmentService(mockRepoAppointment.Object, mockUO.Object, mockLog.Object, mockRepoPatient.Object);
            var result = servicce.Add(new ContractRequest<AddUpdateAppointmentRequest>
            {
                Data = new AddUpdateAppointmentRequest
                {
                    Appointment = new AppointmentView
                    {
                        AppointmentDate = DateTime.Now.AddDays(3),
                        AppointmentTypeId = 1,
                        PatientId = 1

                    }
                }
            });
            Assert.AreEqual(true, result.ErrorMessages.Any());
            Assert.AreEqual("Las citas se deben agendar con mínimo 24 horas de antelación.", result.ErrorMessages[0]);



        }


        [TestMethod]
        public void TesAddAppointment()
        {
            AutoMapperBootStrapper.ConfigureAutoMapper();

            var mockRepoPatient = new Mock<IPatientRepository>();
            var mockRepoAppointment = new Mock<IAppointmentRepository>();
            var mockUO = new Mock<IUnitOfWork>();
            var mockLog = new Mock<ILogger<AppointmentService>>();



            mockRepoAppointment.Setup(p => p.Add(new Appointment()));


            IAppointmentService servicce = new AppointmentService(mockRepoAppointment.Object, mockUO.Object, mockLog.Object, mockRepoPatient.Object);
            var result = servicce.Add(new ContractRequest<AddUpdateAppointmentRequest>
            {
                Data = new AddUpdateAppointmentRequest
                {
                    Appointment = new AppointmentView
                    {
                        AppointmentDate = DateTime.Now.AddDays(3),
                        AppointmentTypeId = 1,
                        PatientId = 1

                    }
                }
            });
            Assert.AreNotEqual(null, result.Data);
            Assert.AreEqual(true, result.Data.Any());
            Assert.AreEqual(false, result.ErrorMessages.Any());



        }
    }
}
