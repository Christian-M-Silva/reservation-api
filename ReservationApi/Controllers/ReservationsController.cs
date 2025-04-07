using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Interfaces.IServices;
using ReservationApi.Models.Entities;
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
        /*
        // GET: api/<ReservationsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ReservationsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ReservationsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReservationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReservationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
