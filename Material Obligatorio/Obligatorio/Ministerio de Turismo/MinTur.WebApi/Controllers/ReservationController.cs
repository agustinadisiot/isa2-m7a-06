using System;
using System.Collections.Generic;
using System.Linq;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.Domain.BusinessEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MinTur.Models.In;
using MinTur.Models.Out;
using MinTur.WebApi.Filters;
using MinTur.Domain.Reports;

namespace MinTur.WebApi.Controllers
{
    [EnableCors("AllowEverything")]
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationManager _reservationManager;

        public ReservationController(IReservationManager reservationManager)
        {
            _reservationManager = reservationManager;
        }

        [HttpGet]
        [ServiceFilter(typeof(AdministratorAuthorizationFilter))]
        public IActionResult GetAll()
        {
            List<Reservation> retrievedReservations = _reservationManager.GetAllReservations();
            List<ReservationDetailsModel> reservationDetails = retrievedReservations.Select(reservation => new ReservationDetailsModel(reservation)).ToList();
            return Ok(reservationDetails);
        }

        [HttpGet("{id:Guid}")]
        [ServiceFilter(typeof(AdministratorAuthorizationFilter))]
        public IActionResult GetSpecificReservation(Guid id)
        {
            Reservation retrievedReservation = _reservationManager.GetReservationById(id);
            ReservationDetailsModel reservationDetailsModel = new ReservationDetailsModel(retrievedReservation);
            return Ok(reservationDetailsModel);
        }

        [HttpGet("{id:Guid}/state")]
        public IActionResult GetReservationState(Guid id)
        {
            Reservation retrievedReservation = _reservationManager.GetReservationById(id);
            ReservationCheckStateModel reservationCheckStateModel = new ReservationCheckStateModel(retrievedReservation);
            return Ok(reservationCheckStateModel);
        }

        [HttpPost]
        public IActionResult MakeReservation([FromBody] ReservationIntentModel reservationIntentModel)
        {
            Reservation createdReservation = _reservationManager.RegisterReservation(reservationIntentModel.ToEntity());
            ReservationConfirmationModel confirmation = new ReservationConfirmationModel(createdReservation);
            return Created("api/reservations/" + createdReservation.Id, confirmation);
        }

        [HttpPut("{id:Guid}/state")]
        [ServiceFilter(typeof(AdministratorAuthorizationFilter))]
        public IActionResult UpdateReservationState(Guid id, [FromBody] ReservationStateIntentModel reservationStateIntentModel)
        {
            Reservation updatedReservation = _reservationManager.UpdateReservationState(id, reservationStateIntentModel.ToEntity());
            ReservationCheckStateModel reservationCheckStateModel = new ReservationCheckStateModel(updatedReservation);
            return Ok(reservationCheckStateModel);
        }


        [HttpGet]
        [Route("generateReport")]
        [ServiceFilter(typeof(AdministratorAuthorizationFilter))]
        public IActionResult GenerateReport([FromQuery] ReservationReportInputModel reportInput)
        {
            ReservationReport generatedReport = _reservationManager.GenerateReservationReport(reportInput.ToEntity());
            List<ReservationIndividualReportModel> individualReportsModel = generatedReport.ReservationPerResort.
                Select(r => new ReservationIndividualReportModel(r)).ToList();
            return Ok(individualReportsModel);
        }
    }
}
