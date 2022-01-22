using Autofac;
using Autofac.Extras.DynamicProxy;
using CachingAspect;
using Calculator;
using DividedByZeroAspect;
using HistoryAspect;
using LoggingAspect;

// Enabling Interceptors with Autofac DI
var builder = new ContainerBuilder();

builder.Register(i => new LoggerInterceptor(Console.Out));
builder.Register(i => new CacheInterceptor());
builder.Register(i => new DividedByZeroInterceptor());
builder.Register(i => new HistoryInterceptor());

builder.RegisterType<Calculator.Calculator>()
       .As<ICalculator>()
       .EnableInterfaceInterceptors()
       .InterceptedBy(typeof(LoggerInterceptor))
       .InterceptedBy(typeof(CacheInterceptor))
       .InterceptedBy(typeof(DividedByZeroInterceptor))
       .InterceptedBy(typeof(HistoryInterceptor));

var container = builder.Build();

var calculator = container.Resolve<ICalculator>();

calculator.Add(2, 5);
calculator.Divide(2, 0);
