//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.11.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/Mariana Artavia Vene/Documents/I SEMESTRE 2023/Compiladores e Interpretes/ConsoleCompi/ConsoleCompi/src\MiniCSharpScanner.g4 by ANTLR 4.11.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.11.1")]
[System.CLSCompliant(false)]
public partial class MiniCSharpScanner : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		LIST=1, INT_ID=2, STRING_ID=3, CHAR_ID=4, BOOL_ID=5, DOUBLE_ID=6, CLASS=7, 
		USING=8, VOID=9, IF=10, ELSE=11, WHILE=12, FOR=13, BREAK=14, RETURN=15, 
		READ=16, WRITE=17, NEW=18, DELMETHOD=19, ADDMETHOD=20, LENMETHOD=21, DEL=22, 
		ADD=23, LEN=24, BOOL=25, INT=26, DOUBLE=27, STRINGCONST=28, CHARCONST=29, 
		EscapeSequence=30, ASSIGN=31, AND=32, OR=33, PLUS=34, MINUS=35, PLUSPLUS=36, 
		MINUSMINUS=37, MULT=38, DIV=39, MOD=40, EQUALS=41, NOTEQUALS=42, LESSTHAN=43, 
		GREATERTHAN=44, LESSOREQUALS=45, GREATOREQUALS=46, DOT=47, SEMICOLON=48, 
		COMMA=49, LEFTPAREN=50, RIGHTPAREN=51, LEFTBRACK=52, RIGHTBRACK=53, LEFTSBRACK=54, 
		RIGHTSBRACK=55, IDENTIFIER=56, COMMENT=57, BLOCKCOMMENT=58, WS=59;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"LIST", "INT_ID", "STRING_ID", "CHAR_ID", "BOOL_ID", "DOUBLE_ID", "CLASS", 
		"USING", "VOID", "IF", "ELSE", "WHILE", "FOR", "BREAK", "RETURN", "READ", 
		"WRITE", "NEW", "DELMETHOD", "ADDMETHOD", "LENMETHOD", "DEL", "ADD", "LEN", 
		"BOOL", "INT", "DOUBLE", "STRINGCONST", "CHARCONST", "EscapeSequence", 
		"ASSIGN", "AND", "OR", "PLUS", "MINUS", "PLUSPLUS", "MINUSMINUS", "MULT", 
		"DIV", "MOD", "EQUALS", "NOTEQUALS", "LESSTHAN", "GREATERTHAN", "LESSOREQUALS", 
		"GREATOREQUALS", "DOT", "SEMICOLON", "COMMA", "LEFTPAREN", "RIGHTPAREN", 
		"LEFTBRACK", "RIGHTBRACK", "LEFTSBRACK", "RIGHTSBRACK", "LETTER", "DIGIT", 
		"IDENTIFIER", "COMMENT", "BLOCKCOMMENT", "WS"
	};


	public MiniCSharpScanner(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public MiniCSharpScanner(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, null, "'int'", "'string'", "'char'", "'bool'", "'double'", "'class'", 
		"'using'", "'void'", "'if'", "'else'", "'while'", "'for'", "'break'", 
		"'return'", "'read'", "'write'", "'new'", "'.del'", "'.add'", "'.len'", 
		"'del'", "'add'", "'len'", null, null, null, null, null, null, "'='", 
		"'&&'", "'||'", "'+'", "'-'", "'++'", "'--'", "'*'", "'/'", "'%'", "'=='", 
		"'!='", "'<'", "'>'", "'<='", "'>='", "'.'", "';'", "','", "'('", "')'", 
		"'{'", "'}'", "'['", "']'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "LIST", "INT_ID", "STRING_ID", "CHAR_ID", "BOOL_ID", "DOUBLE_ID", 
		"CLASS", "USING", "VOID", "IF", "ELSE", "WHILE", "FOR", "BREAK", "RETURN", 
		"READ", "WRITE", "NEW", "DELMETHOD", "ADDMETHOD", "LENMETHOD", "DEL", 
		"ADD", "LEN", "BOOL", "INT", "DOUBLE", "STRINGCONST", "CHARCONST", "EscapeSequence", 
		"ASSIGN", "AND", "OR", "PLUS", "MINUS", "PLUSPLUS", "MINUSMINUS", "MULT", 
		"DIV", "MOD", "EQUALS", "NOTEQUALS", "LESSTHAN", "GREATERTHAN", "LESSOREQUALS", 
		"GREATOREQUALS", "DOT", "SEMICOLON", "COMMA", "LEFTPAREN", "RIGHTPAREN", 
		"LEFTBRACK", "RIGHTBRACK", "LEFTSBRACK", "RIGHTSBRACK", "IDENTIFIER", 
		"COMMENT", "BLOCKCOMMENT", "WS"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "MiniCSharpScanner.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static MiniCSharpScanner() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,59,420,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,2,21,
		7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,2,27,7,27,2,28,
		7,28,2,29,7,29,2,30,7,30,2,31,7,31,2,32,7,32,2,33,7,33,2,34,7,34,2,35,
		7,35,2,36,7,36,2,37,7,37,2,38,7,38,2,39,7,39,2,40,7,40,2,41,7,41,2,42,
		7,42,2,43,7,43,2,44,7,44,2,45,7,45,2,46,7,46,2,47,7,47,2,48,7,48,2,49,
		7,49,2,50,7,50,2,51,7,51,2,52,7,52,2,53,7,53,2,54,7,54,2,55,7,55,2,56,
		7,56,2,57,7,57,2,58,7,58,2,59,7,59,2,60,7,60,1,0,1,0,1,0,1,0,1,0,1,0,3,
		0,130,8,0,1,0,1,0,1,0,3,0,135,8,0,1,0,1,0,1,1,1,1,1,1,1,1,1,2,1,2,1,2,
		1,2,1,2,1,2,1,2,1,3,1,3,1,3,1,3,1,3,1,4,1,4,1,4,1,4,1,4,1,5,1,5,1,5,1,
		5,1,5,1,5,1,5,1,6,1,6,1,6,1,6,1,6,1,6,1,7,1,7,1,7,1,7,1,7,1,7,1,8,1,8,
		1,8,1,8,1,8,1,9,1,9,1,9,1,10,1,10,1,10,1,10,1,10,1,11,1,11,1,11,1,11,1,
		11,1,11,1,12,1,12,1,12,1,12,1,13,1,13,1,13,1,13,1,13,1,13,1,14,1,14,1,
		14,1,14,1,14,1,14,1,14,1,15,1,15,1,15,1,15,1,15,1,16,1,16,1,16,1,16,1,
		16,1,16,1,17,1,17,1,17,1,17,1,18,1,18,1,18,1,18,1,18,1,19,1,19,1,19,1,
		19,1,19,1,20,1,20,1,20,1,20,1,20,1,21,1,21,1,21,1,21,1,22,1,22,1,22,1,
		22,1,23,1,23,1,23,1,23,1,24,1,24,1,24,1,24,1,24,1,24,1,24,1,24,1,24,3,
		24,266,8,24,1,25,4,25,269,8,25,11,25,12,25,270,1,26,4,26,274,8,26,11,26,
		12,26,275,1,26,1,26,5,26,280,8,26,10,26,12,26,283,9,26,3,26,285,8,26,1,
		26,1,26,3,26,289,8,26,1,26,4,26,292,8,26,11,26,12,26,293,3,26,296,8,26,
		1,27,1,27,1,27,1,27,5,27,302,8,27,10,27,12,27,305,9,27,1,27,1,27,1,28,
		1,28,1,28,3,28,312,8,28,1,28,1,28,1,29,1,29,1,29,1,30,1,30,1,31,1,31,1,
		31,1,32,1,32,1,32,1,33,1,33,1,34,1,34,1,35,1,35,1,35,1,36,1,36,1,36,1,
		37,1,37,1,38,1,38,1,39,1,39,1,40,1,40,1,40,1,41,1,41,1,41,1,42,1,42,1,
		43,1,43,1,44,1,44,1,44,1,45,1,45,1,45,1,46,1,46,1,47,1,47,1,48,1,48,1,
		49,1,49,1,50,1,50,1,51,1,51,1,52,1,52,1,53,1,53,1,54,1,54,1,55,1,55,1,
		56,1,56,1,57,1,57,1,57,5,57,384,8,57,10,57,12,57,387,9,57,1,58,1,58,1,
		58,1,58,5,58,393,8,58,10,58,12,58,396,9,58,1,58,1,58,1,59,1,59,1,59,1,
		59,5,59,404,8,59,10,59,12,59,407,9,59,1,59,1,59,1,59,1,59,1,59,1,60,4,
		60,415,8,60,11,60,12,60,416,1,60,1,60,0,0,61,1,1,3,2,5,3,7,4,9,5,11,6,
		13,7,15,8,17,9,19,10,21,11,23,12,25,13,27,14,29,15,31,16,33,17,35,18,37,
		19,39,20,41,21,43,22,45,23,47,24,49,25,51,26,53,27,55,28,57,29,59,30,61,
		31,63,32,65,33,67,34,69,35,71,36,73,37,75,38,77,39,79,40,81,41,83,42,85,
		43,87,44,89,45,91,46,93,47,95,48,97,49,99,50,101,51,103,52,105,53,107,
		54,109,55,111,0,113,0,115,56,117,57,119,58,121,59,1,0,9,1,0,48,57,2,0,
		69,69,101,101,2,0,43,43,45,45,4,0,10,10,13,13,34,34,92,92,2,0,39,39,92,
		92,8,0,34,34,39,39,92,92,98,98,102,102,110,110,114,114,116,116,3,0,65,
		90,95,95,97,122,2,0,10,10,13,13,3,0,9,10,13,13,32,32,440,0,1,1,0,0,0,0,
		3,1,0,0,0,0,5,1,0,0,0,0,7,1,0,0,0,0,9,1,0,0,0,0,11,1,0,0,0,0,13,1,0,0,
		0,0,15,1,0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,21,1,0,0,0,0,23,1,0,0,0,0,25,
		1,0,0,0,0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,0,0,0,0,33,1,0,0,0,0,35,1,0,0,
		0,0,37,1,0,0,0,0,39,1,0,0,0,0,41,1,0,0,0,0,43,1,0,0,0,0,45,1,0,0,0,0,47,
		1,0,0,0,0,49,1,0,0,0,0,51,1,0,0,0,0,53,1,0,0,0,0,55,1,0,0,0,0,57,1,0,0,
		0,0,59,1,0,0,0,0,61,1,0,0,0,0,63,1,0,0,0,0,65,1,0,0,0,0,67,1,0,0,0,0,69,
		1,0,0,0,0,71,1,0,0,0,0,73,1,0,0,0,0,75,1,0,0,0,0,77,1,0,0,0,0,79,1,0,0,
		0,0,81,1,0,0,0,0,83,1,0,0,0,0,85,1,0,0,0,0,87,1,0,0,0,0,89,1,0,0,0,0,91,
		1,0,0,0,0,93,1,0,0,0,0,95,1,0,0,0,0,97,1,0,0,0,0,99,1,0,0,0,0,101,1,0,
		0,0,0,103,1,0,0,0,0,105,1,0,0,0,0,107,1,0,0,0,0,109,1,0,0,0,0,115,1,0,
		0,0,0,117,1,0,0,0,0,119,1,0,0,0,0,121,1,0,0,0,1,129,1,0,0,0,3,138,1,0,
		0,0,5,142,1,0,0,0,7,149,1,0,0,0,9,154,1,0,0,0,11,159,1,0,0,0,13,166,1,
		0,0,0,15,172,1,0,0,0,17,178,1,0,0,0,19,183,1,0,0,0,21,186,1,0,0,0,23,191,
		1,0,0,0,25,197,1,0,0,0,27,201,1,0,0,0,29,207,1,0,0,0,31,214,1,0,0,0,33,
		219,1,0,0,0,35,225,1,0,0,0,37,229,1,0,0,0,39,234,1,0,0,0,41,239,1,0,0,
		0,43,244,1,0,0,0,45,248,1,0,0,0,47,252,1,0,0,0,49,265,1,0,0,0,51,268,1,
		0,0,0,53,273,1,0,0,0,55,297,1,0,0,0,57,308,1,0,0,0,59,315,1,0,0,0,61,318,
		1,0,0,0,63,320,1,0,0,0,65,323,1,0,0,0,67,326,1,0,0,0,69,328,1,0,0,0,71,
		330,1,0,0,0,73,333,1,0,0,0,75,336,1,0,0,0,77,338,1,0,0,0,79,340,1,0,0,
		0,81,342,1,0,0,0,83,345,1,0,0,0,85,348,1,0,0,0,87,350,1,0,0,0,89,352,1,
		0,0,0,91,355,1,0,0,0,93,358,1,0,0,0,95,360,1,0,0,0,97,362,1,0,0,0,99,364,
		1,0,0,0,101,366,1,0,0,0,103,368,1,0,0,0,105,370,1,0,0,0,107,372,1,0,0,
		0,109,374,1,0,0,0,111,376,1,0,0,0,113,378,1,0,0,0,115,380,1,0,0,0,117,
		388,1,0,0,0,119,399,1,0,0,0,121,414,1,0,0,0,123,130,3,3,1,0,124,130,3,
		5,2,0,125,130,3,7,3,0,126,130,3,9,4,0,127,130,3,11,5,0,128,130,3,115,57,
		0,129,123,1,0,0,0,129,124,1,0,0,0,129,125,1,0,0,0,129,126,1,0,0,0,129,
		127,1,0,0,0,129,128,1,0,0,0,130,131,1,0,0,0,131,134,3,107,53,0,132,135,
		3,115,57,0,133,135,3,51,25,0,134,132,1,0,0,0,134,133,1,0,0,0,134,135,1,
		0,0,0,135,136,1,0,0,0,136,137,3,109,54,0,137,2,1,0,0,0,138,139,5,105,0,
		0,139,140,5,110,0,0,140,141,5,116,0,0,141,4,1,0,0,0,142,143,5,115,0,0,
		143,144,5,116,0,0,144,145,5,114,0,0,145,146,5,105,0,0,146,147,5,110,0,
		0,147,148,5,103,0,0,148,6,1,0,0,0,149,150,5,99,0,0,150,151,5,104,0,0,151,
		152,5,97,0,0,152,153,5,114,0,0,153,8,1,0,0,0,154,155,5,98,0,0,155,156,
		5,111,0,0,156,157,5,111,0,0,157,158,5,108,0,0,158,10,1,0,0,0,159,160,5,
		100,0,0,160,161,5,111,0,0,161,162,5,117,0,0,162,163,5,98,0,0,163,164,5,
		108,0,0,164,165,5,101,0,0,165,12,1,0,0,0,166,167,5,99,0,0,167,168,5,108,
		0,0,168,169,5,97,0,0,169,170,5,115,0,0,170,171,5,115,0,0,171,14,1,0,0,
		0,172,173,5,117,0,0,173,174,5,115,0,0,174,175,5,105,0,0,175,176,5,110,
		0,0,176,177,5,103,0,0,177,16,1,0,0,0,178,179,5,118,0,0,179,180,5,111,0,
		0,180,181,5,105,0,0,181,182,5,100,0,0,182,18,1,0,0,0,183,184,5,105,0,0,
		184,185,5,102,0,0,185,20,1,0,0,0,186,187,5,101,0,0,187,188,5,108,0,0,188,
		189,5,115,0,0,189,190,5,101,0,0,190,22,1,0,0,0,191,192,5,119,0,0,192,193,
		5,104,0,0,193,194,5,105,0,0,194,195,5,108,0,0,195,196,5,101,0,0,196,24,
		1,0,0,0,197,198,5,102,0,0,198,199,5,111,0,0,199,200,5,114,0,0,200,26,1,
		0,0,0,201,202,5,98,0,0,202,203,5,114,0,0,203,204,5,101,0,0,204,205,5,97,
		0,0,205,206,5,107,0,0,206,28,1,0,0,0,207,208,5,114,0,0,208,209,5,101,0,
		0,209,210,5,116,0,0,210,211,5,117,0,0,211,212,5,114,0,0,212,213,5,110,
		0,0,213,30,1,0,0,0,214,215,5,114,0,0,215,216,5,101,0,0,216,217,5,97,0,
		0,217,218,5,100,0,0,218,32,1,0,0,0,219,220,5,119,0,0,220,221,5,114,0,0,
		221,222,5,105,0,0,222,223,5,116,0,0,223,224,5,101,0,0,224,34,1,0,0,0,225,
		226,5,110,0,0,226,227,5,101,0,0,227,228,5,119,0,0,228,36,1,0,0,0,229,230,
		5,46,0,0,230,231,5,100,0,0,231,232,5,101,0,0,232,233,5,108,0,0,233,38,
		1,0,0,0,234,235,5,46,0,0,235,236,5,97,0,0,236,237,5,100,0,0,237,238,5,
		100,0,0,238,40,1,0,0,0,239,240,5,46,0,0,240,241,5,108,0,0,241,242,5,101,
		0,0,242,243,5,110,0,0,243,42,1,0,0,0,244,245,5,100,0,0,245,246,5,101,0,
		0,246,247,5,108,0,0,247,44,1,0,0,0,248,249,5,97,0,0,249,250,5,100,0,0,
		250,251,5,100,0,0,251,46,1,0,0,0,252,253,5,108,0,0,253,254,5,101,0,0,254,
		255,5,110,0,0,255,48,1,0,0,0,256,257,5,116,0,0,257,258,5,114,0,0,258,259,
		5,117,0,0,259,266,5,101,0,0,260,261,5,102,0,0,261,262,5,97,0,0,262,263,
		5,108,0,0,263,264,5,115,0,0,264,266,5,101,0,0,265,256,1,0,0,0,265,260,
		1,0,0,0,266,50,1,0,0,0,267,269,3,113,56,0,268,267,1,0,0,0,269,270,1,0,
		0,0,270,268,1,0,0,0,270,271,1,0,0,0,271,52,1,0,0,0,272,274,7,0,0,0,273,
		272,1,0,0,0,274,275,1,0,0,0,275,273,1,0,0,0,275,276,1,0,0,0,276,284,1,
		0,0,0,277,281,5,46,0,0,278,280,7,0,0,0,279,278,1,0,0,0,280,283,1,0,0,0,
		281,279,1,0,0,0,281,282,1,0,0,0,282,285,1,0,0,0,283,281,1,0,0,0,284,277,
		1,0,0,0,284,285,1,0,0,0,285,295,1,0,0,0,286,288,7,1,0,0,287,289,7,2,0,
		0,288,287,1,0,0,0,288,289,1,0,0,0,289,291,1,0,0,0,290,292,7,0,0,0,291,
		290,1,0,0,0,292,293,1,0,0,0,293,291,1,0,0,0,293,294,1,0,0,0,294,296,1,
		0,0,0,295,286,1,0,0,0,295,296,1,0,0,0,296,54,1,0,0,0,297,303,5,34,0,0,
		298,302,8,3,0,0,299,300,5,92,0,0,300,302,9,0,0,0,301,298,1,0,0,0,301,299,
		1,0,0,0,302,305,1,0,0,0,303,301,1,0,0,0,303,304,1,0,0,0,304,306,1,0,0,
		0,305,303,1,0,0,0,306,307,5,34,0,0,307,56,1,0,0,0,308,311,5,39,0,0,309,
		312,3,59,29,0,310,312,8,4,0,0,311,309,1,0,0,0,311,310,1,0,0,0,312,313,
		1,0,0,0,313,314,5,39,0,0,314,58,1,0,0,0,315,316,5,92,0,0,316,317,7,5,0,
		0,317,60,1,0,0,0,318,319,5,61,0,0,319,62,1,0,0,0,320,321,5,38,0,0,321,
		322,5,38,0,0,322,64,1,0,0,0,323,324,5,124,0,0,324,325,5,124,0,0,325,66,
		1,0,0,0,326,327,5,43,0,0,327,68,1,0,0,0,328,329,5,45,0,0,329,70,1,0,0,
		0,330,331,5,43,0,0,331,332,5,43,0,0,332,72,1,0,0,0,333,334,5,45,0,0,334,
		335,5,45,0,0,335,74,1,0,0,0,336,337,5,42,0,0,337,76,1,0,0,0,338,339,5,
		47,0,0,339,78,1,0,0,0,340,341,5,37,0,0,341,80,1,0,0,0,342,343,5,61,0,0,
		343,344,5,61,0,0,344,82,1,0,0,0,345,346,5,33,0,0,346,347,5,61,0,0,347,
		84,1,0,0,0,348,349,5,60,0,0,349,86,1,0,0,0,350,351,5,62,0,0,351,88,1,0,
		0,0,352,353,5,60,0,0,353,354,5,61,0,0,354,90,1,0,0,0,355,356,5,62,0,0,
		356,357,5,61,0,0,357,92,1,0,0,0,358,359,5,46,0,0,359,94,1,0,0,0,360,361,
		5,59,0,0,361,96,1,0,0,0,362,363,5,44,0,0,363,98,1,0,0,0,364,365,5,40,0,
		0,365,100,1,0,0,0,366,367,5,41,0,0,367,102,1,0,0,0,368,369,5,123,0,0,369,
		104,1,0,0,0,370,371,5,125,0,0,371,106,1,0,0,0,372,373,5,91,0,0,373,108,
		1,0,0,0,374,375,5,93,0,0,375,110,1,0,0,0,376,377,7,6,0,0,377,112,1,0,0,
		0,378,379,7,0,0,0,379,114,1,0,0,0,380,385,3,111,55,0,381,384,3,111,55,
		0,382,384,3,113,56,0,383,381,1,0,0,0,383,382,1,0,0,0,384,387,1,0,0,0,385,
		383,1,0,0,0,385,386,1,0,0,0,386,116,1,0,0,0,387,385,1,0,0,0,388,389,5,
		47,0,0,389,390,5,47,0,0,390,394,1,0,0,0,391,393,8,7,0,0,392,391,1,0,0,
		0,393,396,1,0,0,0,394,392,1,0,0,0,394,395,1,0,0,0,395,397,1,0,0,0,396,
		394,1,0,0,0,397,398,6,58,0,0,398,118,1,0,0,0,399,400,5,47,0,0,400,401,
		5,42,0,0,401,405,1,0,0,0,402,404,8,7,0,0,403,402,1,0,0,0,404,407,1,0,0,
		0,405,403,1,0,0,0,405,406,1,0,0,0,406,408,1,0,0,0,407,405,1,0,0,0,408,
		409,5,42,0,0,409,410,5,47,0,0,410,411,1,0,0,0,411,412,6,59,0,0,412,120,
		1,0,0,0,413,415,7,8,0,0,414,413,1,0,0,0,415,416,1,0,0,0,416,414,1,0,0,
		0,416,417,1,0,0,0,417,418,1,0,0,0,418,419,6,60,0,0,419,122,1,0,0,0,19,
		0,129,134,265,270,275,281,284,288,293,295,301,303,311,383,385,394,405,
		416,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
