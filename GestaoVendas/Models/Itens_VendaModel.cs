namespace GestaoVendas.Models
{
    public class Itens_VendaModel
    {
        /*Precisa ser exatamente igual ao JSON 
        * para que ocorra a deserealização.
        * Todas as propriedades devem ser STRING */

        public string Iditensvenda { get; set; }
        public string Qtd { get; set; }
        public string Valor { get; set; }
        public string Fk_idproduto { get; set; }
        public string Fk_idvenda { get; set; }
    }
}


