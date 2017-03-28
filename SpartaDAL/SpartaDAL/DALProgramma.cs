using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta.Dal
{
    public class DALProgramma
    {
        public static List<string> GetTestLijst()
        {
            List<string> lijst = new List<string>();
            lijst.Add("Davut Demir");
            lijst.Add("Juan Albergen");
            lijst.Add("Joost Kingma");
            lijst.Add("Kaas is een voedingsmiddel gemaakt van melk.");
            lijst.Add("Het Nederlandse woord voor kaas stamt van het Latijnse caseus, dat dezelfde betekenis heeft.");
            lijst.Add("Kaas wordt een 'levend' voedsel genoemd omdat er miljoenen bacteriën in leven en vaak ok schimmels.");
            lijst.Add("In de korst van vooral oude kazen zoals de Mimolette bevindt zich de kaasmijt die gaten en spleten in de kaas vreet.");

            return lijst;
        }
    }
}
