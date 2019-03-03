# ArithmeticParser

Very Simple Proof of Concept Parser for Simple Arithmetic Expressions.

[![Build Status](https://travis-ci.org/FreeApophis/ArithmeticParser.svg?branch=master)](https://travis-ci.org/FreeApophis/ArithmeticParser) [![NuGet package](https://buildstats.info/nuget/SimpleArithmeticParser)](https://www.nuget.org/packages/SimpleArithmeticParser)

This is an example of Recursive Descent Parser which builds an Abstract Syntax Tree.

It also illustrates the usage of the Visitor Pattern to traverse the Syntax Tree.

Supported Operations:

* Binary Operations
** Addition
** Subtraction
** Multiplication
** Division
* Function Calls
* Variables
** Constants

Sample Vistors include:

* CalculateVisitor: calculates the result of the arithmetic expression as a double.
* Parenthesizer: Several Visitors to create infix notation from a syntax tree.
* RPN: Output in Reverse Polish Notation

Some Simple Tests are included, the Parser is intendet only as an academic example.
