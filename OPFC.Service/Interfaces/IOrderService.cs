﻿using System;
using System.Collections.Generic;
using OPFC.API.ServiceModel.Order;
using OPFC.Models;
using OPFC.Repositories.Interfaces;

namespace OPFC.Services.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(CreateOrderRequest orderRequest);
        Order UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        List<Order> GetAllOrder();
        Order GetOrderById(long id);
        List<BrandOrder> GetBrandOrderByBrandId(long brandId);
        List<EventPlannerOrder> GetEventPlannerOrders(long userId);
    }
}
