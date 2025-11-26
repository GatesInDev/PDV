namespace PDV.Clients.Models.Dashboard
{
    public class DashboardSummaryModel
    {
        public int TotalUsers { get; set; }
        public int ActiveSessions { get; set; }
        public int TransactionsToday { get; set; }
        public decimal RevenueToday { get; set; }
        public string ServerStatus { get; set; } = string.Empty;
        public double CpuLoad { get; set; }
    }
}