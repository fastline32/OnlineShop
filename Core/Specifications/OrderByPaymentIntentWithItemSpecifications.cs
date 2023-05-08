using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrderByPaymentIntentWithItemSpecifications : BaseSpecification<Order>
    {
        public OrderByPaymentIntentWithItemSpecifications(string paymentIntentId) 
        : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}