using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AXTConverter.RestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AXTConverterController : ControllerBase
    {
        // GET: api/<AXTConverterController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AXTConverterController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AXTConverterController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AXTConverterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AXTConverterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
