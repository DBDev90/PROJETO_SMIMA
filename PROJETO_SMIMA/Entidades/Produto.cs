using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROJETO_SMIMA.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        
        [Column(TypeName = "decimal(5,2)")]
        public decimal Valor { get; set; }

        public string Imagem { get; set; }
    }
}
