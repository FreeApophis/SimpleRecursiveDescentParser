# ArithmeticParser

Very Simple Proof of Concept Parser for Simple Arithmetic Expressions.

This is an example of Recursive Descent Parser which builds an Abstract Syntax Tree.

It also illustrates the usage of the Visitor Pattern to traverse the Syntax Tree.

Sample Vistors include:

* CalculateVisitor: calculates the result of the arithmetic expression as a double.
* Parenthesizer: Several Visitors to create infix notation from a syntax tree.
* RPN: Output in Reverse Polish Notation

Some Simple Tests are included, the Parser is intendet only as an academic example.
