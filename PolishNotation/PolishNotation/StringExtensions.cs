namespace PolishNotation
{
    public static class StringExtensions
    {
        public static double Evaluate(this string text)
        {
            if (text == null || string.IsNullOrWhiteSpace(text))
                return default(double);
            var terms = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var stack = new Stack<string>(terms);
            return BuildExpressionStack(stack).EvaluateExpressionStack();
        }

        private static Stack<Func<double>> BuildExpressionStack(Stack<string> terms)
        {
            var result = new Stack<Func<double>>();
            var value = default(double);
            foreach(var term in terms)
            {
                value = double.Parse(term);
                result.Push(() => value);
            }
            return result;
        }

        private static double EvaluateExpressionStack(this Stack<Func<double>> expressions)
        {
            var result = 0.0;
            foreach (var expression in expressions)
            {
                result = expression();
            }
            return result;
        }
    }
}