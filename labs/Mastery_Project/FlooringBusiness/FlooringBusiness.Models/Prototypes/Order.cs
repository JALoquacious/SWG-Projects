namespace FlooringBusiness.Models.Prototypes
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public string Customer { get; set; }
        public decimal Area { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal LaborCost { get; set; }
        public decimal Total { get; set; }
        public Product Product { get; set; }
        public StateTax StateTax { get; set; }
    }
}
