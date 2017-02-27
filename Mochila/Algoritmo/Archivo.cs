using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mochila.Algoritmo
{
    public class Archivo
    {
        private System.IO.FileStream archivo { set; get; }

        public Archivo(string file, int opc)
        {
            if (opc == 0)
                archivo = new System.IO.FileStream(file, FileMode.Open);
            else
                archivo = new System.IO.FileStream(file, FileMode.OpenOrCreate);
        }

        internal double[] configuracion()
        {
            archivo.Position = 0;
            System.IO.StreamReader archivoLeer = new System.IO.StreamReader(archivo);
            double[] datos = new double[2];
            string dato = archivoLeer.ReadLine();
            string [] aux = dato.Split(' ');
            datos[0] = double.Parse(aux[0]);
            datos[1] = double.Parse(aux[1]);
            return datos;
        }

        internal int cantidadElementos()
        {
            archivo.Position = 0;
            System.IO.StreamReader archivoLeer = new System.IO.StreamReader(archivo);
            int lineCount = 0;
            while (archivoLeer.ReadLine() != null)
                lineCount++;
            return lineCount - 3;
        }

        public Elemento[] getElementos()
        {
            int tam = cantidadElementos();
            archivo.Position = 0;
            System.IO.StreamReader archivoLeer = new System.IO.StreamReader(archivo);
            Elemento[] elementos = new Elemento[tam];
            archivoLeer.ReadLine();
            for (int i = 0; i < tam; i++)
            {
                string[] var = archivoLeer.ReadLine().Split(' ');
                elementos[i] = new Elemento(double.Parse(var[1]), double.Parse(var[0]));
            }
            return elementos;
        }

        public void close()
        {
            archivo.Dispose();
            archivo.Close();
        }

        public void agregarLinea(string texto)
        {
            System.IO.StreamWriter writer = new StreamWriter(archivo);
            writer.WriteLine(texto);
            writer.Flush();
        }
    }
}