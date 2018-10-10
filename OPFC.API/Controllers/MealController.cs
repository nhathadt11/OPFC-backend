﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Meal;
using OPFC.Models;
using OPFC.Services.UnitOfWork;
using System;
using System.Collections.Generic;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpPost]
        public IActionResult Create(CreateMealRequest request)
        {
            try
            {
                var meal = Mapper.Map<Meal>(request.Meal);
                var created = Mapper.Map<MealDTO>(_serviceUow.MealService.CreateMeal(meal));

                return Created("/Meal", created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var meals = _serviceUow.MealService.GetAllMeal();
                return Ok(Mapper.Map<List<MealDTO>>(meals));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                var found = _serviceUow.MealService.GetMealById(id);
                if (found == null)
                {
                    return NotFound("Meal could not be found.");
                }
                
                return Ok(Mapper.Map<MealDTO>(found));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, UpdateMealRequest request)
        {
            try
            {
                var found = _serviceUow.MealService.GetMealById(id);
                if (found == null)
                {
                    return NotFound("Meal could not be found.");
                }

                var meal = Mapper.Map<Meal>(request.Meal);
                return Ok(Mapper.Map<MealDTO>(_serviceUow.MealService.UpdateMeal(meal)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var found = _serviceUow.MealService.GetMealById(id);
                if (found == null)
                {
                    return NotFound("Meal could not be found.");
                }

                _serviceUow.MealService.DeleteMeal(found);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}