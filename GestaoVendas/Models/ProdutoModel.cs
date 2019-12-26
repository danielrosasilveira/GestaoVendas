using System.ComponentModel.DataAnnotations;

namespace GestaoVendas.Models
{
    public class ProdutoModel
    {
        public string Idproduto { get; set; }

        [Required(ErrorMessage = "Informe o Nome do Produto")]
        public string Nome { get; set; }
        
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe a Preço do Produto")]
        public decimal? Preco_unitario { get; set; }

        [Required(ErrorMessage = "Informe a Quantidade")]
        public int Quantidade { get; set; }

        public byte[] Foto { get; set; }

        public string ContentType { get; set; }
    }
}
