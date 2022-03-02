namespace rna.Authentication.api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {

        public ValuesController()
        {
        }


        // GET api/values/5
        [HttpGet]
        public string Get(int id, string name)
        {
            return $"Hello there Number: {id}, Name: {name}";
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
