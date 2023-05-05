using Antlr4.Runtime;
namespace miniChart.Logica.TypeManager;

public abstract class Tipo
{
    public IToken tok;
    public int nivel;

    protected Tipo(IToken tok)
    {
        this.tok = tok;
        nivel = -1;
    }
}