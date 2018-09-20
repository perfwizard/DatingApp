using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    public class DeliveryNoteController : Controller
    {
        private readonly IDeliveryNoteRepository _dnRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uom;
        private readonly IStatusRepository _statusRepo;
        private readonly string DN_PREFIX = "DN";

        public DeliveryNoteController(IDeliveryNoteRepository dnRepo, 
                IMapper mapper, IUnitOfWork uom, IStatusRepository statusRepo)
        {
            _dnRepo = dnRepo;
            _mapper = mapper;
            _uom = uom;
            _statusRepo = statusRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetDns(DeliveryNoteParams param)
        {
            var dns = await _dnRepo.GetDeliveryNotes(param);

            Response.AddPagination(
                        dns.CurrentPage, dns.PageSize, dns.TotalCount, dns.TotalPages);

            var ret = _mapper.Map<List<DeliveryNoteResource>>(dns);
            //ret.ForEach(s => 
            //    s.StatusName = (_statusRepo.GetStatusName(s.StatusCode)).StatusName);
            return Ok(ret);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dn = await _dnRepo.GetDeliveryNoteWithLines(id);

            var ret = _mapper.Map<DeliveryNoteResource>(dn);
            //ret.StatusName = (await _statusRepo.GetStatusName(ret.StatusCode)).StatusName;

            return Ok(ret);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]DeliveryNoteResource dn)
        {
            var saveDn = _mapper.Map<DeliveryNote>(dn);

            var dnPrefix = DN_PREFIX + 
                        DateTime.Today.Year.ToString().Substring(2) + 
                        DateTime.Today.Month.ToString("d2");

            var lastDn = await _dnRepo.GetLastDnNo(dnPrefix);
            string nextNo = "0001";
            if (lastDn != null)
            {
                var currentLastStringNo = lastDn.DnNo.Substring(6, 4);
                var currentLastNo = Int32.Parse(currentLastStringNo);
                nextNo = ("0000" + (currentLastNo+1)).Substring(("0000" + (currentLastNo+1)).Length - 4);
            }
            saveDn.DnNo = dnPrefix + nextNo;
            saveDn.CreateDate = saveDn.UpdateDate = DateTime.Now;
            saveDn.CreateBy = saveDn.UpdateBy = User.FindFirst(ClaimTypes.Name).Value;
            

            _dnRepo.AddAsync(saveDn);

            await _uom.CommitAsync();

            return Created("Created sucessfully.", dn);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]DeliveryNoteResource dn)
        {
            var storedDn = await _dnRepo.GetByIdAsync(dn.Id);
            
            _mapper.Map(dn, storedDn);
            
            storedDn.UpdateDate = DateTime.Now;
            storedDn.UpdateBy = User.FindFirst(ClaimTypes.Name).Value;
            await _uom.CommitAsync();
            
            return Ok("Updated successfully.");
        }
    }
}