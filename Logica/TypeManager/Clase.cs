using System.Linq;
using Antlr4.Runtime;

namespace miniChart.Logica.TypeManager;

public class Clase : Tipo
{
    public readonly string tipo;
    public readonly string tipoDato;
    public Clase(IToken tok) : base(tok)
    {
        tipo = "clase";
        tipoDato = "null";
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