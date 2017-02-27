using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mochila.Algoritmo
{
    public class Solucion
    {
        public int[] vector { set; get; }

        protected FuncionObjetivo fx { set; get; }

        protected double valor { set; get; }

        protected double peso { set; get; }

        protected double densidad { set; get; }

        protected Random r { set; get; }

        public double pesoMaximo { set; get; }

        public Solucion(int dimensiones, FuncionObjetivo fx, Random r, double pesoMaximo)
        {
            vector = new int[dimensiones];
            valor = -1;
            peso = -1;
            densidad = -1;
            this.fx = fx;
            this.r = r;
            this.pesoMaximo = pesoMaximo;
        }

        public void setPeso(double peso)
        {
            this.peso = peso;
        }

        public void setValor(double valor)
        {
            this.valor = valor;
        }

        public void setDensidad(double densidad)
        {
            this.densidad = densidad;
        }

        public double getValor()
        {
            if (valor == -1)
                valor = fx.evaluarValor(this);
            return valor;
        }

        public double getPeso()
        {
            if (peso == -1)
                peso = fx.evaluarPeso(this);
            return peso;
        }

        public double getDensidad()
        {
            if (getPeso() > 0)
            {
                if (densidad == -1)
                    densidad = fx.evaluarDensidad(this);
                return densidad;
            }
            else
                throw new DivideByZeroException("Error, peso o valor mal asignado.");
        }

        public Solucion copy()
        {
            Solucion copy = new Solucion(vector.Length, fx, r, pesoMaximo);
            copy.vector = (int[])vector.Clone();
            return copy;
        }

        public void mostrar()
        {
            Console.WriteLine("valor: " + getValor());
            Console.WriteLine("Peso: " + getPeso());
            foreach (int aux in vector)
                Console.Write(aux);
            Console.WriteLine();
        }

    }
}