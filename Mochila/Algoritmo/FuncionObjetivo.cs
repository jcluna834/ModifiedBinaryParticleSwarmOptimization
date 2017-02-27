using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mochila.Algoritmo
{
    public abstract class FuncionObjetivo
    {
        private int opc { set; get; }

        protected double valorMax { set; get; }

        public Elemento[] elementos { set; get; }

        public void maximizar() { opc = 1; }

        public void minimizar() { opc = 0; }

        public bool isMax() { return opc == 1; }

        public bool isMin() { return opc == 0; }

        public double getCalidad(Solucion s)
        {
            if (isMin())
                return Int32.MaxValue - s.getValor();
            else
                return s.getValor();
        }

        public double getValorMaximo() { return valorMax; }

        public abstract double evaluarPeso(Solucion s);

        public abstract double evaluarValor(Solucion s);

        public abstract double evaluarDensidad(Solucion s);
    }
}