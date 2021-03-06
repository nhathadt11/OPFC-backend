﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Order;
using OPFC.Models;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpPost]
        public ActionResult Create(CreateOrderRequest orderRequest)
        {
            try
            {
                Order created = _serviceUow.OrderService.CreateOrder(orderRequest);
                return Created("/Order", created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var orders = _serviceUow.OrderService.GetAllOrder();

            return Ok(Mapper.Map<List<OrderDTO>>(orders));

        }

        [HttpGet("OrderLine/{id}")]
        public ActionResult GetAllOrderLine(string id)
        {
            if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, "^\\d+$"))
                return BadRequest(new { Message = "Invalid Id" });

            var orderLines = _serviceUow.OrderLineService.GetAllByOrderId(long.Parse(id));

            return Ok(Mapper.Map<List<OrderLine>>(orderLines));

        }

        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {

            if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, "^\\d+$"))
                return BadRequest(new { Message = "Invalid Id" });

            var order = _serviceUow.OrderService.GetOrderById(long.Parse(id));
            if (order == null)
            {
                return NotFound(new { Message = "Could not find Order" });
            }
            return Ok(Mapper.Map<OrderDTO>(order));
        }
        
        [HttpGet("Brand/{brandId}")]
        public ActionResult GetBrandOrders(long brandId)
        {
            try
            {
                var brandOrderList = _serviceUow.OrderService.GetBrandOrderByBrandId(brandId);
                return Ok(brandOrderList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("EventPlanner/User/{userId}")]
        public ActionResult GetEventPlannerOrders(long userId)
        {
            try
            {
                var eventPlannerOrderList = _serviceUow.OrderService.GetEventPlannerOrders(userId);
                return Ok(eventPlannerOrderList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("EventPlanner/{orderId}")]
        public ActionResult GetEventPlannerOrder(long orderId)
        {
            try
            {
                var orderExists = _serviceUow.OrderService.Exits(orderId);
                if (!orderExists)
                {
                    return NotFound("Order could be found.");
                }
    
                var eventPlannerOrder = _serviceUow.OrderService.GetEventPlannerOrderById(orderId);
    
                return Ok(eventPlannerOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("Brand/Approve/{orderLineId}")]
        public ActionResult ApproveBrandOrder(long orderLineId)
        {
            try
            {
                var orderLineExists = _serviceUow.OrderLineService.Exists(orderLineId);
                if (!orderLineExists)
                {
                    return NotFound("Order could not be found.");
                }

                _serviceUow.OrderLineService.Approve(orderLineId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("MarkAsCompleted/{orderLineId}")]
        public ActionResult MarkAsCompletedService(long orderLineId)
        {
            try
            {
                var orderLineExists = _serviceUow.OrderLineService.Exists(orderLineId);
                if (!orderLineExists)
                {
                    return NotFound("Order Line could not be found.");
                }

                _serviceUow.OrderLineService.MarkAsCompleted(orderLineId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("MarkAsIncompleted/{orderLineId}")]
        public ActionResult MarkAsIncompletedService(long orderLineId)
        {
            try
            {
                var orderLineExists = _serviceUow.OrderLineService.Exists(orderLineId);
                if (!orderLineExists)
                {
                    return NotFound("Order Line could not be found.");
                }

                _serviceUow.OrderLineService.MarkAsIncompleted(orderLineId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}