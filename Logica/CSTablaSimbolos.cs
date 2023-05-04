using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;

namespace miniChart.Logica;

public class CSTablaSimbolos
{
    LinkedList<Object> tabla;
    private static int nivelActual;
    public Consola consola; 

    public class Ident
    {
        internal IToken tok;
        internal int type;
        internal int nivel;
        int valor;
        internal int dataType;

        public Ident(IToken tok, int type, int dataType)
        {
            this.tok = tok;
            this.type = type;
            nivel = nivelActual;
            valor = 0;
            this.dataType = dataType;
        }

        public void setValue(int v)
        {
            valor = v;
        }
    }

    public CSTablaSimbolos()
    {
        tabla = new LinkedList<Object>();
        nivelActual = -1;
        consola = new Consola();
    }
    
    public void insertar(IToken id, int tipo, int dataType)
    {
        Ident i = new Ident(id,tipo,dataType);
        tabla.AddFirst(i);
    }
    
    public Ident buscar(String nombre)
    {
        foreach (object id in tabla)
        {
            Ident ident = id as Ident;
            if (ident.tok.Text.Equals(nombre))
            {
                return ident;
            }
        }
        return null;
    }
    
    public void openScope(){
        nivelActual++;
    }
    
    public void CloseScope()
    {
        foreach (var item in tabla.ToList())
        {
            if (((Ident)item).nivel == nivelActual)
            {
                tabla.Remove(item);
            }
        }
        nivelActual--;
    }
    
    public void Imprimir()
    {
        Console.WriteLine("----- INICIO TABLA ------");
        consola.SalidaConsola.Text += "----- INICIO TABLA ------\n";
        foreach (object id in tabla)
        {
            IToken s = (IToken)((Ident)id).tok;
            Console.WriteLine($"Nombre: {s.Text} - {((Ident)id).nivel} - {((Ident)id).type}");
            consola.SalidaConsola.Text += $"Nombre: {s.Text} - {((Ident)id).nivel} - {((Ident)id).type} - {((Ident)id).dataType}\n";
        }
        Console.WriteLine("----- FIN TABLA ------");
        consola.SalidaConsola.Text += "----- FIN TABLA ------";
    }
}