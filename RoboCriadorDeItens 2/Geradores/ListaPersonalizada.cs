using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCriadorDeItens_2.Geradores
{
    public class ListaPersonalizada
    {
        public ListaPersonalizada() { }

        public Guid Id { get; set; }

        public ListaPersonalizada(Guid id)
        {
            this.Id = id;
        }
    }
}
