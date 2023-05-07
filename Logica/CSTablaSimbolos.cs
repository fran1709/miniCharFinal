using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using miniChart.Logica.TypeManager;

namespace miniChart.Logica;

public class CSTablaSimbolos
{
    static LinkedList<Object> tabla;
    public int nivelActual;
    public Consola consola;

    public CSTablaSimbolos()
    {
        tabla = new LinkedList<Object>();
        nivelActual = -1;
        consola = new Consola();
    }
    
    public void insertar(Tipo ident)
    {
        ident.nivel = nivelActual;
        tabla.AddLast(ident);
    }
    public T buscarObjetoTipo<T>(string nombre) where T : Tipo
    {
        foreach (object id in tabla)
        {
            T obj = id as T;
            if (obj != null && obj.tok.Text.Equals(nombre))
            {
                return obj;
            }
        }
        return null;
    }

    public Tipo Buscar(string nombre)
    {
        foreach (object id in tabla)
        {
            Clase clase = id as Clase;
            if (clase != null && clase.tok.Text.Equals(nombre))
            {
                return clase;
            }
            Arreglo arreglo = id as Arreglo;
            if (arreglo != null && arreglo.tok.Text.Equals(nombre))
            {
                return arreglo;
            }
            TipoBasico tipoB = id as TipoBasico;                          
            if (tipoB != null && tipoB.tok.Text.Equals(nombre))   
            {                                                         
                return tipoB;                                       
            }    
            TipoClase tipoC = id as TipoClase;                          
            if (tipoC != null && tipoC.tok.Text.Equals(nombre))   
            {                                                         
                return tipoC;                                       
            }    
            Metodo metodo = id as Metodo;                          
            if (metodo != null && metodo.tok.Text.Equals(nombre))   
            {                                                         
                return metodo;                                       
            }                                                         
        }
        return null;
    }
    
    public void openScope(){
        nivelActual++;
    }
    
    public void CloseScope()
    {
        /*foreach (var item in tabla.ToList())
        {
            if (((Tipo)item).nivel == nivelActual)
            {
                tabla.Remove(item);
            }
        }*/
        nivelActual--;
    }
    
    public void Imprimir()
    {
        Console.WriteLine("----- INICIO TABLA ------");
        consola.SalidaConsola.Text += "----- INICIO TABLA ------\n";
        foreach (object id in tabla)
        {
            Console.WriteLine(id.ToString());
            consola.SalidaConsola.Text += id.ToString();
        }
        Console.WriteLine("----- FIN TABLA ------");
        consola.SalidaConsola.Text += "----- FIN TABLA ------\n";
    }
}