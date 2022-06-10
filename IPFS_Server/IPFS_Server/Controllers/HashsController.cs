using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPFS_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashsController : ControllerBase
    {
        // GET: api/Hashs
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[]
            {
                "key" ,
                "value"
            };
        }

        // GET: api/Hashs/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Hashs
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT: api/Hashs/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
