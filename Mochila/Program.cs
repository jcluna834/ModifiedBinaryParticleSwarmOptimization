using Mochila.Algoritmo;
using Mochila.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mochila
{
    public class Program
    {
        static void Main(string[] args)
        {
            /* PARA GENERAR LOS REPORTES...
            Archivo datos, reporte_mbpso2, reporte_mbpso1;
            //int opc = -1;
            for (int i = 4; i < 5; i++)
            {
                try
                {
                    Random r2, r1;
                    datos = new Archivo("knapsack" + (i + 1) + ".txt", 0);
                    double[] config = datos.configuracion();
                    
                    int semilla1, semilla2, s = (new Random(100)).Next(0,1001);

                    r1 = new Random(s);
                    semilla1 = 560;
                    r2 = new Random(s);
                    semilla2 = 560
                    int swarmSize, dimensiones, ni_mbpso2, opcionArreglo, ni_mbpso1,
                            velocidadMaxima, numIteracionesBL;
                    double peso, factorMejorLocal, factorMejorGlobal;
                    FuncionObjetivo fx;
                    Elemento[] elem;
                    dimensiones = (int)config[0];
                    peso = config[1];
                    elem = datos.getElementos();
                    ni_mbpso2 = 100;
                    ni_mbpso1 = 100;
                    opcionArreglo = 1;
                    factorMejorGlobal = 2.0;
                    factorMejorLocal = 2.0;
                    velocidadMaxima = 4;
                    swarmSize = 50;
                    numIteracionesBL = 100;
                    fx = new FuncionMochila(elem);
                    fx.maximizar();
                    reporte_mbpso2 = new Archivo("reporte_knapsack" + (i + 1) + "_2.txt", 1);
                    for (int j = 0; j < 30; j++)
                    {
                        semilla2++;
                        r2 = new Random(semilla2);
                        long tiempoIi = Environment.TickCount;
                        MBPSO_II algoritmo = new MBPSO_II(r2);
                        algoritmo.inicializar(swarmSize, dimensiones, fx, ni_mbpso2, peso, opcionArreglo, factorMejorLocal, factorMejorGlobal, velocidadMaxima, numIteracionesBL);
                        Solucion optimizado = algoritmo.ejecutarBusquedaLocal(algoritmo.ejecutar());
                        long tiempoEf = Environment.TickCount;
                        string resultado2 = optimizado.getPeso() + " " + optimizado.getValor() + " " + (tiempoEf - tiempoIi) + " " + semilla2;
                        reporte_mbpso2.agregarLinea(resultado2);
                    }
                    reporte_mbpso2.close();
                    Console.WriteLine("reporte_knapsack" + (i + 1) + "_2.txt");
                    reporte_mbpso1 = new Archivo("reporte_knapsack" + (i + 1) + "_1.txt", 1);
                    for (int j = 0; j < 30; j++)
                    {
                        semilla1++;
                        r1 = new Random(semilla1);
                        long tiempoIi = Environment.TickCount;
                        ModifiedBinaryParticleSwarmOptimization alg = new ModifiedBinaryParticleSwarmOptimization(r1);
                        alg.inicializar(swarmSize, dimensiones, fx, ni_mbpso1, peso, opcionArreglo, factorMejorLocal, factorMejorGlobal, velocidadMaxima);
                        Solucion optimizado = alg.ejecutar();
                        long tiempoEf = Environment.TickCount;
                        string resultado1 = optimizado.getPeso() + " " + optimizado.getValor() + " " + (tiempoEf - tiempoIi) + " " + semilla1;
                        reporte_mbpso1.agregarLinea(resultado1);
                    }
                    reporte_mbpso1.close();
                    Console.WriteLine("reporte_knapsack" + (i + 1) + "_1.txt");
                    datos.close();
                }
                catch (Exception e) { Console.WriteLine("Error: " + e.Message + "\n" + e.StackTrace); Console.ReadLine(); }
            }
        }
    }
}*/
            Archivo datos;
            int opc = -1;
            datos = new Archivo("knapsack6.txt", 0);
            double[] config = datos.configuracion();
            do
            {
                try
                {
                    int swarmSize, dimensiones, numIteraciones, semilla, opcionArreglo, velocidadMaxima, numIteracionesBL;
                    double peso, factorMejorLocal, factorMejorGlobal;
                    FuncionObjetivo fx;
                    Elemento[] elem;
                    Random r;

                    dimensiones = (int)config[0];
                    peso = config[1];
                    elem = datos.getElementos();

                    //Parametros a afinar
                    semilla = 789;//(new Random()).Next(1, 1001);
                    numIteraciones = 100;
                    opcionArreglo = 1;
                    //Parametros MBPSO
                    factorMejorGlobal = 0.3;
                    factorMejorLocal = 1.0 - factorMejorGlobal;
                    velocidadMaxima = 4;
                    swarmSize = 20;
                    // un número alto permitirá mejor exploración del espacio de busqueda
                    // pero aumenta el tiempo de ejecucion.

                    //Parametros busqueda local
                    numIteracionesBL = 500;

                    if (semilla == -1)
                        r = new Random();
                    else
                        r = new Random(semilla);

                    fx = new FuncionMochila(elem);
                    //maximizar o minimizar
                    fx.maximizar();

                    Console.WriteLine("Elementos Disponibles: " + elem.Length + " Peso máximo: " + peso + "\nNúmero iteraciones: " + numIteraciones + " Semilla: " + semilla);
                    Console.WriteLine("inicializando...");

                    Console.WriteLine("Tamaño del Enjambre: " + swarmSize);
                    long tiempoInicial = Environment.TickCount;
                    MBPSO_II algoritmo = new MBPSO_II(r);
                    algoritmo.inicializar(swarmSize, dimensiones, fx, numIteraciones, peso, opcionArreglo, factorMejorLocal, factorMejorGlobal, velocidadMaxima, numIteracionesBL);
                    Solucion mejor = algoritmo.ejecutar();
                    long tiempoFinal = Environment.TickCount;
                    Console.WriteLine("Peso máximo: " + peso);
                    Console.WriteLine("Tiempo ejecución: " + (tiempoFinal - tiempoInicial) + " ms");
                    Console.WriteLine("Mejor Solución");
                    mejor.mostrar();

                    //Solucion optimizado = algoritmo.ejecutarBusquedaLocal(mejor);
                    //Console.WriteLine("Mejor Solución Optimizado");
                    //optimizado.mostrar();

                }
                catch (Exception e) { Console.WriteLine("Error: " + e.Message + "\n" + e.StackTrace); }
                Console.Write("Presiona 0 para salir...\nOtra tecla para volver a ejecutar...");
                try
                {
                    opc = Int32.Parse(Console.ReadKey(false).KeyChar.ToString());
                }
                catch (Exception e) { }
                Console.Clear();
            } while (opc != 0);
            datos.close();
        }
    }
}