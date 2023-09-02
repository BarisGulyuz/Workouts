using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using WeCantSpell.Hunspell;

namespace Workouts.FileOperations
{
    public class FileOperations
    {
        public static List<string> FindAllWrongs(string path)
        {
            var document = WordprocessingDocument.Open(path, false);
            return TurkishSpellCheck(document.MainDocumentPart.Document.Body.InnerText);
        }
        static List<string> TurkishSpellCheck(string text)
        {
            List<string> mistakes = new List<string>();
            string[] words = Regex.Split(text, @"\W+");

            using var dictionaryStream = File.OpenRead(@"tr_TR.dic");
            using var affixStream = File.OpenRead(@"tr_TR.aff");
            var dictionary = WordList.CreateFromStreams(dictionaryStream, affixStream);

            foreach (var word in words)
            {
                if (dictionary.Check(word)) 
                    mistakes.Add(word);
            }

            return mistakes;
        }
    }
}

