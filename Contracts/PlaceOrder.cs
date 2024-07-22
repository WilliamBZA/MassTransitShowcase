namespace Contracts
{
    public class PlaceOrder
    {
        public required Guid OrderId { get; set; }
    }

    public class OrderPlaced
    {
        public required Guid OrderId { get; set; }
    }
}