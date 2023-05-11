using System.Text.RegularExpressions;
using Antlr4.Runtime;

namespace miniChart.Logica.TypeManager;

public class Arreglo : Tipo
{
    public enum TiposArreglo
    {
        Int,
        Char,
        Error,
    }
    
    public readonly string tipo;
    public readonly int tipoDato;
    
    public Arreglo(IToken tok, int td) : base(tok)
    {
        tipo = "array";
        tipoDato = td;
    }
    
    public static bool isTipoArreglo(string tipo)
    {
        return Regex.IsMatch(tipo, "^(int|char)\\[[\\w\\s]*\\]$");
    }
    
    public override string ToString()
    {
        return $"Token: {tok.Text}, Tipo: {tipo}, Tipo de dato: {showTipo(tipoDato)}, Nivel: {nivel} " + "\n";
    }
    
    public static string showTipo(int tipoDato)
    {
        switch (tipoDato)
        {
            case (int)TiposArreglo.Int: return TiposArreglo.Int.ToString().ToLower();
            case (int)TiposArreglo.Char: return TiposArreglo.Char.ToString().ToLower();
            default: return TiposArreglo.Error.ToString();
        }
    }
}