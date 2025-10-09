namespace PDV.Core.Entities
{
    /// <summary>
    /// Representa um registro de auditoria no sistema.
    /// </summary>
    public class Logs
    {
        /// <summary>
        /// Identificador único do log.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome da tabela afetada pela ação.
        /// </summary>
        public string Table { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de ação realizada.
        /// </summary>
        public string Action { get; set; } = string.Empty;

        /// <summary>
        /// Nome do usuário que realizou a ação.
        /// </summary>
        public string User { get; set; } = string.Empty;

        /// <summary>
        /// Data e hora em que a ação foi realizada.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Dados relacionados à ação realizada, geralmente em formato JSON.
        /// </summary>
        public string Data { get; set; } = string.Empty;
    }
}
