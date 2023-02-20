using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pizza_orders.data.Models;
using pizza_orders.data.Repositories.Interfaces;
using pizza_orders.Requests.Orders;
using pizza_orders.Responses.Orders;
using System.Xml.Linq;

namespace pizza_orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IClientRepository clientRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            if (createOrderRequest == null)
            {
                return BadRequest();
            }

            bool clientExits = await _clientRepository.ExitsAsync(createOrderRequest.ClientId);

            if (!clientExits)
            {
                return BadRequest("No existe el cliente especificado");
            }


            bool exitsPayment = PaymentMethod.IsDefined<PaymentMethod>(createOrderRequest.PaymentMethod);

            if (!exitsPayment)
            {
                return BadRequest("No existe el método de pago especificado");
            }

            var order = _mapper.Map<Order>(createOrderRequest);
            order.Date = DateTime.Now;
            order.State = State.Done;
            await _orderDetailRepository.FillPrices(order.OrderDetails);
            order.Prize = order.OrderDetails.Sum(x => x.Amount);

            await _orderRepository.AddAsync(order);
            int saveResult = await _orderRepository.SaveAsync();

            if (!(saveResult > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Valor no esperado al guardar nuevo pedido");
            }

            return Ok();
        }


        [HttpGet("id:int", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderResponse>> GetOrder(int id)
        {
            var order = await _orderRepository.GetAsync(id);

            if (order == null)
            {
                return NotFound("Pedido no encontrado");
            }

            var orderResponse = _mapper.Map<OrderResponse>(order);
            return Ok(orderResponse);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<OrderResponse>>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();
            var ordersResponse = _mapper.Map<List<OrderResponse>>(orders);

            return Ok(ordersResponse);
        }

    }
}
