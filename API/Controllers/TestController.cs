using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		// GET: api/<TestController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			throw new NotImplementedException();	
		}

		// GET api/<TestController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			throw new ArgumentNullException();
		}

		// POST api/<TestController>
		[HttpPost]
		public void Post([FromBody] string value)
		{

			throw new Exception("test");
		}

		// PUT api/<TestController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{

			throw new OutOfMemoryException();
		}

		// DELETE api/<TestController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
