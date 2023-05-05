using System;
using System.Collections.Generic;
using Antlr4.Runtime;

namespace miniChart.Logica.TypeManager;

public class TipoBasico : Tipo
{
    public enum TiposBasicos
    {
        Int, 
        Double, 
        Char, 
        String, 
        Boolean,
        Error,
    }

    public readonly string tipo;
    public readonly int tipoDato;
    
    public TipoBasico(IToken tok, int td) : base(tok)
    {
        tipo = "basico";
        tipoDato = td;
    }

    public static bool isTipoBasico(string tipo)
    {
        return tipo.Equals("int") || tipo.Equals("double") || tipo.Equals("char") || tipo.Equals("string") || tipo.Equals("boolean");
    }
    
    public static string showTipo(int tipoDato)
    {
        switch (tipoDato)
        {
            case (int)TiposBasicos.Int: return TiposBasicos.Int.ToString().ToLower();
            case (int)TiposBasicos.Double: return TiposBasicos.Double.ToString().ToLower();
            case (int)TiposBasicos.Char: return TiposBasicos.Char.ToString().ToLower();
            case (int)TiposBasicos.String: return TiposBasicos.String.ToString().ToLower();
            case (int)TiposBasicos.Boolean: return TiposBasicos.Boolean.ToString().ToLower();
            default: return TiposBasicos.Error.ToString();
        }
    }

    public override string ToString()
    {
        return $"Token: {tok.Text}, Tipo: {tipo}, Tipo de dato: {showTipo(tipoDato)}, Nivel: {nivel} " + "\n";
    }
}