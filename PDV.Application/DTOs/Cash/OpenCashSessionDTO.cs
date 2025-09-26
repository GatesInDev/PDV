using System.ComponentModel.DataAnnotations;

namespace PDV.Application.DTOs.Cash
{
    /// <summary>
    /// DTO para abrir uma sessão de caixa.
    /// </summary>
    public class OpenCashSessionDTO
    {
        /// <summary>
        /// Nome do operador que está abrindo o caixa.
        /// </summary>
        [Required(ErrorMessage = "O nome do operador é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome do operador não pode exceder 100 caracteres.")]
        public string OperatorName { get; set; }

        /// <summary>
        /// Quanto dinheiro há no caixa ao abrir a sessão.
        /// </summary>
        [Required(ErrorMessage = "O valor de abertura é obrigatório.")]
        public decimal OpeningAmount { get; set; }
    }
}
