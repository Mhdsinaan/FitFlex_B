using System;
using System.Threading.Tasks;
using FitFlex.Application.DTO_s.User_dto;
using FitFlex.Application.Interfaces;
using FitFlex.CommenAPi;
using Microsoft.AspNetCore.Mvc;

namespace FitFlex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authcontroller : ControllerBase
    {
        private readonly IAuth _Auth;
        public Authcontroller(IAuth auth)
        {
            _Auth = auth;
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] RegisterDto dto)
        {
            try
            {
                var exist = await _Auth.GetByUSer(dto.Email);

                if (exist != null)
                {
                    return Conflict(new APiResponds<string>("409", "User already exists", null));
                }

                var register = await _Auth.Register(dto);
                if (register == null)
                    return BadRequest(new APiResponds<string>("400", "Registration failed", null));

                return Ok(new APiResponds<string>("200", "Registration successful", null));
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, new APiResponds<string>("500", "Internal server error", ex.Message));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LOGIN([FromBody] LoginDto dto)
        {
            try
            {
                var token = await _Auth.Login(dto);
                if (token is null)
                    return Unauthorized(new APiResponds<string>("401", "Invalid credentials", null));

                return Ok(new APiResponds<LoginResponseDto>("200", "Login successful", token));
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, new APiResponds<string>("500", "Internal server error", ex.Message));
            }
        }
    }
}
