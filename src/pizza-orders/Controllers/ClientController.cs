using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pizza_orders.data.Models;
using pizza_orders.data.Repositories.Interfaces;
using pizza_orders.Requests.Client;
using pizza_orders.Responses.Client;

namespace pizza_orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientController(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ClientResponse>>> GetAllClients()
        {
            var clients = await _clientRepository.GetAllAsync();
            var clientsResponse = _mapper.Map<List<ClientResponse>>(clients);

            return Ok(clientsResponse);
        }

        [HttpGet("id:int", Name = "GetClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientResponse>> GetClient(int id)
        {
            var client = await _clientRepository.GetAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult> CreateClient([FromBody] CreateClientRequest createClientRequest)
        {
            if (createClientRequest == null)
            {
                return BadRequest();
            }

            if (await _clientRepository.ExitsAsync(createClientRequest.Email))
            {
                return BadRequest("Ya existe un cliente con ese email");
            }

            var client = _mapper.Map<Client>(createClientRequest);
            await _clientRepository.AddAsync(client);
            int saveResult = await _clientRepository.SaveAsync();

            if (!(saveResult > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Valor no esperado al guardar client");
            }

            var clientResponse = _mapper.Map<ClientResponse>(client);

            return CreatedAtAction("GetClient", new { id = client.Id }, clientResponse);
        }


    }
}
