using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mochila.Algoritmo
{
    class HillClimbing : Algoritmo
    {
        public HillClimbing(int numIteraciones, FuncionObjetivo fx, Random r, double pesoMaximo)
        {
            this.numIteraciones = numIteraciones;
            this.fx = fx;
            this.r = r;
            this.pesoMaximo = pesoMaximo;
        }

        public override Solucion ejecutar()
        {
            int i = 0;
            do
            {
                Solucion R = tweak(Best.copy());
                if (fx.getCalidad(R) > fx.getCalidad(Best))
                    Best = R.copy();
                i++;
            } while (i < numIteraciones);
            return Best;
        }

        protected override Solucion repararArmonia(Solucion nueva)
        {
            Solucion ajustada;
            if (nueva.getPeso() > pesoMaximo)
                ajustada = apagar(nueva);
            else
                ajustada = nueva.copy();
            return prender(ajustada);
        }

        public int buscar(Solucion copy)
        {
            int posicion = r.Next(copy.vector.Length);
            int i = posicion - 1;
            while (copy.vector[posicion] != 1 && i != posicion)
            {
                posicion++;
                if (posicion >= copy.vector.Length)
                    posicion = 0;
            }
            if (copy.vector[posicion] == 1)
                return posicion;
            else return -1;
        }

        public Solucion tweak(Solucion copy)
        {
            int posicion = buscar(copy);
            if (posicion == -1)
                copy = prender(copy);
            else
                copy.vector[posicion] = 0;
            copy.setPeso(-1);
            posicion = r.Next(copy.vector.Length);
            while (true)
            {
                if (copy.getPeso() > pesoMaximo)
                {
                    if (copy.vector[posicion] == 0)
                        posicion = buscar(copy);
                    copy.setPeso(copy.getPeso() - fx.elementos[posicion].peso);
                    copy.vector[posicion] = 0;
                    if (copy.getPeso() <= pesoMaximo && copy.getPeso() > 0)
                        break;
                }
                else
                {
                    if (copy.vector[posicion] == 0)
                    {
                        copy.setPeso(copy.getPeso() + fx.elementos[posicion].peso);
                        copy.vector[posicion] = 1;
                    }
                }
                posicion = r.Next(copy.vector.Length);
            }
            return copy;
        }
    }
}