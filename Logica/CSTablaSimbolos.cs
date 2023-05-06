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

    public Clase buscarClase(string nombre)
    {
        foreach (object id in tabla)
        {
            Clase clase = id as Clase;
            if (clase != null && clase.tok.Text.Equals(nombre))
            {
                return clase;
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