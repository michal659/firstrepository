using Microsoft.AspNetCore.Mvc;
using smr.Entitis;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace smr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class renterController : ControllerBase
    {
        private readonly DataContext _Context;
        public renterController(DataContext context)
        {
            _Context = context;
        }
        public static List<Renter> renters = new List<Renter>
        {
            new Renter { id = "1", CountryNameOfBusiness = "London", isActive = true },
            new Renter { id = "2", CountryNameOfBusiness = "Austria", isActive = false }
        };



        // GET: api/<renterController>
        [HttpGet]
        public IEnumerable<Renter> Get()
        {
            return renters;
        }

        // GET api/<renterController>/5
        [HttpGet("{id}")]
        public ActionResult<Renter> GetById(string id)
        {

            var renter = renters.Find(x => x.id == id);
            if (renter != null)
            {
                return Ok(renter);
            }
            else
            {
                return NotFound(renter);
            }
        }

        // POST api/<renterController>
        [HttpPost]
        public Renter Post([FromBody] Renter value)
        {
            renters.Add(new Renter { id = value.id, CountryNameOfBusiness = value.CountryNameOfBusiness, isActive = value.isActive });
            return value;
        }

        // PUT api/<renterController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, bool isActive)
        {
            var index = renters.FindIndex(e => e.id == id);
            if (index == -1)
            {
                return NotFound();
            }
            renters[index].isActive = isActive;
            return Ok();
        }
      
    }
}
