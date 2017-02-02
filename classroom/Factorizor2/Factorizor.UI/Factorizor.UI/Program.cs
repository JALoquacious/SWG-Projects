using Factorizor.BLL;

namespace Factorizor.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkFlow instance = new WorkFlow();
            instance.Factorize();
        }
    }
}