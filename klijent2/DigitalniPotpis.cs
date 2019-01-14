using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace klijent2
{
    class DigitalniPotpis
    {
        public void KreiranjeKluceva()
        {
            UpisIspisDatoteka upis = new UpisIspisDatoteka();
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            string putanjaKlijent = "D:\\DS\\1. SEMESTAR\\SIS\\klijent2\\klijent2\\klijent2";
            string putanjaServer = "D:\\DS\\1. SEMESTAR\\SIS\\srver2\\srver2\\srver2";
            string privatniKljuc = csp.ToXmlString(true);
            string javniKljuc = csp.ToXmlString(false);
            upis.Upis(privatniKljuc, putanjaKlijent + "\\privatniKljucKlijent.txt");
            upis.Upis(javniKljuc, putanjaServer + "\\javniKljucKlijent.txt");
        }
        public string VratiHash(string tekst)
        {
            byte[] tekstUBitovima = Encoding.UTF8.GetBytes(tekst);
            byte[] hashTekstUBitovima;

            using (SHA256CryptoServiceProvider shaHash = new SHA256CryptoServiceProvider())
            {
                hashTekstUBitovima = shaHash.ComputeHash(tekstUBitovima);
            }

            return Convert.ToBase64String(hashTekstUBitovima);
        }

        public string KriptiraniHash(string tekst, string kljuc)
        {
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048))
            {
                csp.FromXmlString(kljuc);
                RSAPKCS1SignatureFormatter formater = new RSAPKCS1SignatureFormatter(csp);
                formater.SetHashAlgorithm("SHA256");
                byte[] digitalniPotpis = formater.CreateSignature(Convert.FromBase64String(tekst));
                return Convert.ToBase64String(digitalniPotpis);
            }
        }

        public bool ProvjeraDigitalnogPotpisa(string sazetak, string digitalniPotpis, string kljuc)
        {
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048))
            {
                csp.FromXmlString(kljuc);
                RSAPKCS1SignatureDeformatter formater = new RSAPKCS1SignatureDeformatter(csp);
                formater.SetHashAlgorithm("SHA256");
                if (formater.VerifySignature(Convert.FromBase64String(sazetak), Convert.FromBase64String(digitalniPotpis)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}
