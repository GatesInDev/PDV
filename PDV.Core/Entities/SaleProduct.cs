using PDV.Core.Entities;
using System.Text.Json.Serialization;

/// <summary>
/// Entidade que representa a relação entre uma venda e um produto, incluindo quantidade e preço no momento da venda.
/// </summary>
public class SaleProduct
{
    /// <summary>
    /// Identificador da venda.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Navegação para a venda associada.
    /// </summary>
    [JsonIgnore]
    public Sale Sale { get; set; }

    /// <summary>
    /// Identificador do produto.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Navegação para o produto associado.
    /// </summary>
    [JsonIgnore]
    public Product Product { get; set; }

    /// <summary>
    /// Quantidade do produto na venda.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Valor do produto no momento da venda.
    /// </summary>
    public decimal PriceAtSaleTime { get; set; }
}
