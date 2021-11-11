﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PricingCalculator.Handlers;
using PricingCalculator.Models;
using PricingCalculator.Services;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PricingCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingServiceController : ControllerBase
    {
        /// <summary>
        /// Referens till en service där man hämtar information om en customer
        /// </summary>
        private readonly ICustomerService m_CustomerService;

        /// <summary>
        /// Referens till en service där man beräknar kostnaden
        /// </summary>
        private readonly IPriceCalculateService m_PriceCalculateService;

        /// <summary>
        /// Referens till ett objekt där man kan hämta information från ett customer objekt
        /// </summary>
        private readonly ICustomerHandler m_CustomerHandler;


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="customerService">Referens till en service där man hämtar information om en customer</param>
        /// <param name="priceCalculateService">Referens till en service där man beräknar kostnaden</param>
        /// <param name="customerHandler">Referens till ett objekt där man kan hämta information från ett customer objekt</param>
        public PricingServiceController(ICustomerService customerService, IPriceCalculateService priceCalculateService, ICustomerHandler customerHandler)
        {
            this.m_CustomerService = customerService;
            this.m_PriceCalculateService = priceCalculateService;
            this.m_CustomerHandler = customerHandler;
        }


        /// <summary>
        /// Action för Service A
        /// </summary>
        /// <returns>Pris</returns>        
        /// <response code="200">Ok och priset returneras</response>
        /// <response code="403">Returneras om customer inte kan använda servisen</response>
        /// <response code="404">Returneras om customer med sökt customer id inte finns</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [HttpGet("ServiceA/{customerId}/{startDate}/{endDate}")]
        public async Task<ActionResult<string>> ServiceA(int customerId, DateTime startDate, DateTime endDate)
        {
            m_CustomerService.CreateCustomers();
            Customer customer = m_CustomerService.GetCustomer(customerId);
            if (customer == null)
                return NotFound($"Hittade inte customer med id {customerId}");


            if (m_CustomerHandler.CanUseService(CallingService.SERVICE_A, customer))
            {                
                double price = m_PriceCalculateService.CalculatePrice(CallingService.SERVICE_A, customer, startDate, endDate);
                return Ok(price.ToString());
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
        }


        /// <summary>
        /// Action för Service B
        /// </summary>
        /// <returns>Pris</returns>        
        /// <response code="200">Ok och priset returneras</response>
        /// <response code="403">Returneras om customer inte kan använda servisen</response>
        /// <response code="404">Returneras om customer med sökt customer id inte finns</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [HttpGet("ServiceB/{customerId}/{startDate}/{endDate}")]
        public async Task<ActionResult<string>> ServiceB(int customerId, DateTime startDate, DateTime endDate)
        {
            m_CustomerService.CreateCustomers();
            Customer customer = m_CustomerService.GetCustomer(customerId);
            if (customer == null)
                return NotFound($"Hittade inte customer med id {customerId}");


            if (m_CustomerHandler.CanUseService(CallingService.SERVICE_B, customer))
            {                
                double price = m_PriceCalculateService.CalculatePrice(CallingService.SERVICE_B, customer, startDate, endDate);
                return Ok(price.ToString());
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
        }


        /// <summary>
        /// Action för Service C
        /// </summary>
        /// <returns>Pris</returns>        
        /// <response code="200">Ok och priset returneras</response>
        /// <response code="403">Returneras om customer inte kan använda servisen</response>
        /// <response code="404">Returneras om customer med sökt customer id inte finns</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [HttpGet("ServiceC/{customerId}/{startDate}/{endDate}")]
        public async Task<ActionResult<string>> ServiceC(int customerId, DateTime startDate, DateTime endDate)
        {
            m_CustomerService.CreateCustomers();
            Customer customer = m_CustomerService.GetCustomer(customerId);
            if (customer == null)
                return NotFound($"Hittade inte customer med id {customerId}");


            if (m_CustomerHandler.CanUseService(CallingService.SERVICE_C, customer))
            {                
                double price = m_PriceCalculateService.CalculatePrice(CallingService.SERVICE_C, customer, startDate, endDate);
                return Ok(price.ToString());
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
        }
    }
}