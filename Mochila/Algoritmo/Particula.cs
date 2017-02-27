using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mochila.Algoritmo
{
    public class Particula : Solucion
    {
        public Solucion Best { set; get; }

        public double[] velocidad { set; get; }

        public Particula(int dimensiones, FuncionObjetivo fx, Random r, double pesoMaximo)
            : base(dimensiones, fx, r, pesoMaximo)
        {
            Best = null;
            velocidad = new double[dimensiones];
        }

        public Particula copy(Solucion Best, double[] velocidad)
        {
            Particula copy = new Particula(vector.Length, fx, r, pesoMaximo);
            copy.vector = (int[])vector.Clone();
            copy.Best = Best;
            copy.velocidad = (double[])velocidad.Clone();
            return copy;
        }

    }
}