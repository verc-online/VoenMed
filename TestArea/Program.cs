using VoenMedLibrary.Models;

namespace TestArea
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            InjuryStatusLocalisModel model = new InjuryStatusLocalisModel();

            model.AddHeadFront();

            model.DeleteHeadFront();

        }
    }
}
