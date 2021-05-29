lexer grammar pascal;

// delim:
WS: [ \t\r\n] -> channel(HIDDEN);

//primitives
fragment PLUS : '+';
fragment MINUS : '-';
fragment STAR : '*';
fragment AMP : '&';
fragment DOLLOR : '$';
fragment PERCENT : '%';
fragment UNDERSCORE : '_';

fragment LETTER : [A-Za-z];
fragment DIGIT : '0' .. '9';
fragment HEXDIGIT : [0-9a-zA-Z];
fragment OCTDIGIT : '0' .. '7';
fragment BINDIGIT : '0' | '1';

//numbers
fragment HEXDIGIT_SEQ : (HEXDIGIT)+;
fragment OCTDIGIT_SEQ : (OCTDIGIT)+;
fragment BINDIGIT_SEQ : (BINDIGIT)+;
fragment DIGIT_SEQ : (DIGIT)+;

fragment EXP : 'E'|'e';
fragment SIGN : (PLUS | MINUS);
fragment SCALE_FACTOR : (EXP SIGN? DIGIT_SEQ);

fragment UINT : (DIGIT_SEQ | (DOLLOR HEXDIGIT_SEQ) | (AMP OCTDIGIT_SEQ) | (PERCENT BINDIGIT_SEQ));
fragment UREAL : (DIGIT_SEQ ('.' DIGIT_SEQ) ? SCALE_FACTOR?);
UNUM : (UREAL | UINT);
SNUM : SIGN? UNUM;

//character strings

fragment QUOTE : '\'';

fragment QUOTED_STRING : QUOTE (STRING_CHARACTER) QUOTE ;
fragment STRING_CHARACTER : (~( [\r] | '\''))*; 
fragment CONTROL_STRING : '#' UINT;

CHARCACTER_STRING : (QUOTED_STRING | CONTROL_STRING)+;

//comments

fragment SBRA : '[';
fragment SKET : ']';
fragment CBRA : '{';
fragment CKET : '{';
fragment BRA : '(';
fragment KET : ')';

SINGLE_LINE_COMMENT: '//' (~[\r\n])* -> channel(HIDDEN);
MLINE_COMMENT : BRA STAR (DEL_COMMENT | .) *? STAR KET -> channel(HIDDEN);
MLINE_COMMENT2: CBRA (DEL_COMMENT | . )*? CKET -> channel(HIDDEN); 
fragment DEL_COMMENT : MLINE_COMMENT | MLINE_COMMENT2 ;

//identifiers
ID : LETTER (LETTER | DIGIT | UNDERSCORE)*;

//simbols
SYMBOL : LETTER | DIGIT | HEXDIGIT;

//fail character

FAIL: .;
