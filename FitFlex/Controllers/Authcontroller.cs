using System;
using System.IO;
using System.Threading.Tasks;
using FitFlex.Application.DTO_s.Trainers_dto;
using FitFlex.Application.DTO_s.User_dto;
using FitFlex.Application.Interfaces;
using FitFlex.CommenAPi;
using FitFlex.Domain.Entities.Users_Model;
using FitFlex.Infrastructure.Migrations;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitFlex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authcontroller : ControllerBase
    {
        private readonly IAuth _Auth;
        private readonly ITrainerservice _Trainerauth;
        public Authcontroller(IAuth auth, ITrainerservice Trainerauth)
        {
            _Auth = auth;
            _Trainerauth = Trainerauth;
        }

        [HttpPost("userREgistration")]
        public async Task<IActionResult> Registration([FromBody] RegisterDto dto)
        {
            try
            {
                var user = await _Auth.GetAllAsync();
                var exist = user.FirstOrDefault(p => p.Email == dto.Email);

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
        [HttpPost("Trainer Registration")]
        public async Task<IActionResult> Regi([FromBody] TrainerRegisterDto dto)
        {
            try
            {
                var trainers = await _Auth.GetAllAsync();
                var exist = trainers.FirstOrDefault(p => p.Email == dto.Email);

                if (exist != null)
                {
                    return Conflict(new APiResponds<string>("409", "trainer already exists", null));
                }

                var register = await _Auth.TrainerRegistration(dto);
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
        [HttpGet("byUserId")]
        public async Task<IActionResult> UserByID(int id)
        {
            try
            {
                var user = await _Auth.GetByUSer(id);
                if (user is null) return NotFound(new APiResponds<string>("404", "no user found", null));
                return Ok(new APiResponds<UserResponseDto>("200", "userdeatils", user));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new APiResponds<string>("500", "Internal server error", ex.Message));
            }
        }
        [HttpGet("ByTrainerId")]
        public async Task<IActionResult> TrainerById(int trainerID)
        {
            try
            {
                var user = await _Trainerauth.GetTrainerByIdAsync(trainerID);
                if (user is null) return NotFound(new APiResponds<string>("404", "Trainer Notfount", null));
                return Ok(new APiResponds<TrainerResponseDto>("200", "Trainer Details", user));
            }catch (Exception ex)
            {
                return StatusCode(500, new APiResponds<string>("500", "Internal server error", ex.Message));
            }

        }
       
    }
}
