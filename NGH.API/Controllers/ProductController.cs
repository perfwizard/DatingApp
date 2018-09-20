using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGH.API.Controllers.Resources;
using NGH.API.DataAccess.Repositories;
using NGH.API.DataAccess.UnitOfWork;
using NGH.API.Helpers;
using NGH.API.Models;

namespace NGH.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly IUnitOfWork _uom;
        private readonly IMapper _mapper;
        private readonly IProductDiscountRepository _productDiscountRepo;

        public ProductController(IProductRepository productRepo, IProductDiscountRepository productDiscountRepo,
            IUnitOfWork uom, IMapper mapper)
        {
            _productDiscountRepo = productDiscountRepo;
            _mapper = mapper;
            _uom = uom;
            _productRepo = productRepo;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var product = await _productRepo.GetAllAsync();

            return Ok(product);
        }
        [HttpGet]
        public async Task<IActionResult> Get(ProductParams param)
        {
            var product = await _productRepo.GetProductList(param);

            Response.AddPagination(product.CurrentPage, product.PageSize,
                product.TotalCount, product.TotalPages);

            return Ok(product);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepo.GetIncludingAsync(p => p.Id == id, p => p.ProductDiscounts);

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]Product prod)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            prod.CreateDate = prod.UpdateDate = DateTime.Now;
            prod.CreateBy = prod.UpdateBy = "Test"; // User.FindFirst(ClaimTypes.Name).Value;

            _productRepo.AddAsync(prod);

            await _uom.CommitAsync();

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody]Product prod)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(_productRepo.IsProductCodeUnique(prod.ProductCode))
                return BadRequest("Product Code already exists.");

            var storedProduct = await _productRepo.GetIncludingAsync(
                p => p.Id == id, p => p.ProductDiscounts);


            if (storedProduct == null)
                return NotFound();

            _productDiscountRepo.DeleteMany(x => x.ProductId == id);

            if (prod.ProductDiscounts != null)
                foreach(var disc in prod.ProductDiscounts)
                {
                    disc.ProductId = storedProduct.Id;
                    disc.CreateDate = disc.UpdateDate = DateTime.Now;
                    disc.CreateBy = disc.UpdateBy = "Test";

                    _productDiscountRepo.AddAsync(disc);
                }

            prod.UpdateDate  = DateTime.Now;
            prod.UpdateBy = "Test"; //User.FindFirst(ClaimTypes.Name).Value;

            _productRepo.Update(prod);

            await _uom.CommitAsync();

            return Ok();
        }
    }
}