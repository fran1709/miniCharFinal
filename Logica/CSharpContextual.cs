using System;

namespace miniChart.Logica;

public class CSharpContextual : MiniCSharpParserBaseVisitor<Object>
{
    public override object VisitProgramAST(MiniCSharpParser.ProgramASTContext context)
    {
        return null;
    }

    public override object VisitUsignAST(MiniCSharpParser.UsignASTContext context)
    {
        return null;
    }

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

    public override object VisitClassDeclaAST(MiniCSharpParser.ClassDeclaASTContext context)
    {
        Visit(context.ident());
        for (int i = 0; i < context.varDecl().Length; i++)
        {
            Visit(context.varDecl(i));
        }
        return null;
    }

    public override object VisitMethDeclaAST(MiniCSharpParser.MethDeclaASTContext context)
    {
        
        return null;
    }

    public override object VisitFormParsAST(MiniCSharpParser.FormParsASTContext context)
    {
        Visit(context.type(0));
        Visit(context.ident(0));
        for (int i = 0; i < context.type().Length; i++)
        {
            Visit(context.type(i));
        }
        for (int i = 0; i < context.ident().Length; i++)
        {
            Visit(context.ident(i));
        }
        return null;
    }

    public override object VisitTypeAST(MiniCSharpParser.TypeASTContext context)
    {
        Visit(context.ident());
        return null;
    }

    public override object VisitAssignStatementAST(MiniCSharpParser.AssignStatementASTContext context)
    {
        
        return null;
    }

    public override object VisitIfStatementAST(MiniCSharpParser.IfStatementASTContext context)
    {
        return null;
    }

    public override object VisitForStatementAST(MiniCSharpParser.ForStatementASTContext context)
    {
        return null;
    }

    public override object VisitWhileConditionStatementAST(MiniCSharpParser.WhileConditionStatementASTContext context)
    {
        return null;
    }

    public override object VisitBreakStatementAST(MiniCSharpParser.BreakStatementASTContext context)
    {
        return null;
    }

    public override object VisitReturnStatementAST(MiniCSharpParser.ReturnStatementASTContext context)
    {
        return null;
    }

    public override object VisitWhileNumberStatementAST(MiniCSharpParser.WhileNumberStatementASTContext context)
    {
        return null;
    }

    public override object VisitWriteNumberStatementAST(MiniCSharpParser.WriteNumberStatementASTContext context)
    {
        return null;    
    }

    public override object VisitDesignAddMethStatementAST(MiniCSharpParser.DesignAddMethStatementASTContext context)
    {
        return null;
    }

    public override object VisitDesignLenMethStatementAST(MiniCSharpParser.DesignLenMethStatementASTContext context)
    {
        return null;
    }

    public override object VisitDesignDelMethStatementAST(MiniCSharpParser.DesignDelMethStatementASTContext context)
    {
        return null;
    }

    public override object VisitBlockStatementAST(MiniCSharpParser.BlockStatementASTContext context)
    {
        return null;
    }

    public override object VisitSemicolonStatementAST(MiniCSharpParser.SemicolonStatementASTContext context)
    {
        return null;
    }

    public override object VisitBlockAST(MiniCSharpParser.BlockASTContext context)
    {
        return null;
    }

    public override object VisitActParsAST(MiniCSharpParser.ActParsASTContext context)
    {
        return null;
    }

    public override object VisitConditionAST(MiniCSharpParser.ConditionASTContext context)
    {
        return null;
    }

    public override object VisitCondTermAST(MiniCSharpParser.CondTermASTContext context)
    {
        return null;
    }

    public override object VisitCondFactAST(MiniCSharpParser.CondFactASTContext context)
    {
        return null;
    }

    public override object VisitCastAST(MiniCSharpParser.CastASTContext context)
    {
        return null;
    }

    public override object VisitExprAST(MiniCSharpParser.ExprASTContext context)
    {
        return null;
    }

    public override object VisitTermAST(MiniCSharpParser.TermASTContext context)
    {
        return null;
    }

    public override object VisitDesignFactorAST(MiniCSharpParser.DesignFactorASTContext context)
    {
        return null;
    }

    public override object VisitCharconstFactorAST(MiniCSharpParser.CharconstFactorASTContext context)
    {
        return null;
    }

    public override object VisitStrconstFactorAST(MiniCSharpParser.StrconstFactorASTContext context)
    {
        return null;
    }

    public override object VisitIntFactorAST(MiniCSharpParser.IntFactorASTContext context)
    {
        return null;
    }

    public override object VisitDoubFactorAST(MiniCSharpParser.DoubFactorASTContext context)
    {
        return null;
    }

    public override object VisitBoolFactorAST(MiniCSharpParser.BoolFactorASTContext context)
    {
        return null;
    }

    public override object VisitNewIdentFactorAST(MiniCSharpParser.NewIdentFactorASTContext context)
    {
        return null;
    }

    public override object VisitExprInparentFactorAST(MiniCSharpParser.ExprInparentFactorASTContext context)
    {
        return null;
    }

    public override object VisitDesignatorAST(MiniCSharpParser.DesignatorASTContext context)
    {
        return null;
    }

    public override object VisitEqualsRelopAST(MiniCSharpParser.EqualsRelopASTContext context)
    {
        return null;
    }

    public override object VisitNotEqualsRelopAST(MiniCSharpParser.NotEqualsRelopASTContext context)
    {
        return null;
    }

    public override object VisitGreatThanRelopAST(MiniCSharpParser.GreatThanRelopASTContext context)
    {
        return null;
    }

    public override object VisitGreatOrEqualRelopAST(MiniCSharpParser.GreatOrEqualRelopASTContext context)
    {
        return null;
    }

    public override object VisitLessThanRelopAST(MiniCSharpParser.LessThanRelopASTContext context)
    {
        return null;
    }

    public override object VisitLessOrEqualRelopAST(MiniCSharpParser.LessOrEqualRelopASTContext context)
    {
        return null;
    }

    public override object VisitPlusAddopAST(MiniCSharpParser.PlusAddopASTContext context)
    {
        return null;
    }

    public override object VisitMinusAddopAST(MiniCSharpParser.MinusAddopASTContext context)
    {
        return null;
    }

    public override object VisitMultMulopAST(MiniCSharpParser.MultMulopASTContext context)
    {
        return null;
    }

    public override object VisitDivMulopAST(MiniCSharpParser.DivMulopASTContext context)
    {
        return null;
    }

    public override object VisitModMulopAST(MiniCSharpParser.ModMulopASTContext context)
    {
        return null;
    }

    public override object VisitIntIdIdentAST(MiniCSharpParser.IntIdIdentASTContext context)
    {
        return null;
    }

    public override object VisitCharIdIdentAST(MiniCSharpParser.CharIdIdentASTContext context)
    {
        return null;
    }

    public override object VisitDoubIdIdentAST(MiniCSharpParser.DoubIdIdentASTContext context)
    {
        return null;
    }

    public override object VisitBoolIdIdentAST(MiniCSharpParser.BoolIdIdentASTContext context)
    {
        return null;
    }

    public override object VisitStrIdIdentAST(MiniCSharpParser.StrIdIdentASTContext context)
    {
        return null;
    }

    public override object VisitIdentifierIdentAST(MiniCSharpParser.IdentifierIdentASTContext context)
    {
        return null;
    }

    public override object VisitListIdentAST(MiniCSharpParser.ListIdentASTContext context)
    {
        return null;
    }
}