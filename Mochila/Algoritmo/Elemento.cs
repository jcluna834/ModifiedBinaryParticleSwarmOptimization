using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mochila.Algoritmo
{
    public class Elemento
    {
        public double peso { set; get; }

        public double valor { set; get; }

        public Elemento(double peso, double valor)
        {
            this.peso = peso;
            this.valor = valor;
        }

        public double getDensidad() 
        {
            if (peso == 0)
                throw new DivideByZeroException("Error división el peso no debe ser vacio.");
            return valor / peso;
        }
    }
}
