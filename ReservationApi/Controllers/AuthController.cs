using Microsoft.AspNetCore.Mvc;
using ReservationApi.Interfaces.IServices;
using ReservationApi.Models.Entities;
using ReservationApi.Models.Enuns;
using ReservationApi.Models.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReservationApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IJwtService jwtService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IJwtService _jwtService = jwtService;


        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserRequest registerRequest)
        {
            try
            {
                if (!Enum.IsDefined(typeof(RoleEnum), registerRequest.Role))
                {
                    return BadRequest("Role Unacceptable");
                }
                UserEntity user = await _authService.RegisterAsync(registerRequest);
                return Created("", new {email =  user.Email, name = user.Name});
            }
            catch (Exception ex) { 
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
               UserEntity? user = await _authService.GetUser(loginRequest);

                if (user == null)
                {
                    return StatusCode(403, new { mensagem = "Incorrect email or password." });
                }

                string jwt = _jwtService.GenerateToken(user.Role, user.Email, user.Id);
                string refreshToken = _jwtService.GenerateRefreshToken(user.Email);

                return Ok(new {jwt, refreshToken});
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /* GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
