using smr.Core.Services;
using Microsoft.AspNetCore.Mvc;
using smr.Entitis;


namespace smr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class renterController : ControllerBase
    {
        private readonly IrenterService _renterService;
        public renterController(IrenterService renterService)
        {
            _renterService = renterService;
        }
       
        // GET: api/<renterController>
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_renterService.GetList());
        }
        // GET api/<renterController>/5
        [HttpGet("{id}")]
        public ActionResult GetById(string id)
        {
            var renter = _renterService.GetById(id);
            if (renter != null)
            {
                return Ok(renter);
            }
            return NotFound();

        }

        // POST api/<renterController>
        [HttpPost]
        public ActionResult Post([FromBody] Renter value)
        {
            var renter = _renterService.GetById(value.id);
            if (renter != null)
            {
                return Conflict();
            }
            _renterService.Add(value);
            return Ok(); ;
        }

        // PUT api/<renterController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, Renter value)
        {
            var renter = _renterService.GetById(id);
            if (renter == null)
            {
                return Conflict();
            }
            _renterService.Put(value);
            return Ok();
        }
        // PUT api/<bankersController>/status/5
        [HttpPut("status/{id}")]
        public ActionResult Put(string id, bool isActive)
        {
            var renter = _renterService.GetById(id);
            if (renter == null)
            {
                return Conflict();
            }
            _renterService.PutStatus(id, isActive);
            return Ok();
        }
      
    }
}
