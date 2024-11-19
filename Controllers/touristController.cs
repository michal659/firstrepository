using Microsoft.AspNetCore.Mvc;
using smr.Entitis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace smr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class touristController : ControllerBase
    {
        private readonly DataContext _Context;
        public touristController(DataContext context)
        {
            _Context = context;
        }
        public static List<Tourist> tourists = new List<Tourist>
        {
            new Tourist { id = "1", phonNumber = "0548416230", isActive = true },
            new Tourist { id = "2", phonNumber = "0548487645", isActive = false }
        };

        // GET: api/<touristControllercs>
        [HttpGet]
        public IEnumerable<Tourist> Get()
        {
            return tourists;
        }


        // GET api/<touristControllercs>/5
        [HttpGet("{id}")]
        public ActionResult<Tourist> GetById(string id)
        {

            var renter = tourists.Find(x => x.id == id);
            if (renter != null)
            {
                return Ok(renter);
            }
            else
            {
                return NotFound(renter);
            }
        }

        // POST api/<touristControllercs>
        [HttpPost]
        public Tourist Post([FromBody] Tourist value)
        {
            tourists.Add(new Tourist { id = value.id, phonNumber = value.phonNumber, isActive = value.isActive });
            return value;
        }

        // PUT api/<touristControllercs>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, bool isActive)
        {
            var index = tourists.FindIndex(e => e.id == id);
            if (index == -1)
            {
                return NotFound();
            }
            tourists[index].isActive = isActive;
            return Ok();
        }


    }
}
