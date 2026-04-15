namespace Project.BLL
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime PlaceAt { get; set; }
        public ICollection<OrderItemReadDto> OrderItems { get; set; }
    }
}
