using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using miniChart.Logica.TypeManager;

namespace miniChart.Logica;

public class CSharpContextual : MiniCSharpParserBaseVisitor<Object>
{
    public CSTablaSimbolos laCsTablaSimbolos;

    public CSharpContextual()
    {
        laCsTablaSimbolos = new CSTablaSimbolos();
    }
    public bool isMultiple(string type)
    {
        switch (type)
        {
            case "==" : return true;
            case "!=" : return true;
            default: return false;
        }
    }
    
    private String showType(int type){
        switch(type){
            case 0: return "int";
            case 1: return "double";
            case 2: return "void";
            default: return "none";
        }
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
        LinkedList<Object> variables = new LinkedList<object>();
        Tipo variable;
        //Se recorren todas las variables (cuando se declaran de un mismo tipo separadas por coma)
        for (int i = 0; i < context.ident().Length; i++)
        {
            int idType = (int)Visit(context.type()); //Se verifica el tipo de datos de la variable
            IToken id = (IToken) Visit(context.ident(i));
            Tipo varType = laCsTablaSimbolos.buscarObjetoTipo<Tipo>(id.Text);
            if (varType == null)
            {
                if (TipoBasico.isTipoBasico(context.type().GetText()))
                {
                    variable = new TipoBasico(id, idType);
                    variables.AddLast(variable);
                    laCsTablaSimbolos.insertar(variable);

                }

                else if (Arreglo.isTipoArreglo(context.type().GetText()))
                {
                    variable = new Arreglo(id, idType);
                    variables.AddLast(variable);
                    laCsTablaSimbolos.insertar(variable);
                }

                else if (TipoClase.IsTipoClase(context.type().GetText()))
                {
                    Clase searched = laCsTablaSimbolos.buscarObjetoTipo<Clase>(context.type().GetText());
                    if (searched != null)
                    {
                        variable = new TipoClase(id, context.type().GetText());
                        variables.AddLast(variable);
                        laCsTablaSimbolos.insertar(variable);
                    }
                    else
                    {
                        laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de tipos: Tipo: \"" + context.type().GetText() + "\" no es un tipo válido." + ShowErrorPosition(context.type().Start) + "\n";
                    }

                }
                
            }
            else
            {
                laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de declaracion: El identificador: \"" + id.Text + "\" ya ha sido declarado." + ShowErrorPosition(id) + "\n";
 
            }
            
        }

        return variables;
    }

    // classDecl : CLASS ident LEFTBRACK (varDecl)* RIGHTBRACK  
    public override object VisitClassDeclaAST(MiniCSharpParser.ClassDeclaASTContext context)
    {
        IToken id = context.ident().Start;
        Clase claseBuscada = laCsTablaSimbolos.buscarObjetoTipo<Clase>(id.Text);
        
        if (Clase.IsClase(id.Text) && claseBuscada == null)
        {
            Clase clase = new Clase(id);
            laCsTablaSimbolos.openScope();
            //Se recorren las declaraciones de las variables de la clase
            for (int i = 0; i < context.varDecl().Length; i++)
            {
                LinkedList<object> classVars = (LinkedList<object>)Visit(context.varDecl(i));
                foreach (Tipo variable in classVars)
                {
                    if (variable is TipoBasico)
                    {
                        clase.addVariable(variable);
                    }
                    else
                    {
                        laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de declaracion: El tipo de variable \"" + variable.tipo + "\" no es permitido en la clase." + ShowErrorPosition(variable.tok) + "\n";

                    }
                }
            }
            laCsTablaSimbolos.CloseScope();
            laCsTablaSimbolos.insertar(clase);
        }
        else
        {
            laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de declaracion: La clase: \"" + id.Text + "\" ya ha sido declarada." + ShowErrorPosition(id) + "\n";
        }
        return null;
    }

