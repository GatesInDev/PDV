using System.ComponentModel.DataAnnotations;

namespace PDV.Application.DTOs.Cash
{
    /// <summary>
    /// DTO para fechar uma sessão de caixa.
    /// </summary>
    public class CloseCashSessionDTO
    {
        /// <summary>
        /// Identificador da sessão de caixa a ser fechada.
        /// </summary>
        [Required(ErrorMessage = "O ID da sessão de caixa é obrigatório.")]
        public Guid Id { get; set; }
    }
}
