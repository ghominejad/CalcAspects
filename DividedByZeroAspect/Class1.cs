using Castle.DynamicProxy;

namespace DividedByZeroAspect
{
    public class DividedByZeroInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            throw new NotImplementedException();
        }
    }

}