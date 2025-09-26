﻿using System.ComponentModel.DataAnnotations;

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

        /// <summary>
        /// Quanto dinheiro há no caixa ao fechar a sessão.
        /// </summary>
        [Required(ErrorMessage = "O valor de fechamento é obrigatório.")]
        public decimal ClosingAmount { get; set; }
    }
}
