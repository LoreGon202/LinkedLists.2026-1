using System;
using System.Collections.Generic;

class Nodo
{
    public string Dato;
    public Nodo Siguiente;
    public Nodo Anterior;

    public Nodo(string dato)
    {
        Dato = dato;
        Siguiente = null;
        Anterior = null;
    }
}

class ListaDoble
{
    private Nodo cabeza;

    // Adicionar en orden ascendente
    public void Adicionar(string dato)
    {
        Nodo nuevo = new Nodo(dato);

        if (cabeza == null)
        {
            cabeza = nuevo;
            return;
        }

        Nodo actual = cabeza;

        while (actual != null && string.Compare(actual.Dato, dato) < 0)
        {
            actual = actual.Siguiente;
        }

        if (actual == cabeza)
        {
            nuevo.Siguiente = cabeza;
            cabeza.Anterior = nuevo;
            cabeza = nuevo;
        }
        else if (actual == null)
        {
            Nodo temp = cabeza;
            while (temp.Siguiente != null)
                temp = temp.Siguiente;

            temp.Siguiente = nuevo;
            nuevo.Anterior = temp;
        }
        else
        {
            Nodo anterior = actual.Anterior;

            anterior.Siguiente = nuevo;
            nuevo.Anterior = anterior;

            nuevo.Siguiente = actual;
            actual.Anterior = nuevo;
        }
    }

    public void MostrarAdelante()
    {
        Nodo actual = cabeza;
        while (actual != null)
        {
            Console.Write(actual.Dato + " -> ");
            actual = actual.Siguiente;
        }
        Console.WriteLine("NULL");
    }

    public void MostrarAtras()
    {
        if (cabeza == null) return;

        Nodo actual = cabeza;
        while (actual.Siguiente != null)
            actual = actual.Siguiente;

        while (actual != null)
        {
            Console.Write(actual.Dato + " -> ");
            actual = actual.Anterior;
        }
        Console.WriteLine("NULL");
    }

    public int Contar(string dato) //Este metodo cuenta cuántas veces aparece un dato en la lista
    {
        int contador = 0;
        Nodo actual = cabeza;

        while (actual != null)
        {
            if (actual.Dato == dato)
                contador++;

            actual = actual.Siguiente;
        }

        return contador;
    }

    public bool EliminarUna(string dato)
    {
        Nodo actual = cabeza;

        while (actual != null)
        {
            if (actual.Dato == dato)
            {
                if (actual.Anterior != null)
                    actual.Anterior.Siguiente = actual.Siguiente;
                else
                    cabeza = actual.Siguiente;

                if (actual.Siguiente != null)
                    actual.Siguiente.Anterior = actual.Anterior;

                return true; //eliminado
            }
            actual = actual.Siguiente;
        }
        return false; //no encontrado
    }

    public bool EliminarTodas(string dato)
    {
        bool eliminado = false;
        Nodo actual = cabeza;

        while (actual != null)
        {
            Nodo siguiente = actual.Siguiente;

            if (actual.Dato == dato)
            {
                if (actual.Anterior != null)
                    actual.Anterior.Siguiente = actual.Siguiente;
                else
                    cabeza = actual.Siguiente;

                if (actual.Siguiente != null)
                    actual.Siguiente.Anterior = actual.Anterior;

                eliminado = true;
            }

            actual = siguiente;
        }
        return eliminado; 
    }

    public void MostrarModa()
    {
        Dictionary<string, int> conteo = new Dictionary<string, int>();
        Nodo actual = cabeza;

        while (actual != null)
        {
            if (!conteo.ContainsKey(actual.Dato))
                conteo[actual.Dato] = 0;

            conteo[actual.Dato]++;
            actual = actual.Siguiente;
        }

        int max = 0;
        foreach (var item in conteo)
        {
            if (item.Value > max)
                max = item.Value;
        }

        Console.WriteLine("Moda(s):");
        foreach (var item in conteo)
        {
            if (item.Value == max)
                Console.WriteLine(item.Key);
        }
    }

    public void MostrarGrafico()
    {
        Dictionary<string, int> conteo = new Dictionary<string, int>();
        Nodo actual = cabeza;

        while (actual != null)
        {
            if (!conteo.ContainsKey(actual.Dato))
                conteo[actual.Dato] = 0;

            conteo[actual.Dato]++;
            actual = actual.Siguiente;
        }

        foreach (var item in conteo)
        {
            Console.Write(item.Key + " ");
            for (int i = 0; i < item.Value; i++)
                Console.Write("\u2B50");
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        ListaDoble lista = new ListaDoble();
        int opcion;

        do
        {
            Console.WriteLine("\n-------- MENÚ ---------");
            Console.WriteLine("1. Adicionar");
            Console.WriteLine("2. Mostrar hacia adelante");
            Console.WriteLine("3. Mostrar hacia atrás");
            Console.WriteLine("4. Mostrar descendente");
            Console.WriteLine("5. Mostrar moda");
            Console.WriteLine("6. Mostrar gráfico");
            Console.WriteLine("7. Existe Si/No");
            Console.WriteLine("8. Eliminar una ocurrencia");
            Console.WriteLine("9. Eliminar todas las ocurrencias");
            Console.WriteLine("10. Salir");
            Console.Write("\n ");
            Console.Write("Selecciona una Opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    {
                        Console.Write("Ingrese un Dato: ");
                        lista.Adicionar(Console.ReadLine());
                        break;
                    }

                case 2:
                    {
                        lista.MostrarAdelante();
                        break;
                    }

                case 3:
                    {
                        lista.MostrarAtras();
                        break;
                    }

                case 4:
                    {
                        lista.MostrarAtras(); // descendente
                        break;
                    }

                case 5:
                    {
                        lista.MostrarModa();
                        break;
                    }

                case 6:
                    {
                        lista.MostrarGrafico();
                        break;
                    }

                case 7:
                    {
                        Console.Write("Buscar Dato: ");
                        string dato = Console.ReadLine();

                        int cantidad = lista.Contar(dato);

                        if (cantidad > 0)
                            Console.WriteLine($"El dato: '{dato}' Existe {cantidad} vez/veces");
                        else
                            Console.WriteLine($"El dato: '{dato}' No existe");
                        break;
                    }

                case 8:
                    {
                        Console.Write("¿Qué dato quieres Eliminar?: ");
                        string datoEliminar = Console.ReadLine();

                        if (lista.EliminarUna(datoEliminar))
                            Console.WriteLine("¡Dato eliminado con exito!");
                        else
                            Console.WriteLine("Dato no encontrado");
                        break;
                    }

                case 9:
                    {
                        Console.Write("¿Qué ocurrencias deseas eliminar?: ");
                        string datosEliminar = Console.ReadLine();

                        if (lista.EliminarTodas(datosEliminar))
                            Console.WriteLine("¡Datos eliminados con exito!");
                        else
                            Console.WriteLine("Dato no encontrado");
                        break;
                    }

            }

        } while (opcion != 10);
    }
}