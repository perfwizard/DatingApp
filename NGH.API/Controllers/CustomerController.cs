using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGH.API.Controllers.Resources;
using NGH.API.DataAccess.Repositories;
using NGH.API.DataAccess.UnitOfWork;
using NGH.API.Helpers;
using NGH.API.Models;

namespace NGH.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IUnitOfWork _uom;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepo, IUnitOfWork uom, IMapper mapper) : base()
        {
            _mapper = mapper;
            _customerRepo = customerRepo;
            _uom = uom;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_customerRepo.IsCustomerCodeUnique(customer.CustomerCode))
                return BadRequest("Customer Code already exists.");

            customer.CreateDate = DateTime.Now;
            customer.CreateBy = "Test"; //User.FindFirst(ClaimTypes.Name).Value;

            _customerRepo.AddAsync(customer);
            await _uom.CommitAsync();

            return Created("Customer was created.", _mapper.Map<CustomerResource>(customer));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_customerRepo.IsCustomerCodeUnique(customer.CustomerCode))
                return BadRequest("Customer Code already exists.");

            customer.UpdateDate = DateTime.Now;
            customer.UpdateBy = "Test"; // User.FindFirst(ClaimTypes.Name).Value;

            _customerRepo.Update(customer);
            await _uom.CommitAsync();

            return Ok(_mapper.Map<CustomerResource>(customer));
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var customers = await _customerRepo.GetAllAsync();

            var customerList = _mapper.Map<List<CustomerResource>>(customers);
            return Ok(customerList);
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerList([FromQuery]CustomerParams cond)
        {
            var customers = await _customerRepo.GetCustomers(cond);

            var customerList = _mapper.Map<List<CustomerResource>>(customers);

            Response.AddPagination(
                customers.CurrentPage, customers.PageSize, customers.TotalCount, customers.TotalPages);

            return Ok(customerList);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerRepo.GetByIdAsync(id);
            return Ok(customer);
        }
        [HttpDelete("{id}")]
        public async void DeleteCustomer(int id)
        {
            var cus = await _customerRepo.GetByIdAsync(id);
            
            cus.UpdateDate = DateTime.Now;
            cus.UpdateBy = User.FindFirst(ClaimTypes.Name).Value;
            cus.Deleted = true;

            _customerRepo.Update(cus);
            await _uom.CommitAsync();
        }

    }
}