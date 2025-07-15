using Microsoft.AspNetCore.Mvc;
using smr.Entitis;
using smr.Core.Services;
using AutoMapper;
using smr.Core.DTOs;
using smr.Models;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace smr.Controllers
{ 
 [Route("api/[controller]")]
[ApiController]
    [Authorize]
public class turnsController : ControllerBase
{
    private readonly IturnService _turnService;
        private readonly IMapper _mapper;
        public turnsController(IturnService turnService,IMapper map)
    {
        _turnService = turnService;
            _mapper = map;
        }

    // GET: api/<turnsController>
    [HttpGet]
    public  async Task<ActionResult> Get()
    {
        //return Ok(_turnService.GetList());

            var turnList =  await _turnService.GetListAsync();
            var turns = _mapper.Map<IEnumerable<TurnDTO>>(turnList);
            return Ok(turns);
        }
        

    // GET api/<turnsController>/5
    [HttpGet("{id}")]
    public async Task <ActionResult> GetById(string id)
    {
            var tur = await _turnService.GetByIdAsync(id);
            var turn = _mapper.Map<TurnDTO>(tur);
            if (tur != null)
            {
                return Ok(turn);
            }
            return NotFound();
        }

    // POST api/<turnsController>
    [HttpPost]
    public  async Task<ActionResult> Post([FromBody] TurnPostModel t)
    {
        var NewTurn= _mapper.Map<Turn>(t);
            var tur =  await _turnService.GetByIdAsync(NewTurn.id);
            if (tur == null)
            {
                return Conflict();
            }
           await _turnService.AddAsync(NewTurn);
            return Ok(); ;
        }
   

        // PUT api/<turnsController>/5
        [HttpPut("{id}")]
    public async Task< ActionResult> Put(string id, [FromBody] TurnPostModel t)
    {
            var NewTurn = _mapper.Map<Turn>(t);
            var turn = await _turnService.UpdateAsync(id, NewTurn);
            if (turn != null)
            {
                return Ok(turn);
            }
            return NotFound();
        }

        // DELETE api/<turnsController>/5
        [HttpDelete("{id}")]
    public async Task< ActionResult> Delete(string id)
    {
        var turn = _turnService.GetByIdAsync(id);
        if (turn == null)
            return Conflict();
      await  _turnService.DeleteAsync(id);
        return Ok();
    }
}
}