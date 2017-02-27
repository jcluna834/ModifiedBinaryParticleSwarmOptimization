using Mochila.Algoritmo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mochila.Funciones
{
    public class FuncionMochila : FuncionObjetivo
    {
        public FuncionMochila(Elemento[] elementos)
        {
            valorMax = 0;
            this.elementos = elementos;
            for (int i = 0; i < elementos.Length; i++)
                valorMax += elementos[i].valor;
        }

        public override double evaluarPeso(Solucion s)
        {
            int i = 0;
            double total = 0.0;
            foreach (Elemento elemento in elementos)
            {
                if (s.vector[i] == 1)
                    total += elemento.peso;
                i++;
            }
            return total;
        }

        public override double evaluarValor(Solucion s)
        {
            int i = 0;
            double total = 0.0;
            foreach (Elemento elemento in elementos)
            {
                if (s.vector[i] == 1)
                    total += elemento.valor;
                i++;
            }
            return total;
        }

        public override double evaluarDensidad(Solucion s)
        {
            int i = 0;
            double total = 0.0;
            foreach (Elemento elemento in elementos)
            {
                if (s.vector[i] == 1)
                    total += elemento.getDensidad();
                i++;
            }
            return total;
        }
    }
}