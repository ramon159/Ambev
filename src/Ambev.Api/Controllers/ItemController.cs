using Ambev.Shared.Entities;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IRepositoryBase<Item> _itemRepository;

        public ItemController(IRepositoryBase<Item> itemRepository)
        {
            this._itemRepository=itemRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await _itemRepository.GetByIdAsync(id);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] Item item, CancellationToken cancellationToken)
        {
            var result = await _itemRepository.AddAsync(item);
            return Ok(result);
        }
    }
}
