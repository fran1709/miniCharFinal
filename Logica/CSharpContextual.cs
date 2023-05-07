using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using miniChart.Logica.TypeManager;

namespace miniChart.Logica;

public class CSharpContextual : MiniCSharpParserBaseVisitor<Object>
{
    private CSTablaSimbolos laCsTablaSimbolos;

    public CSharpContextual()
    {
        this.laCsTablaSimbolos = new CSTablaSimbolos();
    }
    private String showErrorPosition(IToken t)
    {
        return " Fila: " + t.Line + " , Columna: " + (t.Column + 1);
    }

    // program : (using)* CLASS ident LEFTBRACK (varDecl | classDecl | methodDecl)* RIGHTBRACK EOF
    public override object VisitProgramAST(MiniCSharpParser.ProgramASTContext context)
    {
        //Visita a los using
        for (int i = 0; i < context.@using().Length; i++)
        {
            Visit(context.@using(i));
        }
        
        //Agregar la clase PRINCIPAL a la tabla 
        IToken id = context.ident().Start;
        laCsTablaSimbolos.openScope();
        if (Clase.isClase(id.Text))
        {
            Clase clase = new Clase(id);
            laCsTablaSimbolos.insertar(clase);
        }

        //Vista a la declaración de clases
        for (int i = 0; i < context.classDecl().Length; i++)
        {
            Visit(context.classDecl(i));
        }
        
        //Vista a la declaración de variables
        for (int i = 0; i < context.varDecl().Length; i++)
        {
            Visit(context.varDecl(i));
        }  
        
        //Declaración de constantes?
        
        //Vista a la declaración de métodos
        for (int i = 0; i < context.methodDecl().Length; i++)
        {
            Visit(context.methodDecl(i));
        }
        laCsTablaSimbolos.Imprimir(); //Impresión de la tabla como guía
        laCsTablaSimbolos.consola.Show();
        return null;
    }

    // using : USING ident SEMICOLON
    public override object VisitUsignAST(MiniCSharpParser.UsignASTContext context)
    {
        Visit(context.ident());
        return null;
    }
    
    // varDecl : type ident (COMMA ident)* SEMICOLON   
    public override object VisitVarDeclaAST(MiniCSharpParser.VarDeclaASTContext context)
    {
        //Se recorren todas las variables (cuando se declaran de un mismo tipo separadas por coma)
        for (int i = 0; i < context.ident().Length; i++)
        {
            int idType = (int)Visit(context.type()); //Se verifica el tipo de datos de la variable
            IToken id = context.ident(i).Start;
            if (TipoBasico.isTipoBasico(context.type().GetText()))
            {
                TipoBasico tipo = new TipoBasico(id, idType);
                laCsTablaSimbolos.insertar(tipo);

            }

            if (Arreglo.isTipoArreglo(context.type().GetText()))
            {
                Arreglo arreglo = new Arreglo(id, idType);
                laCsTablaSimbolos.insertar(arreglo);
            }

            if (TipoClase.IsTipoClase(context.type().GetText()))
            {
                Clase claseBuscada = this.laCsTablaSimbolos.buscarClase(context.type().GetText());
                // BUSQUEDA MEDIANTE TIPOS GENERICA ?
                Clase searched = this.laCsTablaSimbolos.buscarObjetoTipo<Clase>(context.type().GetText());

                if (claseBuscada != null)
                {
                    TipoClase tipoClase = new TipoClase(id, context.type().GetText());
                    laCsTablaSimbolos.insertar(tipoClase);
                }
                else
                {
                    laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de tipos: Tipo: \"" + context.type().GetText() + "\" no es un tipo válido." + showErrorPosition(context.type().Start) + "\n";
                }

            }
        }

        return null;
    }

    // classDecl : CLASS ident LEFTBRACK (varDecl)* RIGHTBRACK  
    public override object VisitClassDeclaAST(MiniCSharpParser.ClassDeclaASTContext context)
    {
        IToken id = context.ident().Start;
        if (Clase.isClase(id.Text))
        {
            Clase clase = new Clase(id);
            laCsTablaSimbolos.insertar(clase);
        }
        laCsTablaSimbolos.openScope();
        //Se recorren las declaraciones de las variables de la clase
        for (int i = 0; i < context.varDecl().Length; i++)
        {
            Visit(context.varDecl(i));
        }
        laCsTablaSimbolos.CloseScope();
        return null;
    }

