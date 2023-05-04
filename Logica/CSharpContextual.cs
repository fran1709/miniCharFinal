using System;

namespace miniChart.Logica;

public class CSharpContextual : MiniCSharpParserBaseVisitor<Object>
{
    // program : (using)* CLASS ident LEFTBRACK (varDecl | classDecl | methodDecl)* RIGHTBRACK EOF
    public override object VisitProgramAST(MiniCSharpParser.ProgramASTContext context)
    {
        for (int i = 0; i < context.@using().Length; i++)
        {
            Visit(context.@using(i));
        }
        
        Visit(context.ident()); //Agregar la clase a la tabla con este ident 
      
        for (int i = 0; i < context.varDecl().Length; i++)
        {
            Visit(context.varDecl(i));
        }  
          
        for (int i = 0; i < context.classDecl().Length; i++)
        {
            Visit(context.classDecl(i));
        }
          
        for (int i = 0; i < context.methodDecl().Length; i++)
        {
            Visit(context.methodDecl(i));
        }
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
        Visit(context.type());
        Visit(context.ident(0));
        for (int i = 1; i < context.ident().Length; i++)
        {
            Visit(context.ident(i));
        }
        return null;
    }

    // classDecl : CLASS ident LEFTBRACK (varDecl)* RIGHTBRACK  
    public override object VisitClassDeclaAST(MiniCSharpParser.ClassDeclaASTContext context)
    {
        Visit(context.ident());
        for (int i = 0; i < context.varDecl().Length; i++)
        {
            Visit(context.varDecl(i));
        }
        return null;
    }

    // methodDecl : (type | VOID) ident LEFTPAREN formPars? RIGHTPAREN block
    public override object VisitMethDeclaAST(MiniCSharpParser.MethDeclaASTContext context)
    {
        if (context.VOID() == null)
        {
            Visit(context.type());
        }
        Visit(context.ident());
        if (context.formPars() != null)
        {
            Visit(context.formPars());
        }
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
    public override object VisitTypeAST(MiniCSharpParser.TypeASTContext context)
    {
        Visit(context.ident());
        return null;
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
        Visit(context.condition());
        Visit(context.statement(0));
        if (context.statement().Length > 1)
        {
            Visit(context.statement(1));
        }
        return null;
    }

    // statement : FOR LEFTPAREN expr SEMICOLON condition? SEMICOLON statement? RIGHTPAREN statement
    public override object VisitForStatementAST(MiniCSharpParser.ForStatementASTContext context)
    {
        Visit(context.expr());
        if (context.condition() != null)
        {
            Visit(context.condition());
        }

        if (context.statement()!= null)
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
        Visit(context.condition());
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
        Visit(context.expr());
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
        Visit(context.block());
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
    public override object VisitActParsAST(MiniCSharpParser.ActParsASTContext context)
    {
        Visit(context.expr(0));
        if (context.expr().Length > 1)
        {
            for (int i = 1; i < context.expr().Length; i++)
            {
                Visit(context.expr(i));
            }
        }
        return null;
    }

    // condition : condTerm (OR condTerm)* 
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

    // condFact : expr relop expr 
    public override object VisitCondFactAST(MiniCSharpParser.CondFactASTContext context)
    {
        Visit(context.expr(0));
        Visit(context.relop());
        Visit(context.expr(1));
        return null;
    }

    // cast : LEFTPAREN type RIGHTPAREN  
    public override object VisitCastAST(MiniCSharpParser.CastASTContext context)
    {
        Visit(context.type());
        return null;
    }

    // expr : MINUS? cast? term (addop term)* 
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
        // nothing to visit
        return null;
    }

    // factor : STRINGCONST
    public override object VisitStrconstFactorAST(MiniCSharpParser.StrconstFactorASTContext context)
    {
        // nothing to visit
        return null;
    }

    // factor : INT 
    public override object VisitIntFactorAST(MiniCSharpParser.IntFactorASTContext context)
    {
        // nothing to visit
        return null;
    }

    // factor : DOUBLE
    public override object VisitDoubFactorAST(MiniCSharpParser.DoubFactorASTContext context)
    {
        // nothing to visit
        return null;
    }

    // factor : BOOL
    public override object VisitBoolFactorAST(MiniCSharpParser.BoolFactorASTContext context)
    {
        // nothing to visit
        return null;
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
        Visit(context.expr());
        return null;
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
}