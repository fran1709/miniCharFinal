using System.Collections.Generic;
using Antlr4.Runtime;

public class ErrorListener : BaseErrorListener
{
    public List<string> SyntaxErrors { get; } = new List<string>();

    
    public  void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        // Agrega el mensaje de error a la lista de errores de sintaxis.
        string error = $"Error de sintaxis en línea {line}, posición {charPositionInLine}: {msg}";
        SyntaxErrors.Add(error);
    }
}


