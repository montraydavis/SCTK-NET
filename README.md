# Source Code Toolkit

This project aims at building tokenizers (with Named Entities) ready for use in Natural Language Processing of Source Code from a variety of languages with Tensorflow 2.

SCTK generates TRUE tokens from a given programming language -- not just common keywords.

```
export class SimpleClass {
    protected _type: string = "__SIMPLE__";
    public name: string;

    constructor(name) {
        this.name = name;
    }
}

new SimpleClass("My Simple Class");
```

Generates the following tokens and entities:

```
export/KEYWORD_EXPORT
class/KEYWORD_CLASS
SimpleClass/NAN_IDENTIFIER
{/OPEN_BRACE
protected/KEYWORD_MODIFIER
_type/NAN
:/COLON
string/KEYWORD_DTYPE
=/NAN
"__SIMPLE__"/NAN
;/TERMINATOR
public/KEYWORD_MODIFIER
name/NAN
:/COLON
string/KEYWORD_DTYPE
;/TERMINATOR
constructor/NAN
(/OPEN_PARENS
name/NAN
)/CLOSE_PARENS
{/OPEN_BRACE
this/KEYWORD_THIS
./DOT
name/NAN
=/NAN
name/NAN
;/TERMINATOR
}/CLOSE_BRACE
}/CLOSE_BRACE
new/NAN
SimpleClass/NAN
(/OPEN_PARENS
"My Simple Class"/NAN
)/CLOSE_PARENS
;/TERMINATOR
```

# Loading a Corpus

Loading Source Code documents can be done as follows:


```
// A dictionary containing some common TypeScript keywords
TypeScriptTokenizer tsTokenizer = new TypeScriptTokenizer("data/universal/ts_tokens.out");

// Load the Corpus
tsTokenizer.LoadCorpus("data/typescript/simple_class.ts");
```

Document is tokenized, and can be accessed using the ```.Tokens``` property.

```
var tokens = tsTokenizer.Tokens;
```

# Languages Supported [PARTIAL SUPPORT]

- TypeScript [90%]

## Planned Language Support

- C#
- Python
- More to come!

This may change in the future!

# The Big Picture

While this project is very infant in development, I plan to continuously update this project until it is mature enough for a production environment (at the earliest).

The goal is to create embeddings based on source code which can later be used to generate Deep Learning models for source code prediction and generation.

This approach (should) greatly reduce the complexity of Natural Language Understanding since large source code documents can be converted to a relatively small number of numerical representations.

# Disclaimer !

This is a very new and on-going project. Initially, the focus is functionality. Functionality features will be preferred over code quality (initially).
