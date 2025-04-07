using Ambev.Api.Models;
using Ambev.Infrastructure.Extensions;
using Ambev.Shared.Common.Http;
using Ambev.Shared.Entities;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IRepositoryBase<Item> _itemRepository;

        public ItemController(IRepositoryBase<Item> itemRepository)
        {
            _itemRepository=itemRepository;
        }


        /// <summary>
        /// Gets a single item
        /// </summary>
        /// <param name="id">item id</param>
        /// <returns></returns>
        /// <response code="200">The item was successfully retrieved.</response>
        /// <response code="404">The item does not exist.</response>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Item), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetItem([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await _itemRepository.GetByIdAsync(id, cancellationToken);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetItems([FromQuery] QueryParameters query, CancellationToken cancellationToken)
        {

            var paginedResult = await _itemRepository.DbSet.PagingAsync(
                pageNumber: query.Page,
                pageSize: query.Size,
                sortTerm: query.Order,
                cancellationToken: cancellationToken
            );

            return Ok(paginedResult);
        }
        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] Item item, CancellationToken cancellationToken)
        {
            var result = await _itemRepository.AddAsync(item);
            return Ok(result);
        }

    }
}
