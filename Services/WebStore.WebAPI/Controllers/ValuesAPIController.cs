using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.WebAPI.Controllers
{
    [Route("api/values")]
    //[Route("api/[controller]")]
    [ApiController]
    public class ValuesAPIController : ControllerBase
    {
        static readonly Dictionary<int, string> values = Enumerable.Range(1, 10)
            .Select(x => (Id: x, Value: $"Value-{x}"))
            .ToDictionary(v => v.Id, v => v.Value);

        private readonly ILogger<ValuesAPIController> logger;

        public ValuesAPIController(ILogger<ValuesAPIController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (!values.ContainsKey(id))
                return NotFound();

            return Ok(values[id]);
        }

        [HttpGet("count")]
        public int Count() => values.Count();

        [HttpGet]
        public IEnumerable<string> GetAll() => values.Values;

        [HttpPost] //POST -> http://localhost:5001/api/values/
        [HttpPost("add")] //POST -> http://localhost:5001/api/values/add/
        public IActionResult Add([FromBody] string value)
        {
            var id = values.Count == 0 ? 1 : values.Keys.Max() + 1;
            values[id] = value;

            return CreatedAtAction(nameof(GetById), new { id }, value);
        }

        [HttpPut("{id}")] //PUT -> http://localhost:5001/api/values/
        public IActionResult Edit(int id, [FromBody] string value)
        {
            if (!values.ContainsKey(id))
                return NotFound();

            values[id] = value;

            return Ok(values[id]);
        }

        [HttpDelete("{id}")] //DELETE -> http://localhost:5001/api/values/
        public IActionResult Delete(int id)
        {
            if (!values.ContainsKey(id))
                return NotFound();

            values.Remove(id);

            return Ok();
        }
    }
}
