using System.ComponentModel.DataAnnotations;

namespace Entities.Models.Enums
{
    public enum StatusProduto
    {
        [Display(Name = "Disponivel")] Disponivel = 0,
        [Display(Name = "Vendido, Aguardando Entregador")] Vendido = 1,
        [Display(Name = "Aguardando Aprovação")] Aguardando_Aprovacao = 2,
        [Display(Name = "Em Rota De Entrega")] Em_Rota_De_Entrega = 3,
        [Display(Name = "Entregue")] Entregue = 4,
        [Display(Name = "Bloqueado")] Bloqueado = 5,
        [Display(Name = "Produto Avaliado")] ProdutoAvaliado = 6

    }
    
}
