using System.ComponentModel;
using ASP.NET_Project.Models;

namespace ASP.NET_Project.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {

        }
        public OrderViewModel(int id, string shipName, Order.PaymentTypeEnum paymentType, double totalPrice, Order.OrderStatusEnum orderStatus)
        {
            Id = id;
            OrderStatus = orderStatus;
            PaymentTypeId = paymentType;
            TotalPrice = totalPrice;
            ShipName = shipName;
        }
        public int Id { get; set; }
        public Order.OrderStatusEnum OrderStatus { get; set; }
        public Order.PaymentTypeEnum PaymentTypeId { get; set; }
        [DisplayName("TotalPrice")]
        public double TotalPrice { get; set; }
        [DisplayName("ShipName")]
        public string ShipName { get; set; }
    }
}