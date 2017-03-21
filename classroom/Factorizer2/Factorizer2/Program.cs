using Factorizer.BLL;

namespace Factorizer.UI
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