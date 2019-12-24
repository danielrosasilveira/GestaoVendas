using System.ComponentModel.DataAnnotations;

namespace GestaoVendas.Models
{
    public class ClienteModel
    {
        public int Idcliente { get; set; }

        [Required(ErrorMessage = "Informe Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe CEP")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Informe Rua/Av.")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Informe Nr.")]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = "Informe Bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Informe Cidade")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe UF")]
        public string UF { get; set; }
    }
}
