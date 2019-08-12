using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OccBooking.Application.Commands;
using OccBooking.Application.Queries;
using OccBooking.Common.Dispatchers;

namespace OccBooking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController : BaseController
    {
        public ValuesController(IDispatcher dispatcher) : base(dispatcher)
        {
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultFromCommand = await CommandAsync(new TestCommand());
            var resultFromQuery = await QueryAsync(new TestQuery());

            return Ok(resultFromCommand.IsSuccess + resultFromQuery.Value);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
