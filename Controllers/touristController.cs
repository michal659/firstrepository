using Microsoft.AspNetCore.Mvc;
using smr.Entitis;
using smr.Core.Services;
using AutoMapper;
using smr.Core.DTOs;
using smr.Models;
using Microsoft.AspNetCore.Authorization;

namespace smr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "touirst")]
    public class touristController : ControllerBase
    {
        private readonly ItouirstService _touristService;
        private readonly IMapper _mapper;
        public touristController(ItouirstService customerService,IMapper map)
        {
            _touristService = customerService;
            _mapper = map;

        }


        // GET: api/customers
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get()
        {
            var touirstsList =  await _touristService.GetListAsync();
            var touirsts = _mapper.Map<IEnumerable<touirstDTO>>(touirstsList);
            return Ok(touirsts);
        }

        // GET: api/customers/{id}
        [HttpGet("{id}")]
        public async Task< ActionResult> GetById(string id)
        {

            var touir =  await _touristService.GetByIdAsync(id);
            var touirst = _mapper.Map<renterDTO>(touir);
            if (touir != null)
            {
                return Ok(touirst);
            }
            return NotFound();
        }
        // POST api/<customersController>
        [HttpPost]
        public async Task< ActionResult> Post([FromBody]  toirstPostModel t)
        {
            var NewTouirst = _mapper.Map<Tourist>(t);
            var touir = _touristService.GetByIdAsync(NewTouirst.id);
            if (touir == null)
            {
                return Conflict();
            }
             await _touristService.AddAsync(NewTouirst);
            return Ok(); ;
        }
     
  


// PUT: api/customers/{id}
[HttpPut("{id}")]
        public async  Task <ActionResult> Put(string id, [FromBody] toirstPostModel t)
        {
            var NewTouirst = await _touristService.UpdateAsync(id, _mapper.Map<Tourist>(t));
               
            
            if (NewTouirst != null)
            {
                return Ok(NewTouirst);
            }
            return NotFound();
        }



    // PUT api/customers/status/5
    [HttpPut("status/{id}")]
        public async  Task<ActionResult>Put(string id, bool isActive)
        {
            var touirst = await _touristService.GetByIdAsync(id);
            if (touirst == null)
            {
                return Conflict();
            }
           await _touristService.PutStatusAsync(id, isActive);
            return Ok();
        }
    }
}