    // methodDecl : (type | VOID) ident LEFTPAREN formPars? RIGHTPAREN block
    public override object VisitMethDeclaAST(MiniCSharpParser.MethDeclaASTContext context)
    {
        int idType = 7; //Tipo de datos del método cuando es void
        if (context.VOID() == null)
        {
            idType = (int) Visit(context.type());
        }
        IToken id = context.ident().Start;

        //Visita a los parametros del método
        if (context.formPars() != null)
        {
            Visit(context.formPars());
        }
        
        //Visita al bloque del método
        Visit(context.block());
        return null;
    }

    // formPars : type ident (COMMA type ident)*  
    public override object VisitFormParsAST(MiniCSharpParser.FormParsASTContext context)
    {
        Visit(context.type(0));
        Visit(context.ident(0));

        for (int i = 1; context.type().Length > i; i++)
        {
            Visit(context.type(i));
            Visit(context.ident(i));
        }
        return null;
    }

    //  type : ident (LEFTSBRACK RIGHTSBRACK)? 
    //Verificación de tipos
    public override object VisitTypeAST(MiniCSharpParser.TypeASTContext context)
    {
        TipoBasico.TiposBasicos result = TipoBasico.TiposBasicos.Error;
        if (context.ident().GetText().Equals("int"))
        {
            result = TipoBasico.TiposBasicos.Int;
        } 
        else if (context.ident().GetText().Equals("double"))
        {
            result = TipoBasico.TiposBasicos.Double;
        } 
        else if (context.ident().GetText().Equals("char"))
        {
            result = TipoBasico.TiposBasicos.Char;
        } 
        else if (context.ident().GetText().Equals("string"))
        {
            result = TipoBasico.TiposBasicos.String;
        }
        else if (context.ident().GetText().Equals("boolean"))
        {
            result = TipoBasico.TiposBasicos.Boolean;
        }
        else if (context.ident().GetText().Equals("int[]"))
        {
            result = TipoBasico.TiposBasicos.Int;
        }
        else if (context.ident().GetText().Equals("char[]"))
        {
            result = TipoBasico.TiposBasicos.Char;
        }
        else if (Clase.isClase(context.ident().GetText()))
        {
            result = TipoBasico.TiposBasicos.Error;
        }
        else
        {
            laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de tipos: Tipo: \"" + context.ident().GetText() + "\" no es un tipo válido." + showErrorPosition(context.ident().Start) + "\n";
        }
        return result;
    }

    // statement : designator (ASSIGN expr | LEFTPAREN actPars? RIGHTPAREN | PLUSPLUS | MINUSMINUS) SEMICOLON 
    public override object VisitAssignStatementAST(MiniCSharpParser.AssignStatementASTContext context)
    {
        Visit(context.designator());
        if (context.expr() != null)
        {
            Visit(context.expr());
        } else if (context.actPars() != null)
        {
            Visit(context.actPars());
        }

        return null;
    }

    // statement : IF LEFTPAREN condition RIGHTPAREN statement (ELSE statement)?  
    public override object VisitIfStatementAST(MiniCSharpParser.IfStatementASTContext context)
    {
        var tipoCondicion = (Tipo)Visit(context.condition());
        if (tipoCondicion == null)
        {
            // La expresión es nula, se reporta el error
            laCsTablaSimbolos.consola.SalidaConsola.Text += "Error: La condición del if es nula."+ showErrorPosition(context.condition().Start)  +"\n";
            return null;
        }
        if (!tipoCondicion.GetType().Equals(typeof(bool)))
        {
            // La expresión no es de tipo booleano, se reporta el error
            laCsTablaSimbolos.consola.SalidaConsola.Text += string.Format("Error: La condición debe ser de tipo bool, pero es de tipo {0}.\n", tipoCondicion.GetType().Name);
            return null;
        }
        Visit(context.condition());
        Visit(context.statement(0));
        if (context.statement().Length > 1)
        {
            Visit(context.statement(1));
        }
        // La condición es de tipo booleano, se devuelve el tipo
        return tipoCondicion;
    }

