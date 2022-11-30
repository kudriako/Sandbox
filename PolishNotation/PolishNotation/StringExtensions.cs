namespace PolishNotation
{
    public static class StringExtensions
    {
        public static double Evaluate(this string text)
        {
            if (text == null || string.IsNullOrWhiteSpace(text))
                return default(double);
            var terms = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var operations = BuildExpressionStack(new Stack<string>(terms));
            var result = operations.EvaluateExpressionStack();
            return result;
        }

        private static Queue<Action<Stack<double>>> BuildExpressionStack(Stack<string> terms)
        {
            var operations = new Queue<Action<Stack<double>>>();
            foreach (var term in terms)
            {
                switch (term)
                {
                    case "+":
                        operations.Enqueue(stack => stack.Push(stack.Pop() + stack.Pop()));
                        break;
                    case "-":
                        operations.Enqueue(stack => stack.Push(stack.Pop() - stack.Pop()));
                        break;
                    case "*":
                        operations.Enqueue(stack => stack.Push(stack.Pop() * stack.Pop()));
                        break;
                    case "/":
                        operations.Enqueue(stack => stack.Push(stack.Pop() / stack.Pop()));
                        break;
                    case ")":
                        operations.Enqueue(stack => { });
                        break;
                    case "(":
                        operations.Enqueue(stack => { });
                        break;
                    default:
                        var value = double.Parse(term);
                        operations.Enqueue(stack => stack.Push(value));
                        break;
                }

            }
            return operations;
        }

        private static double EvaluateExpressionStack(this Queue<Action<Stack<double>>> operations)
        {
            var operands = new Stack<double>();
            foreach (var operation in operations)
            {
                operation(operands);
            }
            var result = operands.Pop();
            return result;
        }
    }
}