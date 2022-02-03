using Orleans;

namespace OrleansTestingApp.Interfaces
{
    public interface IHelloGrain : IGrainWithStringKey
    {
        Task<string> SayHello(string greeting);
    }
}
