using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace klijent2
{
    class UpisIspisDatoteka
    {
        public void Upis(string tekst, string putanja)
        {
            File.WriteAllText(putanja, tekst);
        }

        public string Citanje(string putanja)
        {
            string tekst = File.ReadAllText(putanja);
            return tekst;
        }
    }
}
