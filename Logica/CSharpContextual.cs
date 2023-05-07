﻿using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using miniChart.Logica.TypeManager;

namespace miniChart.Logica;

public class CSharpContextual : MiniCSharpParserBaseVisitor<Object>
{
    private CSTablaSimbolos laCsTablaSimbolos;

    public CSharpContextual()
    {
        laCsTablaSimbolos = new CSTablaSimbolos();
    }
    private String ShowErrorPosition(IToken t)
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
        IToken id = (IToken) Visit(context.ident());
        laCsTablaSimbolos.openScope();
        if (Clase.IsClase(id.Text))
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
            IToken id = (IToken) Visit(context.ident(i));
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
                Clase searched = laCsTablaSimbolos.buscarObjetoTipo<Clase>(context.type().GetText());
                if (searched != null)
                {
                    TipoClase tipoClase = new TipoClase(id, context.type().GetText());
                    laCsTablaSimbolos.insertar(tipoClase);
                }
                else
                {
                    laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de tipos: Tipo: \"" + context.type().GetText() + "\" no es un tipo válido." + ShowErrorPosition(context.type().Start) + "\n";
                }

            }
        }

        return null;
    }

    // classDecl : CLASS ident LEFTBRACK (varDecl)* RIGHTBRACK  
    public override object VisitClassDeclaAST(MiniCSharpParser.ClassDeclaASTContext context)
    {
        IToken id = context.ident().Start;
        if (Clase.IsClase(id.Text))
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
        int idType = (int)Metodo.TipoMetodo.Void;
        if (context.VOID() == null)
        {
            idType = (int) Visit(context.type());
        }
        IToken id = (IToken) Visit(context.ident());
        Metodo metodo = new Metodo(id, idType);
        
        //Visita a los parametros del método
        if (context.formPars() != null)
        {
            metodo.parametros = (LinkedList<object>)Visit(context.formPars());
            metodo.cantidadParam = metodo.parametros.Count;
        }
        
        //Visita al bloque del método
        Visit(context.block());
        laCsTablaSimbolos.insertar(metodo);
        return null;
    }

    // formPars : type ident (COMMA type ident)*  
    public override object VisitFormParsAST(MiniCSharpParser.FormParsASTContext context)
    {
        LinkedList<Object> parametros = new LinkedList<object>(); 
        for (int i = 0; i < context.type().Length; i++)
        {
            int idType = (int)Visit(context.type(i));
            IToken id = (IToken) Visit(context.ident(i));

            if (TipoBasico.isTipoBasico(context.type(i).GetText()))
            {
                TipoBasico tb = new TipoBasico(id, idType);
                parametros.AddLast(tb);
            } else if (TipoClase.IsTipoClase(context.type(i).GetText()))
            {
                TipoClase tc = new TipoClase(id, context.type(i).GetText());
                parametros.AddLast(tc);
            }
            else if (Arreglo.isTipoArreglo(context.type(i).GetText()))
            {
                Arreglo arr = new Arreglo(id, idType);
                parametros.AddLast(arr);
            }
            else
            {
                laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de tipos: Tipo: \"" + context.type(i).GetText() + "\" no es un tipo válido." + ShowErrorPosition(context.type(i).Start) + "\n";
            }
        }
        return parametros;
    }

    //  type : ident (LEFTSBRACK RIGHTSBRACK)? 
    public override object VisitTypeAST(MiniCSharpParser.TypeASTContext context)
    {
        TipoBasico.TiposBasicos result = TipoBasico.TiposBasicos.Error;
        if (context.ident().GetText().Equals("int") || context.ident().GetText().Equals("int[]"))
        {
            result = TipoBasico.TiposBasicos.Int;
        } 
        else if (context.ident().GetText().Equals("double"))
        {
            result = TipoBasico.TiposBasicos.Double;
        } 
        else if (context.ident().GetText().Equals("char") || context.ident().GetText().Equals("char[]"))
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
        else if (Clase.IsClase(context.ident().GetText()))
        {
            result = TipoBasico.TiposBasicos.Error;
        }
        else
        {
            laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de tipos: Tipo: \"" + context.ident().GetText() + "\" no es un tipo válido." + ShowErrorPosition(context.ident().Start) + "\n";
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
            laCsTablaSimbolos.consola.SalidaConsola.Text += "Error: La condición del if es nula."+ ShowErrorPosition(context.condition().Start)  +"\n";
            return null;
        }
        if (!tipoCondicion.GetType().Equals(typeof(bool)))
        {
            // La expresión no es de tipo booleano, se reporta el error
            laCsTablaSimbolos.consola.SalidaConsola.Text += string.Format("Error: La condición debe ser de tipo bool, pero es de tipo {0}.\n", tipoCondicion.GetType().Name);
            return null;
        }
        laCsTablaSimbolos.openScope();

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

        laCsTablaSimbolos.openScope();
        Visit(context.expr());
        if (context.condition() != null)
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
        laCsTablaSimbolos.CloseScope();

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
        
        laCsTablaSimbolos.openScope();
        Visit(context.condition());
        Visit(context.statement());
        laCsTablaSimbolos.CloseScope();
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
            ReportError($"Se esperaba una expresión de tipo int o double en la instrucción 'write', se encontró '{exprType}'", token);
        }

        if (context.INT() != null)
        {
            if (!(exprType is int))
            {
                IToken token = context.INT().Symbol;
                ReportError($"Se esperaba una expresión de tipo int después de la ',' en la instrucción 'write', se encontró '{exprType}'", token);
            }
        }
        else if (context.DOUBLE() != null)
        {
            if (!(exprType is double))
            {
                IToken token = context.DOUBLE().Symbol;
                ReportError($"Se esperaba una expresión de tipo double después de la ',' en la instrucción 'write', se encontró '{exprType}'", token);
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
        laCsTablaSimbolos.openScope();
        for (int i = 0; i < context.varDecl().Length; i++)
        {
            Visit(context.varDecl(i));
        }

       
        for (int i = 0; i < context.statement().Length; i++)
        {
            Visit(context.statement(i));
        }
        laCsTablaSimbolos.CloseScope();
        
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
    public override object VisitConditionAST(MiniCSharpParser.ConditionASTContext context)
    {
        // Obtiene el tipo de la primera expresión
        int firstType = (int) Visit(context.condTerm(0));
    
        // Verifica que todas las expresiones tengan el mismo tipo
        for (int i = 1; i < context.condTerm().Length; i++)
        {
            int currentType = (int) Visit(context.condTerm(i));
        
            if (firstType != currentType)
            {
                // Si los tipos no son iguales en las expresiones, entonces hay un error de tipo en la condición
                laCsTablaSimbolos.consola.SalidaConsola.Text += string.Format("Error: La condición deben ser iguales en tipo. {0}.\n", "Primera Condicion: ",firstType, ", Segunda Condición: ",currentType);
            }
        }
    
        return null;
    }

    // condTerm : condFact (AND condFact)*
    public override object VisitCondTermAST(MiniCSharpParser.CondTermASTContext context)
    {
        // Obtiene el tipo de la primera expresión
        int firstType = (int) Visit(context.condFact(0));
    
        // Verifica que todas las expresiones tengan el mismo tipo
        for (int i = 1; i < context.condFact().Length; i++)
        {
            int currentType = (int) Visit(context.condFact(i));
        
            if (firstType != currentType)
            {
                // Si los tipos no son iguales, entonces hay un error de tipo en la condición
                laCsTablaSimbolos.consola.SalidaConsola.Text += string.Format("Error: La condición deben ser iguales en tipo. {0}.\n", "Primera Condicion: ",firstType, ", Segunda Condición: ",currentType);
            }
        }
    
        return firstType;
    }

    // condFact : expr (RELOP | EQOP) expr
    public override object VisitCondFactAST(MiniCSharpParser.CondFactASTContext context)
    {
        // Obtiene el tipo de la primera expresión
        var firstType = (int) Visit(context.expr(0));
    
        // Obtiene el tipo de la segunda expresión
        int secondType = (int) Visit(context.expr(1));
    
        // Verifica que las expresiones tengan el mismo tipo
        if (firstType != secondType)
        {
            // Si los tipos no son iguales, entonces hay un error de tipo en la condición
            laCsTablaSimbolos.consola.SalidaConsola.Text += string.Format("Error: La condición deben ser iguales en tipo. {0}.\n", "Primera Condicion: ",firstType, ", Segunda Condición: ",secondType);
        }
    
        // Retorna el tipo de la primera expresión, que es el tipo de la expresión condicional
        return firstType;
    }

    // cast : LEFTPAREN type RIGHTPAREN  
    // falta verificacion de tipos
    public override object VisitCastAST(MiniCSharpParser.CastASTContext context)
    {

        int type = (int) Visit(context.type());
        //falta verificar
        return null;
    }

    // expr : MINUS? cast? term (addop term)* 
    public override object VisitExprAST(MiniCSharpParser.ExprASTContext context)
    {
        TipoBasico.TiposBasicos tipo;
    
        if (context.MINUS() != null)
        {
            tipo = (TipoBasico.TiposBasicos)Visit(context.term(0));
            if (tipo != TipoBasico.TiposBasicos.Int && tipo != TipoBasico.TiposBasicos.Double)
            {
                laCsTablaSimbolos.consola.SalidaConsola.Text += "Operador unario '-' solo puede ser aplicado a operandos de tipo int o double.";
            }
        }
    
        if (context.cast() != null)
        {
            tipo = (TipoBasico.TiposBasicos)Visit(context.cast());
        }
        else
        {
            tipo = (TipoBasico.TiposBasicos)Visit(context.term(0));
        }
    
        for (int i = 1; i < context.term().Length; i++)
        {
            TipoBasico.TiposBasicos termTipo = (TipoBasico.TiposBasicos)Visit(context.term(i));
            if (tipo != termTipo)
            {
                laCsTablaSimbolos.consola.SalidaConsola.Text += "Tipos incompatibles en expresión aritmética.";
            }
            tipo = (TipoBasico.TiposBasicos)Visit(context.addop(i - 1));
        }
        return tipo;
    }


    // term : factor (mulop factor)* 
    // Verificar tipos
    public override object VisitTermAST(MiniCSharpParser.TermASTContext context)
    {
        TipoBasico.TiposBasicos tipoAnterior = (TipoBasico.TiposBasicos)Visit(context.factor(0));
        for (int i = 1; i < context.factor().Length; i++)
        {
            TipoBasico.TiposBasicos tipoActual = (TipoBasico.TiposBasicos)Visit(context.factor(i));
            if (tipoAnterior != TipoBasico.TiposBasicos.Int && tipoAnterior != TipoBasico.TiposBasicos.Double)
            {
                // Si el tipo anterior no es int ni double, hay un error de tipos
                throw new Exception("Error de tipos: se esperaba un valor numérico");
            }
            if (tipoActual != TipoBasico.TiposBasicos.Int && tipoActual != TipoBasico.TiposBasicos.Double)
            {
                // Si el tipo actual no es int ni double, hay un error de tipos
                throw new Exception("Error de tipos: se esperaba un valor numérico");
            }
            if (tipoAnterior != tipoActual)
            {
                // Si los tipos son diferentes, hay un error de tipos
                throw new Exception("Error de tipos: los operandos no son del mismo tipo");
            }
            Visit(context.mulop(i - 1));
            tipoAnterior = tipoActual;
        }
        return tipoAnterior;
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
        return TipoBasico.TiposBasicos.String; 
    }

    // factor : INT 
    public override object VisitIntFactorAST(MiniCSharpParser.IntFactorASTContext context)
    {
        return TipoBasico.TiposBasicos.Int; 
    }

    // factor : DOUBLE
    public override object VisitDoubFactorAST(MiniCSharpParser.DoubFactorASTContext context)
    {
        return TipoBasico.TiposBasicos.Double;
    }

    // factor : BOOL
    public override object VisitBoolFactorAST(MiniCSharpParser.BoolFactorASTContext context)
    {
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
        return context.INT_ID().Symbol;
    }

    // ident : CHAR_ID
    public override object VisitCharIdIdentAST(MiniCSharpParser.CharIdIdentASTContext context)
    {
        return context.CHAR_ID().Symbol;
    }

    // ident : DOUBLE_ID
    public override object VisitDoubIdIdentAST(MiniCSharpParser.DoubIdIdentASTContext context)
    {
        return context.DOUBLE_ID().Symbol;
    }

    // ident : BOOL_ID
    public override object VisitBoolIdIdentAST(MiniCSharpParser.BoolIdIdentASTContext context)
    {
        return context.BOOL_ID().Symbol;
    }

    // ident : STRING_ID
    public override object VisitStrIdIdentAST(MiniCSharpParser.StrIdIdentASTContext context)
    {
        return context.STRING_ID().Symbol;
    }

    // ident : IDENTIFIER
    public override object VisitIdentifierIdentAST(MiniCSharpParser.IdentifierIdentASTContext context)
    {
        return context.IDENTIFIER().Symbol;
    }

    // ident : LIST
    public override object VisitListIdentAST(MiniCSharpParser.ListIdentASTContext context)
    {
        return context.LIST().Symbol;
    }
    
    private void ReportError(string message, IToken token)
    {
        laCsTablaSimbolos.consola.SalidaConsola.Text += $"Line {token.Line}, column {token.Column}: error: {message}";
    }

}