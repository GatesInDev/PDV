using System.Text.Json.Serialization;

namespace PDV.Core.Entities
{
    /// <summary>
    /// Entidade que representa uma sessão de caixa.
    /// </summary>
    public class CashSession
    {
        /// <summary>
        /// Identificador único da sessão de caixa.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do operador de caixa responsável pela sessão.
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// Data e hora de abertura da sessão de caixa.
        /// </summary>
        public DateTime OpenedAt { get; set; }

        /// <summary>
        /// Data e hora de fechamento da sessão de caixa. Pode ser nulo se o caixa ainda estiver aberto.
        /// </summary>
        public DateTime? ClosedAt { get; set; }

        /// <summary>
        /// Valor em dinheiro no caixa no momento da abertura da sessão.
        /// </summary>
        public decimal OpeningAmount { get; set; }

        /// <summary>
        /// Valor em dinheiro no caixa no momento do fechamento da sessão. Pode ser nulo se o caixa ainda estiver aberto.
        /// </summary>
        public decimal? ClosingAmount { get; set; }


        /// <summary>
        /// Lista de vendas associadas a esta sessão de caixa.
        /// </summary>
        public List<Sale> Sales { get; set; } = new();
    }
}
