using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mochila.Algoritmo
{
    class ModifiedBinaryParticleSwarmOptimization: Algoritmo
    {
        private Particula[] swarm { set; get; }

        private double factorMejorLocal { set; get; }

        private double factorMejorGlobal { set; get; }

        private int velocidadMaxima { set; get; }

        private int swarmSize { set; get; }

        public ModifiedBinaryParticleSwarmOptimization(Random r) { this.r = r; }

        //Inicialización de parametros
        public void inicializar(int swarmSize, int dimensiones, FuncionObjetivo fx, int NI, double peso, int opcionArreglo, double factorMejorLocal, double factorMejorGlobal, int velocidadMaxima)
        {
            pesoMaximo = peso;
            numIteraciones = NI;
            Best = null;
            this.swarmSize = swarmSize;
            this.fx = fx;
            this.opcionArreglo = opcionArreglo;
            this.factorMejorLocal = factorMejorLocal;
            this.factorMejorGlobal = factorMejorGlobal;
            this.velocidadMaxima = velocidadMaxima;
            // Inicialización del cumulo de particulas
            swarm = new Particula[this.swarmSize];
            int i = 0;
            for (; i < this.swarmSize; i++)
                swarm[i] = new Particula(dimensiones, fx, r, pesoMaximo);
            inicializarSwarm();
        }

        private void inicializarSwarm()
        {
            /* 
             * Divide el espacio de busqueda en cuadrantes y de manera 
             * aleatoria pone 1 o 0 en cada posicion del cuadrante, permitiendo
             * que la población inicialice cubriendo gran parte del espacio de busqueda.
             */
            int fraccion = (int)(swarm[0].vector.Length / swarmSize), pos = 0;
            for (int j = 0; j < swarmSize; j++)
            {
                int i = 0;
                while (i < fraccion)
                {
                    swarm[j].vector[pos] = r.Next(0, 2);
                    i++;
                    pos++;
                }
                if (swarm[j].getPeso() > pesoMaximo || swarm[j].getPeso() <= 0)
                    swarm[j] = (Particula)arreglarSolucion(swarm[j].copy(swarm[j].Best, swarm[j].velocidad));
            }
        }

        //ejecución del algoritmo
        public override Solucion ejecutar()
        {
            int i = 0;
            do
            {
                actualizarBest();
                int j = 0;
                for (; j < swarmSize; j++)
                {
                    actualizarVelocidad(swarm[j]);
                    actualizarPosicion(swarm[j]);
                    swarm[j] = (Particula)arreglarSolucion(swarm[j].copy(swarm[j].Best, swarm[j].velocidad));
                }
                i++;
            } while (i < numIteraciones);
            return Best;
        }

        private void actualizarPosicion(Particula p)
        {
            int i = 0, tam = p.velocidad.Length;
            p.setPeso(0);
            for (; i < tam; i++)
            {
                if (aleatorio() < probabilidad(p.vector[i], p.velocidad[i]))
                    p.vector[i] = 1;
                else
                    p.vector[i] = 0;
                p.setPeso(p.getPeso() + (p.vector[i] * fx.elementos[i].peso));
                if (p.getPeso() >= pesoMaximo)
                    break;
            }
        }

        private double probabilidad(int x, double v)
        {
            return (x + v + velocidadMaxima)
                / (1 + 2 * velocidadMaxima);
        }

        private double aleatorio()
        {
            return (r.Next(-velocidadMaxima, 1 + velocidadMaxima) + velocidadMaxima)
                    / (1 + 2 * velocidadMaxima);
        }

        private void actualizarVelocidad(Particula p)
        {
            int i = 0, tam = p.velocidad.Length;
            for (; i < tam; i++)
                p.velocidad[i] = p.velocidad[i]
                    + factorMejorLocal * r.NextDouble() * (p.Best.vector[i] - p.vector[i])
                    + factorMejorGlobal * r.NextDouble() * (Best.vector[i] - p.vector[i]);
        }

        private void actualizarBest()
        {
            int i = 0;
            for (; i < swarm.Length; i++)
            {
                if (swarm[i].Best == null || fx.getCalidad(swarm[i]) > fx.getCalidad(swarm[i].Best))
                    swarm[i].Best = swarm[i].copy();
                if (Best == null || fx.getCalidad(swarm[i].Best) > fx.getCalidad(Best))
                    Best = swarm[i].Best.copy();
            }
        }

        protected override Solucion repararArmonia(Solucion n)
        {
            Particula ajustada, nueva = (Particula)n;
            if (nueva.getPeso() > pesoMaximo)
                ajustada = (Particula)apagar(nueva);
            else
                ajustada = nueva.copy(nueva.Best, nueva.velocidad);
            return prender(ajustada);
        }
    }
}