    // statement : FOR LEFTPAREN expr SEMICOLON condition? SEMICOLON statement? RIGHTPAREN statement
    public override object VisitForStatementAST(MiniCSharpParser.ForStatementASTContext context)
    {
        var tipoExpr = (Tipo)Visit(context.expr());

        // Verificar que el tipo del expr sea numérico
        if (tipoExpr == null || !(tipoExpr is int))
        {
            laCsTablaSimbolos.consola.SalidaConsola.Text += "Error: La expresión del for debe ser de tipo numérico.\n";
            return null;
        }

        // Si la condition existe, visita su subárbol y obtiene su tipo
        if (context.condition() != null)
        {
            var tipoCondicion = (Tipo)Visit(context.condition());

            // Verificar que el tipo de la condition sea bool
            if (tipoCondicion == null || !(tipoCondicion is bool))
            {
                laCsTablaSimbolos.consola.SalidaConsola.Text += "Error: La condición del for debe ser de tipo bool.\n";
                return null;
            }
        }

        // Si statement existe, visita su subárbol y obtiene su tipo
        if (context.statement() != null)
        {
            Visit(context.statement(0));
        }

        if (context.statement().Length > 1)
        {
            Visit(context.statement(1));
        }

        return null;
    }


    // statement : WHILE LEFTPAREN condition RIGHTPAREN statement
    public override object VisitWhileConditionStatementAST(MiniCSharpParser.WhileConditionStatementASTContext context)
    {
        var tipoCondicion = (Tipo)Visit(context.condition());
    
        // Verifica que el tipo de la condición sea bool
        if (tipoCondicion == null || !(tipoCondicion is bool))
        {
            laCsTablaSimbolos.consola.SalidaConsola.Text += "Error: La condición del while debe ser de tipo bool.\n";
            return null;
        }
    
        Visit(context.statement());
        return null;
    }


    // statement : BREAK SEMICOLON  
    public override object VisitBreakStatementAST(MiniCSharpParser.BreakStatementASTContext context)
    {
        // nothing to visit
        return null;
    }

    // statement : RETURN expr? SEMICOLON
    // modificar a futuro con implementacion de los metodos
    public override object VisitReturnStatementAST(MiniCSharpParser.ReturnStatementASTContext context)
    {
        if (context.expr() != null)
        {
            Visit(context.expr());
        }
        return null;
    }

    //  statement : READ LEFTPAREN designator RIGHTPAREN SEMICOLON    
    public override object VisitWhileNumberStatementAST(MiniCSharpParser.WhileNumberStatementASTContext context)
    {
        Visit(context.designator());
        return null;
    }

    // statement : WRITE LEFTPAREN expr (COMMA (INT | DOUBLE))? RIGHTPAREN SEMICOLON
    public override object VisitWriteNumberStatementAST(MiniCSharpParser.WriteNumberStatementASTContext context)
    {
        object exprType = Visit(context.expr());
        bool isIntOrDouble = exprType is int || exprType is double;

        if (!isIntOrDouble)
        {
            IToken token = context.expr().Start;
            laCsTablaSimbolos.consola.SalidaConsola.Text += $"\nSe esperaba una expresión de tipo int o double en la instrucción 'write', se encontró " + token.Text + "\n";
        }

        if (context.INT() != null)
        {
            if (!(exprType is int))
            {
                IToken token = context.INT().Symbol;
                laCsTablaSimbolos.consola.SalidaConsola.Text +=$"\nSe esperaba una expresión de tipo int después de la ',' en la instrucción 'write', se encontró " + token.Text + "\n";
            }
        }
        else if (context.DOUBLE() != null)
        {
            if (!(exprType is double))
            {
                IToken token = context.DOUBLE().Symbol;
                laCsTablaSimbolos.consola.SalidaConsola.Text += $"\nSe esperaba una expresión de tipo double después de la ',' en la instrucción 'write', se encontró " + token.Text + "\n";
            }
        }

        return null;
    }


