﻿using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController: Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger,
          IMapper mapper ,
          UserManager<StoreUser> userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Get([FromQuery]bool includeItems=true)
        {
            try
            {
                var username = User.Identity.Name;
                var result = _repository.GetAllOrdersByUsername(username, includeItems);
                return Ok(_mapper.Map <IEnumerable< Order>,IEnumerable<OrderViewModel>>(result));
            }
            catch (Exception ex) 
            {

                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }
        //[HttpGet]
        //public IActionResult GetAllOrders()
        //{
        //    try
        //    {
        //        var result = _repository.GetAllOrders();
        //        return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(_repository.GetAllOrders()));
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogError($"Failed to get orders: {ex}");
        //        return BadRequest("Failed to get orders");
        //    }
        //}
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(User.Identity.Name,id);
                if (order != null) return Ok(_mapper.Map<Order,OrderViewModel>(order));
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get order: with id {id} : {ex}");
                return BadRequest($"Failed to get order with id {id}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);
                    if(newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }
                    else
                    {

                    }
                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

                    newOrder.User = currentUser;
                    _repository.AddOrder(newOrder);
                    if (_repository.SaveAll())
                    {
                       
                        return Created($"api/orders{newOrder.Id}",_mapper.Map<Order, OrderViewModel>(newOrder));

                    }
                    else
                    {
                        return BadRequest("Failed to save new order");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new order : {ex}");
                return BadRequest($"Failed to save new order");
            }
        }
    }
}
