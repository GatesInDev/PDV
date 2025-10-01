using System.ComponentModel.DataAnnotations;

namespace PDV.Application.DTOs.Cash
{
    /// <summary>
    /// DTO para abrir uma sessão de caixa.
    /// </summary>
    public class OpenCashSessionDTO
    {
        /// <summary>
        /// Quanto dinheiro há no caixa ao abrir a sessão.
        /// </summary>
        [Required(ErrorMessage = "O valor de abertura é obrigatório.")]
        public decimal OpeningAmount { get; set; }
    }
}
