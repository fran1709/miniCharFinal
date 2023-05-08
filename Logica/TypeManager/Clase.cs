using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;

namespace miniChart.Logica.TypeManager;

public class Clase : Tipo
{
    public readonly string tipo;
    public readonly string tipoDato;
    public LinkedList<Object> variables;
    public LinkedList<Object> metodos;
    public Clase(IToken tok) : base(tok)
    {
        tipo = "clase";
        tipoDato = "null";
        variables = new LinkedList<object>();
        metodos = new LinkedList<object>();
    }
    
    public static bool IsClase(string nombre)
    {
        return !TipoBasico.isTipoBasico(nombre) && !string.IsNullOrEmpty(nombre) && nombre.All(c => char.IsLetterOrDigit(c));
    }
    
    public override string ToString()
    {
        return $"Token: {tok.Text}, Tipo: {tipo}, Tipo de dato: {tipoDato}, Nivel: {nivel} " + "\n";
    }
}