namespace Ambev.Shared.Entities.Sales
{
    public enum SaleStatus
    {
        PendingPayment = 0,
        Paid = 1,
        Transporting = 2,
        Delivered = 3,
        Cancelled = 4,
        Completed = 5,
    }
}