using Project.Common;
using Project.DAL;

namespace Project.BLL
{
    public class CartManager : ICartManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartManager(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GeneralResult<CartReadDto>> AddToCartAsync(string userId, CartCreateDto cartCreateDto)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(cartCreateDto.ProductId);
            var totalQuantity = cartCreateDto.Quantity;

            if (product == null)
            {
                return GeneralResult<CartReadDto>.NotFound($"Product with ID {cartCreateDto.ProductId} not found.");
            }
            var cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            if(cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow,
                    CartItems = new List<CartItem>()
                };
                await _unitOfWork.CartRepository.AddAsync(cart);
                await _unitOfWork.SaveAsync();
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartCreateDto.ProductId);
            if (existingItem != null)
            {
                totalQuantity += existingItem.Quantity;
            }
            if (product.StockQuantity < totalQuantity)
            {
                return GeneralResult<CartReadDto>.FailRequest("Not enough stock available.");

            }
            if (existingItem != null)
            {
                existingItem.Quantity = totalQuantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = cartCreateDto.ProductId,
                    Quantity = totalQuantity,
                    CartId = cart.Id
                });
            }

            await _unitOfWork.SaveAsync();

            var resultCart = new CartReadDto
            {
                CartItems = cart.CartItems.Select(ci => new CartItemReadDto
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    ProductName = ci.Products.Name,

                    Price = ci.Products.Price
                }).ToList(),
                CartTotal = cart.CartItems.Sum(ci => ci.Quantity * ci.Products.Price)
            };

                return GeneralResult<CartReadDto>.SuccessResult(resultCart, "Product added to cart successfully.");
        }

        public async Task<GeneralResult<CartReadDto>> GetCartReadAsync(string userId)
        {
            var cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            if(cart == null)
            {
                return GeneralResult<CartReadDto>.NotFound($"Cart for user with ID {userId} not found.");
            }
            var resultCart = new CartReadDto
            {
                CartItems = cart.CartItems.Select(ci => new CartItemReadDto
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    ProductName = ci.Products.Name,
                    Price = ci.Products.Price,
                    ProductImageUrl = ci.Products.ImageUrl,
                    TotalPrice = ci.Products.Price * ci.Quantity
                }).ToList(),
                CartTotal = cart.CartItems.Sum(ci => ci.Quantity * ci.Products.Price)
            };
            return GeneralResult<CartReadDto>.SuccessResult(resultCart, "Cart retrieved successfully.");
        }

        public async Task<GeneralResult> RemoveFromCartAsync(string userId, int productId)
        {
            var cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            if(cart == null)
            {
                return GeneralResult<CartReadDto>.NotFound($"Cart for user with ID {userId} not found.");
            }
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if(cartItem == null)
            {
                return GeneralResult<CartReadDto>.NotFound($"Product with ID {productId} not found in cart.");
            }
            _unitOfWork.CartRepository.RemoveCartItem(cartItem);
            await _unitOfWork.SaveAsync();
            return GeneralResult.SuccessResult("Product removed from cart successfully.");
        }

        public async Task<GeneralResult<CartCreateDto>> UpdateCartAsync(string userId, CartCreateDto cartCreateDto)
        {
            var cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            if(cart == null)
            {
                return GeneralResult<CartCreateDto>.NotFound($"Cart for user with ID {userId} not found.");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartCreateDto.ProductId);
            if(cartItem == null)
            {
                return GeneralResult<CartCreateDto>.NotFound($"Product with ID {cartCreateDto.ProductId} not found in cart.");
            }

            cartItem.Quantity = cartCreateDto.Quantity;
            await _unitOfWork.SaveAsync();

            var result = new CartReadDto
            {
                CartItems = cart.CartItems.Select(ci => new CartItemReadDto
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Products.Name,
                    ProductImageUrl = ci.Products.ImageUrl,
                    Price = ci.Products.Price,
                    Quantity = ci.Quantity,
                    TotalPrice = ci.Products.Price * ci.Quantity
                }).ToList(),
                CartTotal = cart.CartItems.Sum(ci => ci.Products.Price * ci.Quantity)
            };
            return GeneralResult<CartCreateDto>.SuccessResult(cartCreateDto, "Cart updated successfully.");
        }
    }
}
