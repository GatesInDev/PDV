using PDV.Application.DTOs.Product;

namespace PDV.Application.DTOs.Sales
{
    public class CreateSalesDTO
    {
        /// <summary>
        /// Lista de produtos vendidos na venda.
        /// </summary>
        public List<ProductFormSaleDTO> Products { get; set; }  // Navigation property

        /// <summary>
        /// Define o método de pagamento utilizado na venda.
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Define o operador de caixa responsável pela venda.
        /// </summary>
        public string CashOperator { get; set; }
    }
}
