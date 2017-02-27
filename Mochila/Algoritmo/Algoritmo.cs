using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mochila.Algoritmo
{
    public abstract class Algoritmo
    {
        protected FuncionObjetivo fx { set; get; }

        public Solucion Best { set; get; }

        protected int numIteraciones { set; get; }

        protected int opcionArreglo { set; get; }

        protected Random r { set; get; }

        protected double pesoMaximo { set; get; }

        public abstract Solucion ejecutar();

        protected Solucion arreglarSolucion(Solucion nueva)
        {
            nueva.setValor(-1);
            if (opcionArreglo == 1)
                return repararArmonia(nueva);
            else
                return contrapeso(nueva);
        }

        protected Solucion contrapeso(Solucion ajustada)
        {
            ajustada.setValor(ajustada.getValor() - (fx.getValorMaximo() * (ajustada.getPeso() - pesoMaximo)));
            return ajustada;
        }

        protected abstract Solucion repararArmonia(Solucion nueva);

        protected Solucion prender(Solucion ajustada)
        {
            if (ajustada.getPeso() > pesoMaximo)
                return apagar(ajustada);
            else
            {
                if (ajustada.getPeso() == pesoMaximo)
                    return ajustada;
                else
                {
                    int tam = ajustada.vector.Length, pos = r.Next(0, tam);
                    ajustada.setPeso(ajustada.getPeso() + fx.elementos[pos].peso);
                    ajustada.vector[pos] = 1;
                    return prender(ajustada);
                }
            }
        }

        protected Solucion apagar(Solucion nueva)
        {
            int tam = nueva.vector.Length;
            int pos = r.Next(0, tam);
            int i = pos + 1;
            while (nueva.getPeso() > pesoMaximo)
            {
                if (i >= tam)
                    i = 0;
                if (nueva.vector[i] == 1)
                {
                    nueva.setPeso(nueva.getPeso() - fx.elementos[i].peso);
                    nueva.vector[i] = 0;
                }
                i++;
            }
            if (nueva.getPeso() > 0)
                return nueva;
            else
                return prender(nueva);
        }
    }
}
