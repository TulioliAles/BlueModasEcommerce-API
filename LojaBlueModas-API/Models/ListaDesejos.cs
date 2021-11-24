using System;

namespace LojaBlueModas_API.Models
{
    public partial class ListaDesejos
    {
        public string ListaDesejosId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