    // methodDecl : (type | VOID) ident LEFTPAREN formPars? RIGHTPAREN block
    public override object VisitMethDeclaAST(MiniCSharpParser.MethDeclaASTContext context)
    {
        IToken id = (IToken) Visit(context.ident());
        Metodo metodoBuscado = laCsTablaSimbolos.buscarObjetoTipo<Metodo>(id.Text);
        if (metodoBuscado == null)
        {
            int idType = (int)Metodo.TipoMetodo.Void;
            if (context.VOID() == null)
            {
                idType = (int) Visit(context.type());
            }
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
        }
        else
        {
            laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de declaracion: El metodo: \"" + id.Text + "\" ya ha sido declarado." + ShowErrorPosition(id) + "\n";
        }
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
        var idType = Visit(context.designator());
        if (idType is TipoBasico.TiposBasicos type && (int)type == (int)TipoBasico.TiposBasicos.Error)
        {
            laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de alcances, identificador \"" + context.designator().GetText() + "\" no declarado en asignación." + ShowErrorPosition(context.designator().Start) +"\n";
        }
            
        if (context.expr() != null)
        {
            var exprType = Visit(context.expr());
            {
                if (exprType is TipoBasico.TiposBasicos && !exprType.Equals(idType) || exprType is string type3 && !type3.Equals(idType))
                    laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de tipos: \""+ idType + "\" y \"" + exprType + "\" no son compatibles." + ShowErrorPosition(context.ASSIGN().Symbol) +"\n";

            }
                
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
            //laCsTablaSimbolos.consola.SalidaConsola.Text += string.Format("Error: La condición debe ser de tipo bool, pero es de tipo {0}.\n", tipoCondicion.GetType().Name);
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
                //laCsTablaSimbolos.consola.SalidaConsola.Text += "Error: La condición del for debe ser de tipo bool.\n";
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
            //laCsTablaSimbolos.consola.SalidaConsola.Text += "Error: La condición del while debe ser de tipo bool.\n";
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

    // statement : block
    public override object VisitBlockStatementAST(MiniCSharpParser.BlockStatementASTContext context)
    {
        laCsTablaSimbolos.openScope();
        Visit(context.block());
        laCsTablaSimbolos.CloseScope();
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
        var type = Visit(context.expr(0));
        if (context.expr().Length > 1)
        {
            for (int i = 1; i < context.expr().Length; i++)
            {
                Visit(context.expr(i));
            }
        }
        return type;
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

    // condFact : expr RELOP expr
    public override object VisitCondFactAST(MiniCSharpParser.CondFactASTContext context)
    {
        var type1 = (int) Visit(context.expr(0));
        var type2 = (int) Visit(context.expr(1));
        if (!isMultiple(context.relop().GetText()) && type1 != type2)
        {
            laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de tipos " + TipoBasico.showTipo(type1) + " y " + TipoBasico.showTipo(type2) + " no son compatibles. Solo se aceptan de tipo INT" + ShowErrorPosition(context.expr(0).Start) + "\n";
        }
        else if (isMultiple(context.relop().GetText()) && type1 != type2)
        {
            laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de tipos " + TipoBasico.showTipo(type1) + " y " + TipoBasico.showTipo(type2) + " no son compatibles." + ShowErrorPosition(context.expr(0).Start) + "\n";

        }
        return null;
    }


    // cast : LEFTPAREN type RIGHTPAREN  
    // retorna el valor numerico del tipo visitado, si es clase retorna error = 6
    public override object VisitCastAST(MiniCSharpParser.CastASTContext context)
    {
        return Visit(context.type());
    }

    // expr : MINUS? cast? term (addop term)* 
    // falta verificacion del CASTEO
    public override object VisitExprAST(MiniCSharpParser.ExprASTContext context)
    {
        
        if (context.cast() != null)
        {
            var casteo = (int)Visit(context.cast());
            if (casteo == 6)
            {
                laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de expresión, despues se explica";
            }
            Visit(context.cast());
        }
        /*
         *
         * 
         * DUDA CON EL CASTEO !!!!! SE DEBE VERIFICAR SI EL TIPO DEL TERM ES COMPATIBLE CON EL CASTEO Y RETORNAR EL TIPO DEL CAST
         *
         * 
         */
        var type = Visit(context.term(0));
    
        for (int i = 1; i < context.term().Length; i++)
        {
            var type2 = Visit(context.term(i));
            if (type != type2)
            {
                laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de tipos " + type + " y " + type2 + " no son compatibles. Solo se aceptan de tipo int" + ShowErrorPosition(context.term(0).Start) + "\n";
            }
            if (!isMultiple(context.addop(i - 1).GetText()))
            {
                laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de operador " + context.addop(i-1).GetText() + " no es permtido en este caso. " + ShowErrorPosition(context.addop(0).Start) + "\n";
            }
            Visit(context.addop(i - 1));
        }
        return type;
    }
    
    // term : factor (mulop factor)* 
    // retorna el type del primer factor, sino null en teoria
    public override object VisitTermAST(MiniCSharpParser.TermASTContext context)
    {
        // tipo de factor
        var type = Visit(context.factor(0));
        for (int i = 1; i < context.factor().Length; i++)
        {
            var type2 = Visit(context.factor(i));
            if (type != type2)
            {
                laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de tipos " + type + " y " + type2 + " no son compatibles. Solo se aceptan de tipo INT" + ShowErrorPosition(context.factor(0).Start) + "\n";
            } 
            if (!isMultiple(context.mulop(i - 1).GetText()))
            {
                laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de operando " + context.mulop(i-1).GetText() + " no es permtido en este caso. " + ShowErrorPosition(context.mulop(0).Start) + "\n";
            }
            Visit(context.mulop(i - 1));
        }
        return type;
    }

    // factor : designator (LEFTPAREN (actPars)? RIGHTPAREN)?
    // retorna el tipo de designator
    public override object VisitDesignFactorAST(MiniCSharpParser.DesignFactorASTContext context)
    {
        var type = Visit(context.designator());
        if (context.actPars() != null)
        {
            Visit(context.actPars());
        }
        return type;
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
    // retorna el IToken
    public override object VisitNewIdentFactorAST(MiniCSharpParser.NewIdentFactorASTContext context)
    {
        IToken token = (IToken)Visit(context.ident());
        string type = token.Text;
        if (!TipoClase.IsTipoClase(type) && !Arreglo.isTipoArreglo(type))
        { 
            laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de tipos, se esperaba una el nombre de una clase o un array, se encontró " + token.Text + "\n" + ShowErrorPosition(context.ident().Start)  +"\n";
            return null;
        }

        switch (type)
        {
            case "int[]":
                return TipoBasico.TiposBasicos.Int;
            case "char[]":
                return TipoBasico.TiposBasicos.Char;
            default:
            {
                Clase newClase = laCsTablaSimbolos.buscarObjetoTipo<Clase>(type);
                if (newClase != null)
                {
                    return newClase.tok.Text;
                }
                break;
            }
        }
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
        Tipo varType = null;

        for (int i = 0; i < context.ident().Length; i++)
        {
            var idToken = (IToken)Visit(context.ident(i));
            string id = idToken.Text;

            if (i == 0)
            {
                varType = laCsTablaSimbolos.buscarObjetoTipo<Tipo>(id);
            }
            else
            {
                if (varType is TipoClase claseType)
                {
                    Clase clase = laCsTablaSimbolos.buscarObjetoTipo<Clase>(claseType.tipoDato);
                    if (id != null)
                    {
                        varType = clase.buscarAtributo<TipoBasico>(id);
                        if (varType == null)
                        {
                            laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de alcances, atributo \"" + id + "\" no encontrado en la clase \"" + clase.tok.Text + "\"." + ShowErrorPosition(idToken) + "\n";
                            return TipoBasico.TiposBasicos.Error;
                        }
                    }
                    return ((Clase)varType).tok.Text;
                }
            }

        }
        if (varType == null)
        {
            laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de alcances, identificador \"" + context.ident(0).GetText() + "\" no declarado en asignación." + ShowErrorPosition(context.ident(0).Start) + "\n";
            return TipoBasico.TiposBasicos.Error;
        }

        if (varType is TipoBasico basicoType)
        {
            return basicoType.tipoDato;
        }

        if (varType is TipoClase claseTypo)
        {
            return claseTypo.tipoDato;
        }

        if (varType is Arreglo arregloType)
        {
            if (context.INT() != null)
            {
                return TipoBasico.TiposBasicos.Int;
            }
    
            var indexIdent = context.IDENTIFIER(0);
            if (indexIdent != null)
            {
                var indexType = laCsTablaSimbolos.buscarObjetoTipo<TipoBasico>(indexIdent.GetText());
                if (indexType != null && indexType.tipoDato == (int)TipoBasico.TiposBasicos.Int)
                {
                    return arregloType.tipoDato;
                }
        
                laCsTablaSimbolos.consola.SalidaConsola.Text += "Error: el índice del arreglo debe ser de tipo int" + ShowErrorPosition(indexIdent.Symbol) + "\n";
                return TipoBasico.TiposBasicos.Error;
            }
    
            return arregloType.tipoDato;
        }
        //laCsTablaSimbolos.consola.SalidaConsola.Text += "Error de alcances, identificador \"" + context.ident(0).GetText() + "\" no declarado en asignación." + ShowErrorPosition(context.ident(0).Start) + "\n";
        return TipoBasico.TiposBasicos.Error;
    }

    // ident : INT_ID
    // retorna IToken
    public override object VisitIntIdIdentAST(MiniCSharpParser.IntIdIdentASTContext context)
    {
        return TipoBasico.TiposBasicos.Int;
    }

    // ident : CHAR_ID
    // retorna IToken
    public override object VisitCharIdIdentAST(MiniCSharpParser.CharIdIdentASTContext context)
    {
        return TipoBasico.TiposBasicos.Char;
    }

    // ident : DOUBLE_ID
    // retorna IToken
    public override object VisitDoubIdIdentAST(MiniCSharpParser.DoubIdIdentASTContext context)
    {
        return TipoBasico.TiposBasicos.Double;
    }

    // ident : BOOL_ID
    // retorna IToken
    public override object VisitBoolIdIdentAST(MiniCSharpParser.BoolIdIdentASTContext context)
    {
        return TipoBasico.TiposBasicos.Boolean;
    }

    // ident : STRING_ID
    // retorna IToken
    public override object VisitStrIdIdentAST(MiniCSharpParser.StrIdIdentASTContext context)
    {
        return TipoBasico.TiposBasicos.String;
    }

    // ident : IDENTIFIER
    // retorna IToken
    public override object VisitIdentifierIdentAST(MiniCSharpParser.IdentifierIdentASTContext context)
    {
        return context.IDENTIFIER().Symbol;
    }

    // ident : LIST
    // retorna IToken
    public override object VisitListIdentAST(MiniCSharpParser.ListIdentASTContext context)
    {
        return context.LIST().Symbol;
    }

    public override object VisitRelop(MiniCSharpParser.RelopContext context)
    {
        return context.GetChild(0);
    }

    public override object VisitAddop(MiniCSharpParser.AddopContext context)
    {
        return context.GetChild(0);
    }

    public override object VisitMulop(MiniCSharpParser.MulopContext context)
    {
        return context.GetChild(0);
    }
}