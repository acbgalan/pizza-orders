using pizza_orders.data.Models;

namespace pizza_orders.Requests.Orders
{
    public class CreateOrderRequest
    {
        public int ClientId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<CreateOrderRequestDetail> Details { get; set; }
    }
}
