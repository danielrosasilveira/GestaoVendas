using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoVendas.Models
{
    public class VendedorModel
    {
        public int Idvendedor { get; set; }

        [Required(ErrorMessage = "Informe Nome")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Informe Sexo")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Informe E-mail")]        
        [DataType(DataType.EmailAddress)]        
        public string Email { get; set; }

        [Required(ErrorMessage = "Confirme a Senha")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 3, 
            ErrorMessage = "Senha mínima de 3 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirme a Senha")]
        [StringLength(255, MinimumLength = 3,
            ErrorMessage = "Senha mínima de 3 caracteres")]
        [DataType(DataType.Password)]
        [Compare(nameof(Senha), ErrorMessage = "Senha não Confere")]
        public string ConfirmSenha { get; set; }

        [Required(ErrorMessage = "Informe Nível")]
        public string Nivel { get; set; }

        [Required(ErrorMessage = "Informe Status")]
        public string Status { get; set; }

        [Column(TypeName = "Imagem")]
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "JPG, JPEG ou PNG")]        
        public byte[] Foto { get; set; }
        
        public string ContentType { get; set; }
    }
}



