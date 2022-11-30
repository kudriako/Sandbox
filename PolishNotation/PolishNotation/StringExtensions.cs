namespace PolishNotation
{
    public static class StringExtensions
    {
        public static double Evaluate(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
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
                operations.Enqueue(term switch
                {
                    "+" => stack => stack.Push(stack.Pop() + stack.Pop()),
                    "-" => stack => stack.Push(stack.Pop() - stack.Pop()),
                    "*" => stack => stack.Push(stack.Pop() * stack.Pop()),
                    "/" => stack => stack.Push(stack.Pop() / stack.Pop()),
                    ")" => stack => { },
                    "(" => stack => { },
                    _ => stack => stack.Push(double.Parse(term))
                });
            };
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