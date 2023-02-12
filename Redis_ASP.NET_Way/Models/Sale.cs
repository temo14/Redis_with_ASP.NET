namespace Redis_ASP.NET_Way.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int Total { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
