using Microsoft.AspNetCore.Mvc;
using smr.Entitis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace smr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class turnController : ControllerBase
    {
        private readonly DataContext _Context;
        public turnController(DataContext context)
        {
            _Context = context;
        }
        public static List<Turn> turns = new List<Turn>
        {
            new Turn { id="1" ,day="12/10/24",hour="10:00"},
            new Turn {id="2", day="6/3/23",hour="3:00"}
        };

        // GET: api/<turnController>
        [HttpGet]
        public IEnumerable<Turn> Get()
        {
            return turns;
        }

        // GET api/<turnController>/5
        [HttpGet("{id}")]
        public ActionResult<Turn> GetById(string id)
        {
            var turn = turns.Find(x => x.id == id);
            if (turn != null)
            {
                return Ok(turn);
            }
            else
            {
                return NotFound(turn);
            }
        }


        // POST api/<turnController>
        [HttpPost]
        public Turn Post([FromBody] Turn value)
        {
            turns.Add(new Turn { id = value.id, day = value.day, hour = value.hour });
            return value;
        }


        // PUT api/<turnController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Turn value)
        {
            var turn = turns.Find(x => x.id == id);
            if (turn != null)
            {
                turn.day = value.day;
                turn.hour = value.hour;
                return Ok(turn);
            }
            else
                return NotFound();
        }

        // DELETE api/<turnController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var index = turns.FindIndex(e => e.id == id);
            if (index != -1)
                turns.RemoveAt(index);
        }
    }
}
