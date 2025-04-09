using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Interfaces.IServices;
using ReservationApi.Models.Entities;
using ReservationApi.Models.Request;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReservationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController(IResevationService resevationService) : ControllerBase
    {
        private readonly IResevationService _reservationService = resevationService;

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult> CreateReservation(ReservationEntity reservation)
        {

            try
            {
                var claims = HttpContext.User.Claims;

                var name = claims.FirstOrDefault(c => c.Type == "Name")?.Value;
                var clientId = claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;

                if (clientId == null || name == null)
                {
                    return Unauthorized(new { message = "Invalid credentials" });
                }
                reservation.Name = name;
                reservation.ClientId = clientId;

                ReservationEntity reservationEntity = await _reservationService.CreateReservation(reservation);

                return Ok(reservationEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("/search")]
        public async Task<ActionResult> GetReservations([FromQuery] FilterRequest filterRequest)
        {

            try
            {
                var claims = HttpContext.User.Claims;
                var role = claims.FirstOrDefault(c => c.Type == "Role")?.Value;

                if (role == "User")
                {
                    var clientId = claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                    filterRequest.IdClient = clientId;
                }

                IEnumerable<ReservationEntity?> reservation = await _reservationService.GetReservations(filterRequest);

                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("/{id}")]
        public async Task<ActionResult> DeleteReservation(Guid id)
        {
            try
            {
                await _reservationService.DeleteReservation(id);

                return Ok("Reserva deletada com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
