using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using models;

namespace catalog.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ICosmosDBRepository<models.product> _cosmosDBRepository;

        public ProductController(ICosmosDBRepository<models.product> cosmosDBRepository)
        {
            _cosmosDBRepository = cosmosDBRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<models.product>> Get()
        {
            var items = await _cosmosDBRepository.GetItemsAsync(d => d.Id != null);
            return items;
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<models.product> Get(string id)
        {
            var item = await _cosmosDBRepository.GetItemAsync(id);
            return item;
        }

        [HttpPost]
        public void Post([FromBody]models.product value)
        {
            _cosmosDBRepository.CreateItemAsync(value).Wait();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _cosmosDBRepository.DeleteItemAsync(id).Wait();
        }

    }
}