    // statement : designator ADDMETHOD LEFTPAREN (designator | INT) (COMMA (designator | INT))* RIGHTPAREN SEMICOLON
    public override object VisitDesignAddMethStatementAST(MiniCSharpParser.DesignAddMethStatementASTContext context)
    {
        Visit(context.designator(0));
        if (context.INT(0) == null)
        {
            Visit(context.designator(1));
        }

        // DUDA CON ESTA PARTE ->  (COMMA (designator | INT))*
        if ( (context.INT(0) == null) && (context.designator().Length > 2))
        {
            for (int i = 2; i < context.designator().Length; i++)
            {
                Visit(context.designator(i));
            }
        }

        return null;
    }

    // statement : designator LENMETHOD LEFTPAREN RIGHTPAREN SEMICOLON 
    public override object VisitDesignLenMethStatementAST(MiniCSharpParser.DesignLenMethStatementASTContext context)
    {
        Visit(context.designator());
        return null;
    }

    // statement : designator DELMETHOD LEFTPAREN INT RIGHTPAREN SEMICOLON   
    public override object VisitDesignDelMethStatementAST(MiniCSharpParser.DesignDelMethStatementASTContext context)
    {
        Visit(context.designator());
        return null;
    }

    // statement : block
    public override object VisitBlockStatementAST(MiniCSharpParser.BlockStatementASTContext context)
    {
        laCsTablaSimbolos.openScope();
        Visit(context.block());
        laCsTablaSimbolos.CloseScope();
        return null;
    }

    // statement : SEMICOLON
    public override object VisitSemicolonStatementAST(MiniCSharpParser.SemicolonStatementASTContext context)
    {
        // nothing to visit
        return null;
    }

    // block : LEFTBRACK (varDecl | statement)* RIGHTBRACK
    public override object VisitBlockAST(MiniCSharpParser.BlockASTContext context)
    {
        for (int i = 0; i < context.varDecl().Length; i++)
        {
            Visit(context.varDecl(i));
        }

       
        for (int i = 0; i < context.statement().Length; i++)
        {
            Visit(context.statement(i));
        }
        
        return null;
    }

    // actPars : expr (COMMA expr)*
    // duda con la verificacion de tipos de los parametros de los metodos
    public override object VisitActParsAST(MiniCSharpParser.ActParsASTContext context)
    {
        int result = -1;
        Visit(context.expr(0));
        if (context.expr().Length > 1)
        {
            for (int i = 1; i < context.expr().Length; i++)
            {
                Visit(context.expr(i));
            }
        }
        return result;
    }

    // condition : condTerm (OR condTerm)*
    // falta implementar verificacion de tipos
    public override object VisitConditionAST(MiniCSharpParser.ConditionASTContext context)
    {
        Visit(context.condTerm(0));
        if (context.condTerm().Length > 1)
        {
            for (int i = 1; i < context.condTerm().Length; i++)
            {
                Visit(context.condTerm(i));
            }
        }
        return null;
    }

    // condTerm : condFact (AND condFact)*
    // falta implementar verificacion de tipos
    public override object VisitCondTermAST(MiniCSharpParser.CondTermASTContext context)
    {
        Visit(context.condFact(0));
        if (context.condFact().Length > 1)
        {
            for (int i = 1; i < context.condFact().Length; i++)
            {
                Visit(context.condFact(i));
            }
        }
        return null;
    }

    // condFact : expr (RELOP | EQOP) expr
    // falta implementar verificacion de tipos
    public override object VisitCondFactAST(MiniCSharpParser.CondFactASTContext context)
    {
        Visit(context.expr(0));
        Visit(context.relop());
        Visit(context.expr(1));
        return null;
    }


    // cast : LEFTPAREN type RIGHTPAREN  
    // falta implementar verificacion de tipos
    public override object VisitCastAST(MiniCSharpParser.CastASTContext context)
    {
        int type = (int) Visit(context.type());
        //falta verificar
        return null;
    }

    // expr : MINUS? cast? term (addop term)* 
    // falta implementar verificacion de tipos
    public override object VisitExprAST(MiniCSharpParser.ExprASTContext context)
    {
        if (context.cast() != null)
        {
            Visit(context.cast());
        }
        Visit(context.term(0));
    
        for (int i = 1; i < context.term().Length; i++)
        {
            Visit(context.addop(i - 1));
            Visit(context.term(i));
        }
        return null;
    }



