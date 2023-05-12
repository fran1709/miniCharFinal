using Antlr4.Runtime;
namespace miniChart.Logica.TypeManager;

public abstract class Tipo
{
    public IToken tok;
    public int nivel;
    public readonly string tipo;

    protected Tipo(IToken tok, string tipo)
    {
        this.tok = tok;
        nivel = -1;
        this.tipo = tipo;
    }
}