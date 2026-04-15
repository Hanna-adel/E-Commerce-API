using System;
using System.Collections.Generic;
using System.Text;

namespace Project.BLL
{
    public class CartReadDto
    {
        public ICollection<CartItemReadDto> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}