    // term : factor (mulop factor)* 
    // Verificar tipos
    public override object VisitTermAST(MiniCSharpParser.TermASTContext context)
    {
        Visit(context.factor(0));
        for (int i = 1; i < context.factor().Length; i++)
        {
            Visit(context.mulop(i - 1));
            Visit(context.factor(i));
        }
        return null;
    }


    // factor : designator (LEFTPAREN (actPars)? RIGHTPAREN)?
    // falta implementar verificacion de tipos
    public override object VisitDesignFactorAST(MiniCSharpParser.DesignFactorASTContext context)
    {
        Visit(context.designator());
        if (context.actPars() != null)
        {
            Visit(context.actPars());
        }
        return null;
    }

    // factor : CHARCONST
    public override object VisitCharconstFactorAST(MiniCSharpParser.CharconstFactorASTContext context)
    {
        return TipoBasico.TiposBasicos.Char; 
    }

    // factor : STRINGCONST
    public override object VisitStrconstFactorAST(MiniCSharpParser.StrconstFactorASTContext context)
    {
        // nothing to visit
        return TipoBasico.TiposBasicos.String; 
    }

    // factor : INT 
    public override object VisitIntFactorAST(MiniCSharpParser.IntFactorASTContext context)
    {
        // nothing to visit
        return TipoBasico.TiposBasicos.Int; 
    }

    // factor : DOUBLE
    public override object VisitDoubFactorAST(MiniCSharpParser.DoubFactorASTContext context)
    {
        // nothing to visit
        return TipoBasico.TiposBasicos.Double;
    }

    // factor : BOOL
    public override object VisitBoolFactorAST(MiniCSharpParser.BoolFactorASTContext context)
    {
        // nothing to visit
        return TipoBasico.TiposBasicos.Boolean; 
    }

    // factor : NEW ident
    public override object VisitNewIdentFactorAST(MiniCSharpParser.NewIdentFactorASTContext context)
    {
        Visit(context.ident());
        return null;
    }

    // factor : LEFTPAREN expr RIGHTPAREN 
    public override object VisitExprInparentFactorAST(MiniCSharpParser.ExprInparentFactorASTContext context)
    {
        return Visit(context.expr());;
    }

    // designator : ident (DOT ident | LEFTSBRACK expr RIGHTSBRACK)* 
    public override object VisitDesignatorAST(MiniCSharpParser.DesignatorASTContext context)
    {
        Visit(context.ident(0));
        for (int i = 1; i < context.ident().Length; i++)
        {
            Visit(context.ident(i));
        }
        
        for (int i = 0; i < context.expr().Length; i++)
        {
            Visit(context.expr(i));
        }
        return null;
    }

    // ident : INT_ID
    public override object VisitIntIdIdentAST(MiniCSharpParser.IntIdIdentASTContext context)
    {
        // nothing to visit
        return null;
    }

    // ident : CHAR_ID
    public override object VisitCharIdIdentAST(MiniCSharpParser.CharIdIdentASTContext context)
    {
        // nothing to visit
        return null;
    }

    // ident : DOUBLE_ID
    public override object VisitDoubIdIdentAST(MiniCSharpParser.DoubIdIdentASTContext context)
    {
        // nothing to visit
        return null;
    }

    // ident : BOOL_ID
    public override object VisitBoolIdIdentAST(MiniCSharpParser.BoolIdIdentASTContext context)
    {
        // nothing to visit
        return null;
    }

    // ident : STRING_ID
    public override object VisitStrIdIdentAST(MiniCSharpParser.StrIdIdentASTContext context)
    {
        // nothing to visit
        return null;
    }

    // ident : IDENTIFIER
    public override object VisitIdentifierIdentAST(MiniCSharpParser.IdentifierIdentASTContext context)
    {
        // nothing to visit
        return null;
    }

    // ident : LIST
    public override object VisitListIdentAST(MiniCSharpParser.ListIdentASTContext context)
    {
        // nothing to visit
        return null;
    }
    
    private void ReportError(string message, IToken token)
    {
        laCsTablaSimbolos.consola.SalidaConsola.Text += $"Line {token.Line}, column {token.Column}: error: {message}";
    }

}