﻿using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPFC.Services.UnitOfWork;
using OPFC.API.ServiceModel.Order;
using OPFC.Constants;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PayPalController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.ServiceStackHost.Instance.TryResolve<IServiceUow>();

        [HttpPost("CreatePayment")]
        [AllowAnonymous]
        public IActionResult CreatePayment(CreateOrderRequest request)
        {
            try
            {
                var payment = _serviceUow.PaypalService.CreatePayment
                (
                    request,
                    $"{AppSettings.BACKEND_BASE_URL}/Paypal/ExecutePayment",
                    $"{AppSettings.BACKEND_BASE_URL}/PayPal/Cancel",
                    "sale"
                );

                return Created("CreatePayment", new { redirect = payment.links[1].href });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExecutePayment")]
        [AllowAnonymous]
        public IActionResult ExecutePayment([FromQuery(Name = "PayerID")] string PayerID,
                                            [FromQuery(Name = "paymentId")] string paymentId,
                                            [FromQuery(Name = "token")] string token)
        {
			try
			{
                var orderId = _serviceUow.PaypalService.SaveOrderAndExecutePayment(paymentId, PayerID);
                return Redirect($"{AppSettings.FRONTEND_BASE_URL}/profile/event-planner/order/{orderId}");
            }
            catch
			{
                return Redirect($"{AppSettings.FRONTEND_BASE_URL}/error");
			}
        }


        [HttpPost("Refund/{orderLineId}")]
        [AllowAnonymous]
        public IActionResult Refund(long orderLineId)
        {
            try
            {
                _serviceUow.PaypalService.Refund(orderLineId);
                _serviceUow.OrderLineService.Cancel(orderLineId);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
