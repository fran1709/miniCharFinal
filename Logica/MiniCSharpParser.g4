parser grammar MiniCSharpParser;

options {
  tokenVocab = MiniCSharpScanner;
}

program : (using)* CLASS ident LEFTBRACK (varDecl | classDecl | methodDecl)* RIGHTBRACK EOF;

using : USING ident SEMICOLON;

varDecl : type ident (COMMA ident)* SEMICOLON;

classDecl : CLASS ident LEFTBRACK (varDecl)* RIGHTBRACK;

methodDecl : (type | VOID) ident LEFTPAREN formPars? RIGHTPAREN block;

formPars : type ident (COMMA type ident)*;

type : ident (LEFTSBRACK RIGHTSBRACK)? ;

statement : designator (ASSIGN expr | LEFTPAREN actPars? RIGHTPAREN | PLUSPLUS | MINUSMINUS) SEMICOLON
          | IF LEFTPAREN condition RIGHTPAREN statement (ELSE statement)?
          | FOR LEFTPAREN expr SEMICOLON condition? SEMICOLON statement? RIGHTPAREN statement
          | WHILE LEFTPAREN condition RIGHTPAREN statement
          | BREAK SEMICOLON
          | RETURN expr? SEMICOLON
          | READ LEFTPAREN designator RIGHTPAREN SEMICOLON
          | WRITE LEFTPAREN expr (COMMA (INT | DOUBLE))? RIGHTPAREN SEMICOLON
          | designator ADDMETHOD LEFTPAREN (designator | INT) (COMMA (designator | INT))* RIGHTPAREN SEMICOLON
          | designator LENMETHOD LEFTPAREN RIGHTPAREN SEMICOLON
          | designator DELMETHOD LEFTPAREN INT RIGHTPAREN SEMICOLON
          | block
          | SEMICOLON;

block : LEFTBRACK (varDecl | statement)* RIGHTBRACK;
 
actPars : expr (COMMA expr)*; 

condition : condTerm (OR condTerm)*;

condTerm : condFact (AND condFact)*;

condFact : expr relop expr;

cast : LEFTPAREN type RIGHTPAREN;

expr : MINUS? cast? term (addop term)*;

term : factor (mulop factor)*;

factor : designator (LEFTPAREN (actPars)? RIGHTPAREN)?
       | CHARCONST
       | STRINGCONST
       | INT
       | DOUBLE
       | BOOL
       | NEW ident
       | LEFTPAREN expr RIGHTPAREN;

designator : ident (DOT ident | LEFTSBRACK expr RIGHTSBRACK)*;

relop : EQUALS 
        | NOTEQUALS 
        | GREATERTHAN 
        | GREATOREQUALS 
        | LESSTHAN 
        | LESSOREQUALS;

addop : PLUS | MINUS;

mulop : MULT | DIV | MOD;

ident : INT_ID 
        | CHAR_ID
        | DOUBLE_ID
        | BOOL_ID
        | STRING_ID
        | IDENTIFIER
        | LIST
        ;