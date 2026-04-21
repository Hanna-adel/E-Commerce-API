using Project.Common;
using Project.DAL;

namespace Project.BLL
{
    public class OrderManager : IOrderManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GeneralResult<OrderReadDto>> GetOrderByIdAsync(int id, string userId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderWithItemAsync(id);
            if(order == null)
            {
                return GeneralResult<OrderReadDto>.NotFound($"Order with id {id} not found.");
            }
            if(order.UserId != userId)
            {
                return GeneralResult<OrderReadDto>.FailRequest("You are not authorized to view this order.");
            }
            var result = new OrderReadDto
            {
                Id = order.Id,
                ShippingAddress = order.ShippingAddress,
                Status = order.status.ToString(),
                TotalAmount = order.TotalAmount,
                PlaceAt = order.PlacedAt,
                OrderItems = order.OrderItems.Select(oi => new OrderItemReadDto
                {
                    ProductName = oi.Products.Name,
                    ProductImageUrl = oi.Products.ImageUrl,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.UnitPrice * oi.Quantity
                }).ToList()
            };

            return GeneralResult<OrderReadDto>.SuccessResult(result, "Order retrieved successfully");
        }

        public async Task<GeneralResult<IEnumerable<OrderReadDto>>> GetUserOrderAsync(string userId)
        {
            var orders = await _unitOfWork.OrderRepository.GetUserOrderAsync(userId);
            if (!orders.Any())
            {
                return GeneralResult<IEnumerable<OrderReadDto>>.NotFound($"No orders found.");
            }

            var result = orders.Select(order => new OrderReadDto
            {
                Id = order.Id,
                ShippingAddress = order.ShippingAddress,
                Status = order.status.ToString(),
                TotalAmount = order.TotalAmount,
                PlaceAt = order.PlacedAt,
                OrderItems = order.OrderItems.Select(oi => new OrderItemReadDto
                {
                    ProductName = oi.Products.Name,
                    ProductImageUrl = oi.Products.ImageUrl,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.UnitPrice * oi.Quantity
                }).ToList()
            });
            return GeneralResult<IEnumerable<OrderReadDto>>.SuccessResult(result, "Orders retrieved successfully");
        }

        public async Task<GeneralResult<OrderReadDto>> PlaceOrderAsync(string userId, OrderCreateDto orderCreateDto)
        {
            var cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            if(cart == null || !cart.CartItems.Any())
            {
                return GeneralResult<OrderReadDto>.NotFound("Cart not found for the user.");
            }
            foreach(var item in cart.CartItems)
            {
                if(item.Products.StockQuantity < item.Quantity)
                {
                    return GeneralResult<OrderReadDto>.FailRequest($"Product {item.Products.Name} is out of stock.");
                }
            }
            var order = new Order
            {
                ShippingAddress = orderCreateDto.ShippingAddress,
                UserId = userId,
                PlacedAt = DateTime.UtcNow,
                status = 0,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in cart.CartItems)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.ProductId);
                if(product == null)
                {
                    return GeneralResult<OrderReadDto>.NotFound($"Product with id {item.ProductId} not found.");
                }
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };
                order.OrderItems.Add(orderItem);

                product.StockQuantity -= item.Quantity;
                _unitOfWork.ProductRepository.Update(product);
            }

            order.TotalAmount = order.OrderItems.Sum(oi => oi.UnitPrice * oi.Quantity);

            foreach(var items in cart.CartItems)
            {
                _unitOfWork.CartRepository.RemoveCartItem(items);
            }

            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.SaveAsync();

            var result = new OrderReadDto
            {
                Id = order.Id,
                ShippingAddress = order.ShippingAddress,
                Status = order.status.ToString(),
                TotalAmount = order.TotalAmount,
                PlaceAt = order.PlacedAt,
                OrderItems = order.OrderItems.Select(oi => new OrderItemReadDto
                {
                    ProductName = oi.Products.Name,
                    ProductImageUrl = oi.Products.ImageUrl,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.UnitPrice * oi.Quantity
                }).ToList()
            };

            return GeneralResult<OrderReadDto>.SuccessResult(result, "Order placed successfully");
        }

        public async Task<GeneralResult<OrderReadDto>> UpdateOrderStatusAsync(int orderId, OrderStatusUpdateDto dto)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderWithItemAsync(orderId);

            if (order == null)
                return GeneralResult<OrderReadDto>.NotFound($"order with id {orderId} not found.");

            order.status = (OrderStatus)dto.Status;

            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveAsync();
            var result = new OrderReadDto
            {
                Id = order.Id,
                ShippingAddress = order.ShippingAddress,
                Status = order.status.ToString(),
                TotalAmount = order.TotalAmount,
                PlaceAt = order.PlacedAt,
                OrderItems = order.OrderItems.Select(oi => new OrderItemReadDto
                {
                    ProductName = oi.Products.Name,
                    ProductImageUrl = oi.Products.ImageUrl,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.UnitPrice * oi.Quantity
                }).ToList()
            };
            return GeneralResult<OrderReadDto>.SuccessResult(result,"Order placed successfully");
        }

        public async Task<GeneralResult<bool>> CancelOrderAsync(string userId, int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderWithItemAsync(orderId);

            if (order == null)
                return GeneralResult<bool>.NotFound("Order canceled successfully");

            if (order.UserId != userId)
                return GeneralResult<bool>.FailRequest("Access denied");

            if (order.status == OrderStatus.Shipped || order.status == OrderStatus.Delivered)
                return GeneralResult<bool>.FailRequest("Cannot cancel an order that has already been shipped or delivered");

            order.status = OrderStatus.Cancelled;

            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveAsync();

            return GeneralResult<bool>.SuccessResult(true);
        }

        public Task<GeneralResult<OrderReadDto>> PlaceOrderAsync(OrderCreateDto orderCreateDto, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
