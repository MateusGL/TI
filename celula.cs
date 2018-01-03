using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste
{
    class celula
    {
        int origem, destino, peso, cor;
        public celula(int v1, int v2, int v3)
        {
            this.origem = v1;
            this.destino = v2;
            this.peso = v3;
        }
        public int GetOrigem
        {
            get
            {
                return this.origem;
            }
        }
        public int GetDestino
        {
            get
            {
                return this.destino;
            }
        }
        public int Getpeso
        {
            get
            {
                return this.peso;
            }
        }
        public int Getcor
        {
            get
            {
                return this.cor;
            }
            set
            {
                this.cor = value;
            }
        }
    }
}
