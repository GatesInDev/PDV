namespace PDV.Application.DTOs.Metrics
{
    public class DataIndicatorsDTO
    {
        /// <summary>
        /// Saída do ticket médio Diario (DailyIncome/DailySales).
        /// </summary>
        public decimal MediumTicket { get; set; }

        /// <summary>
        /// Saída do ganho total diario.
        /// </summary>
        public decimal DailyIncome { get; set; }

        /// <summary>
        /// Saída da quantidade de protudos vendidas no dia.
        /// </summary>
        public int DailySales { get; set; }
    }
}
