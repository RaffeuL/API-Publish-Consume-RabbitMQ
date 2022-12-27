namespace Domain.SharedKernel.Models
{
    public class BaseOrderModel
    {
        public int? OrderId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
    }
}
