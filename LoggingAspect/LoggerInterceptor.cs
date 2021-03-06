using Castle.DynamicProxy;

namespace LoggingAspect
{
    public class LoggerInterceptor : IInterceptor
    {
        TextWriter writer;
        public LoggerInterceptor(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }
            this.writer = writer;
        }
        
        public void Intercept(IInvocation invocation)
        {
            var name = $"{invocation.Method.DeclaringType}.{invocation.Method.Name}";
            var args = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()));
            
            writer.WriteLine($"Calling: {name}");
            writer.WriteLine($"Args: {args}");

            var watch = System.Diagnostics.Stopwatch.StartNew();
            invocation.Proceed(); //Intercepted method is executed here.
            watch.Stop();
            var executionTime = watch.ElapsedMilliseconds;
            writer.WriteLine($"Done: result was {invocation.ReturnValue}");
            writer.WriteLine($"Execution Time: {executionTime} ms.");
            writer.WriteLine();
        }
    }

}