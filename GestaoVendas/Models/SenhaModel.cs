using System.ComponentModel.DataAnnotations;

namespace GestaoVendas.Models
{
    public class SenhaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Senha Atual")]
        [DataType(DataType.Password)]       
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Nova Senha")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 3,
            ErrorMessage = "Senha mínima de 3 caracteres")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirme a Nova Senha")]
        [StringLength(255, MinimumLength = 3,
            ErrorMessage = "Senha mínima de 3 caracteres")]
        [DataType(DataType.Password)]
        [Compare(nameof(NovaSenha), ErrorMessage = "Senha não Confere")]
        public string ConfirmaNovaSenha { get; set; }
    }
}

