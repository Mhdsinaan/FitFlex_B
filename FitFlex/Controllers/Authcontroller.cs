﻿using FitFlex.Application.DTO_s.Trainers_dto;
using FitFlex.Application.DTO_s.User_dto;
using FitFlex.Application.Interfaces;
using FitFlex.Application.services;
using FitFlex.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuth _Auth;
    private readonly ITrainerservice _Trainerauth;

    public AuthController(IAuth auth, ITrainerservice trainerAuth)
    {
        _Auth = auth;
        _Trainerauth = trainerAuth;
    }

    [HttpPost("userRegistration")]
    public async Task<IActionResult> Registration([FromBody] RegisterDto dto)
    {
        var user = await _Auth.Register(dto);
        if (user == null) return NotFound(user);

        return Ok(user);
    }

    [HttpPost("trainerRegistration")]
    public async Task<IActionResult> TrainerRegistration([FromBody] TrainerRegisterDto dto)
    {
        var trainer = await _Auth.TrainerRegistration(dto);
        if (trainer == null) return NotFound(trainer);

        return Ok(trainer);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _Auth.Login(dto);
        if (token == null) return Unauthorized();

        return Ok(token);
    }

    [HttpGet("byUserId/{id}")]
    public async Task<IActionResult> UserByID(int id)
    {
        var user = await _Auth.GetByUser(id);
        if (user == null) return NotFound(user);

        return Ok(user);
    }

    [HttpGet("byTrainerId/{trainerId}")]
    public async Task<IActionResult> TrainerById(int trainerId)
    {
        var trainer = await _Trainerauth.GetTrainerByIdAsync(trainerId);
        if (trainer == null) return NotFound(trainer);

        return Ok(trainer);
    }

    [HttpDelete("deleteTrainer/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteTrainer(int id)
    {
        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var trainer = await _Trainerauth.DeleteTrainerAsync(id, currentUserId);

        if (trainer == null) return NotFound(trainer);

        return Ok(trainer);
    }

    [HttpPut("{trainerId}/status")] 
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateTrainerStatus(int trainerId, [FromQuery] TrainerStatus newStatus)
    {
        var result = await _Trainerauth.UpdateTrainerStatusAsync(trainerId, newStatus);

        if (result == null)
            return NotFound(result);

        return Ok(result);
    }

}   