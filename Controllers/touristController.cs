using Microsoft.AspNetCore.Mvc;
using smr.Entitis;
using smr.Core.Services;


namespace smr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class touristController : ControllerBase
    {
        private readonly ItouirstService _touristService;
        public touristController(ItouirstService customerService)
        {
            _touristService = customerService;
        }


        // GET: api/customers
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_touristService.GetList());
        }

        // GET: api/customers/{id}
        [HttpGet("{id}")]
        public ActionResult GetById(string id)
        {
            var touirst = _touristService.GetById(id);
            if (touirst != null)
            {
                return Ok(touirst);
            }
            return NotFound();

        }
        // POST api/<customersController>
        [HttpPost]
        public ActionResult Post([FromBody] Tourist value)
        {
            var touirst = _touristService.GetById(value.id);
            if (touirst != null)
            {
                return Conflict();
            }
            _touristService.Add(value);
            return Ok(); ;
        }

        // PUT: api/customers/{id}
        [HttpPut("{id}")]
        public ActionResult Put(string id, Tourist value)
        {
            var touirst = _touristService.Put(id, value);
            if (touirst == null)
            {
                return Conflict();
            }
           
            return Ok();
        }
        // PUT api/customers/status/5
        [HttpPut("status/{id}")]
        public ActionResult Put(string id, bool isActive)
        {
            var touirst = _touristService.GetById(id);
            if (touirst == null)
            {
                return Conflict();
            }
            _touristService.PutStatus(id, isActive);
            return Ok();
        }
    }
}

