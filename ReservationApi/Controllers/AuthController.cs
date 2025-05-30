﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Interfaces.IServices;
using ReservationApi.Models.Entities;
using ReservationApi.Models.Enuns;
using ReservationApi.Models.Request;
using ReservationApi.Models.Shared;

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
        public async Task<ActionResult> Login(LoginUserRequest loginRequest)
        {
            try
            {
               UserEntity? user = await _authService.GetUserAsync(loginRequest);

                if (user == null)
                {
                    return StatusCode(403, new { mensagem = "Incorrect email or password." });
                }

                string jwt = _jwtService.GenerateToken(user.Role, user.Name, user.Id);
                RefreshTokenModel refreshToken = await _jwtService.GenerateRefreshToken(user.Email);

                return Ok(new {jwt, refreshToken});
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("refresh-token/{id}")]
        public async Task<ActionResult> RefreshToken(Guid id, string refreshToken)
        {
            try
            {
               UserEntity? user = await _jwtService.ValidateRefreshToken(id, refreshToken);

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid refresh token" });
                }
                string jwt = _jwtService.GenerateToken(user.Role, user.Name, user.Id);

                return Ok(new { jwt });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
