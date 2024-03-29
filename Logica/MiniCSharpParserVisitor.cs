//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.11.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/Mariana Artavia Vene/Documents/GitHub/miniCharFinal/Logica\MiniCSharpParser.g4 by ANTLR 4.11.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="MiniCSharpParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.11.1")]
[System.CLSCompliant(false)]
public interface IMiniCSharpParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by the <c>programAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgramAST([NotNull] MiniCSharpParser.ProgramASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>usignAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.using"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUsignAST([NotNull] MiniCSharpParser.UsignASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>varDeclaAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.varDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarDeclaAST([NotNull] MiniCSharpParser.VarDeclaASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>classDeclaAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.classDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClassDeclaAST([NotNull] MiniCSharpParser.ClassDeclaASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>methDeclaAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.methodDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethDeclaAST([NotNull] MiniCSharpParser.MethDeclaASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>formParsAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.formPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFormParsAST([NotNull] MiniCSharpParser.FormParsASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>typeAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeAST([NotNull] MiniCSharpParser.TypeASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>assignStatementAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignStatementAST([NotNull] MiniCSharpParser.AssignStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ifStatementAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfStatementAST([NotNull] MiniCSharpParser.IfStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>forStatementAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForStatementAST([NotNull] MiniCSharpParser.ForStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>whileConditionStatementAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileConditionStatementAST([NotNull] MiniCSharpParser.WhileConditionStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>breakStatementAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBreakStatementAST([NotNull] MiniCSharpParser.BreakStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>returnStatementAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturnStatementAST([NotNull] MiniCSharpParser.ReturnStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>whileNumberStatementAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileNumberStatementAST([NotNull] MiniCSharpParser.WhileNumberStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>writeNumberStatementAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWriteNumberStatementAST([NotNull] MiniCSharpParser.WriteNumberStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>blockStatementAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlockStatementAST([NotNull] MiniCSharpParser.BlockStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>semicolonStatementAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSemicolonStatementAST([NotNull] MiniCSharpParser.SemicolonStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>blockAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlockAST([NotNull] MiniCSharpParser.BlockASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>actParsAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.actPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitActParsAST([NotNull] MiniCSharpParser.ActParsASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>conditionAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConditionAST([NotNull] MiniCSharpParser.ConditionASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>condTermAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.condTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCondTermAST([NotNull] MiniCSharpParser.CondTermASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>condFactAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.condFact"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCondFactAST([NotNull] MiniCSharpParser.CondFactASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>castAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.cast"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCastAST([NotNull] MiniCSharpParser.CastASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>exprAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprAST([NotNull] MiniCSharpParser.ExprASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>termAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTermAST([NotNull] MiniCSharpParser.TermASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>designFactorAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDesignFactorAST([NotNull] MiniCSharpParser.DesignFactorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>charconstFactorAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCharconstFactorAST([NotNull] MiniCSharpParser.CharconstFactorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>strconstFactorAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStrconstFactorAST([NotNull] MiniCSharpParser.StrconstFactorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>intFactorAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIntFactorAST([NotNull] MiniCSharpParser.IntFactorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>doubFactorAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDoubFactorAST([NotNull] MiniCSharpParser.DoubFactorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>boolFactorAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBoolFactorAST([NotNull] MiniCSharpParser.BoolFactorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>newIdentFactorAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNewIdentFactorAST([NotNull] MiniCSharpParser.NewIdentFactorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>exprInparentFactorAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprInparentFactorAST([NotNull] MiniCSharpParser.ExprInparentFactorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>intIdIdentAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.ident"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIntIdIdentAST([NotNull] MiniCSharpParser.IntIdIdentASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>charIdIdentAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.ident"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCharIdIdentAST([NotNull] MiniCSharpParser.CharIdIdentASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>doubIdIdentAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.ident"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDoubIdIdentAST([NotNull] MiniCSharpParser.DoubIdIdentASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>boolIdIdentAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.ident"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBoolIdIdentAST([NotNull] MiniCSharpParser.BoolIdIdentASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>strIdIdentAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.ident"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStrIdIdentAST([NotNull] MiniCSharpParser.StrIdIdentASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>identifierIdentAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.ident"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdentifierIdentAST([NotNull] MiniCSharpParser.IdentifierIdentASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>listIdentAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.ident"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListIdentAST([NotNull] MiniCSharpParser.ListIdentASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>designatorAST</c>
	/// labeled alternative in <see cref="MiniCSharpParser.designator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDesignatorAST([NotNull] MiniCSharpParser.DesignatorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MiniCSharpParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRelop([NotNull] MiniCSharpParser.RelopContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MiniCSharpParser.addop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAddop([NotNull] MiniCSharpParser.AddopContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MiniCSharpParser.mulop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMulop([NotNull] MiniCSharpParser.MulopContext context);
}
