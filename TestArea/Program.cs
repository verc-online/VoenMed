using TextFormattingHelper;
using VoenMedLibrary.Models;

namespace TestArea
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string savePath = "C:\\Users\\LisiPisi\\Documents\\Foms100";

            WordDocumentDocx helper = new();
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
            {
                {"%IssuedBy%", "А-хssз-когда"}
            };

            helper.CreateForm100(keyValuePairs, savePath);

        }

    }
}
