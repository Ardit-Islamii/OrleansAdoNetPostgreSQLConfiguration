using Orleans;
using OrleansTestingApp.Interfaces;

namespace OrleansTestingApp
{
    public class HelloGrain : Grain, IHelloGrain
    {
        public Task<string> SayHello(string greeting) => Task.FromResult($"Hello, {greeting}!");
    }
}
