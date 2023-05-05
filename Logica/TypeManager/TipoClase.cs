using Antlr4.Runtime;

namespace miniChart.Logica.TypeManager;

public class TipoClase : Tipo
{
    public readonly string tipo;
    public readonly string tipoDato;
    
    public TipoClase(IToken tok, string td) : base(tok)
    {
        tipo = "tipoClase";
        tipoDato = td;
    }
    
    public static bool IsTipoClase(string ident)
    {
        return Clase.isClase(ident);
    }
    
    public override string ToString()
    {
        return $"Token: {tok.Text}, Tipo: {tipo}, Tipo de dato: {tipoDato}, Nivel: {nivel} " + "\n";
    }
    
    
}