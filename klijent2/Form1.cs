using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace klijent2
{
    public partial class Form1 : Form
    {
        string primljenaPoruka;
        string kriptiraniHashSlanje;
        string kriptiraniHashPrimanje;
        int brojPaketaZaPrimanje = 0;
        int brojacPrimljenihPaketa = 0;
        int brojcek = 0;

        SocketAsyncEventArgs argumentiSlanje = new SocketAsyncEventArgs();
        Socket soket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        SocketAsyncEventArgs argumentiPrimanje = new SocketAsyncEventArgs();
        byte[] poruka = new byte[1024];
        public Form1()
        {
            InitializeComponent();
            argumentiSlanje.RemoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8007);
            argumentiPrimanje.SetBuffer(poruka, 0, 1024);
            argumentiPrimanje.Completed += ObradaPoruke;
            soket.ExclusiveAddressUse = false;

        }
        void ObradaPoruke(object sender, SocketAsyncEventArgs e)
        {
            if (e.LastOperation == SocketAsyncOperation.Receive)
            {
                string primljenaPorukaString = UTF8Encoding.UTF8.GetString(argumentiPrimanje.Buffer, 0, e.BytesTransferred);
                if (primljenaPorukaString.IndexOf("poruka:") == -1 && int.TryParse(primljenaPorukaString, out brojPaketaZaPrimanje))
                {
                    if (brojPaketaZaPrimanje != 0)
                    {
                        brojcek = brojPaketaZaPrimanje;
                    }
                    
                    brojacPrimljenihPaketa = 0;
                    primljenaPoruka = "";
                }
                else
                {
                    primljenaPoruka = primljenaPoruka + primljenaPorukaString;
                    brojacPrimljenihPaketa++;
                    if (brojacPrimljenihPaketa == brojcek)
                    {
                        int duljinaStringa = primljenaPoruka.Length;
                        int indeksKriptiranogHasha = primljenaPoruka.IndexOf("kriptiraniHash:");
                        string porukaSZaglavljem = primljenaPoruka.Remove(indeksKriptiranogHasha);
                        string kriptiraniHashSaZaglavljem = primljenaPoruka.Remove(0, indeksKriptiranogHasha);
                        primljenaPoruka = porukaSZaglavljem.Remove(0, 7);
                        kriptiraniHashPrimanje = kriptiraniHashSaZaglavljem.Remove(0, 15);
                        uiOutputPrimljenaPoruka.Invoke((MethodInvoker)delegate
                        {

                            uiOutputPrimljenaPoruka.Text = porukaSZaglavljem + Environment.NewLine + Environment.NewLine + kriptiraniHashSaZaglavljem;

                        });
                    }
                   // Ispis(primljenaPoruka);
                }
                
                soket.ReceiveAsync(argumentiPrimanje);

            }
        }
        private void Ispis(string message)
        {
            UpisIspisDatoteka uid = new UpisIspisDatoteka();
            int duljinaStringa = message.Length;
            int indeksKriptiranogHasha = message.IndexOf("kriptiraniHash:");
            string porukaSZaglavljem = message.Remove(indeksKriptiranogHasha);
            string kriptiraniHashSaZaglavljem = message.Remove(0, indeksKriptiranogHasha);
            primljenaPoruka = porukaSZaglavljem.Remove(0, 7);
            kriptiraniHashPrimanje = kriptiraniHashSaZaglavljem.Remove(0, 15);
            uiOutputPrimljenaPoruka.Invoke((MethodInvoker)delegate
            {

                uiOutputPrimljenaPoruka.Text = porukaSZaglavljem + Environment.NewLine + Environment.NewLine + kriptiraniHashSaZaglavljem;

            });

        }
        private void Slusanje()
        {
            soket.Bind(new IPEndPoint(IPAddress.Any, 8006));
            soket.ReceiveAsync(argumentiPrimanje);
        }

        private void PosaljiPoruku(byte[] slanje)
        {
            
            argumentiSlanje.SetBuffer(slanje, 0, slanje.Length);
            soket.SendToAsync(argumentiSlanje);
            System.Threading.Thread.Sleep(100);
            
        }

        private void uiActionPosalji_Click(object sender, EventArgs e)
        {
            kriptiraniHashSlanje = "";
            string poruka = uiInputTekstZaSlanje.Text;
            DigitalniPotpis dp = new DigitalniPotpis();
            UpisIspisDatoteka uid = new UpisIspisDatoteka();
            string kljuc = uid.Citanje("D:\\DS\\1. SEMESTAR\\SIS\\klijent2\\klijent2\\klijent2\\privatniKljucKlijent.txt");
            int brojPaketaZaHash;
            byte[] porukaUBitovima = Encoding.UTF8.GetBytes(poruka);

            if (porukaUBitovima.Length % 256 == 0)
            {
                brojPaketaZaHash = porukaUBitovima.Length / 256;
            }
            else
            {
                brojPaketaZaHash = (porukaUBitovima.Length / 256) + 1;
            }

            for (int i = 0; i < brojPaketaZaHash; i++)
            {

                int pocetak = i * 256;
                int kraj;
                if (porukaUBitovima.Length < (i + 1) * 256)
                {
                    kraj = porukaUBitovima.Length;
                }
                else
                {
                    kraj = (i + 1) * 256;
                }
                int brojac = 0;
                byte[] pomocnoPolje = new byte[256];
                for (int j = pocetak; j < kraj; j++)
                {
                    pomocnoPolje[brojac] = porukaUBitovima[j];
                    brojac++;
                }

                kriptiraniHashSlanje = kriptiraniHashSlanje + dp.KriptiraniHash(dp.VratiHash(Encoding.UTF8.GetString(pomocnoPolje)), kljuc);

            }
            string digitalniPotpis = "poruka:" + poruka + "kriptiraniHash:" + kriptiraniHashSlanje;
            byte[] slanje = new byte[1024];
            slanje = UTF8Encoding.UTF8.GetBytes(digitalniPotpis);
            int brojPaketa;
            if (slanje.Length > 1024)
            {
                if (slanje.Length % 1024 == 0)
                {
                    brojPaketa = slanje.Length / 1024;
                }
                else
                {
                    brojPaketa = slanje.Length / 1024;
                    brojPaketa++;
                }
                byte[] brojPaketaBitovi = UTF8Encoding.UTF8.GetBytes(brojPaketa.ToString());


                for (int i = 0; i < brojPaketa + 1; i++)
                {
                    if (i == 0)
                    {
                        PosaljiPoruku(brojPaketaBitovi);
                    }
                    else
                    {
                        int pocetak = (i - 1) * 1024;
                        int kraj;
                        if (slanje.Length < i * 1024)
                        {
                            kraj = slanje.Length;
                        }
                        else
                        {
                            kraj = i * 1024;
                        }

                        byte[] pomocnoPolje = new byte[1024];
                        int brojac = 0;
                        for (int j = pocetak; j < kraj; j++)
                        {
                            pomocnoPolje[brojac] = slanje[j];
                            brojac++;
                        }
                        PosaljiPoruku(pomocnoPolje); ;
                    }
                }
            }
            else
            {
                brojPaketa = 1;
                PosaljiPoruku(UTF8Encoding.UTF8.GetBytes(brojPaketa.ToString()));
                PosaljiPoruku(slanje);
            }
            uiInputTekstZaSlanje.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Slusanje();
        }

        private void uiActionKreirajKljuceve_Click(object sender, EventArgs e)
        {
            DigitalniPotpis dp = new DigitalniPotpis();
            dp.KreiranjeKluceva();
        }

        private void uiActionProvjeraPoruke_Click(object sender, EventArgs e)
        {
            UpisIspisDatoteka uid = new UpisIspisDatoteka();
            string kljuc = uid.Citanje("D:\\DS\\1. SEMESTAR\\SIS\\klijent2\\klijent2\\klijent2\\javniKljucServera.txt");
            DigitalniPotpis dp = new DigitalniPotpis();

            bool provjera = true;
            byte[] pomocnoPolje;
            int velicinaPaketa = 172;
            int brojPaketaZaDekriptiranje = Encoding.UTF8.GetBytes(kriptiraniHashPrimanje).Length / velicinaPaketa;
            byte[] porukaUBitovimaDekriptiranje = Encoding.UTF8.GetBytes(kriptiraniHashPrimanje);
            byte[] porukaUBitovima = Encoding.UTF8.GetBytes(primljenaPoruka);

            for (int i = 0; i < brojPaketaZaDekriptiranje; i++)
            {
                int pocetak = i * velicinaPaketa;
                int kraj;
                if (porukaUBitovimaDekriptiranje.Length < (i + 1) * velicinaPaketa)
                {
                    kraj = porukaUBitovimaDekriptiranje.Length;
                }
                else
                {
                    kraj = (i + 1) * velicinaPaketa;
                }
                int brojacDekriptiranje = 0;
                byte[] pomocnoPoljeDekriptiranje = new byte[velicinaPaketa];
                for (int j = pocetak; j < kraj; j++)
                {
                    pomocnoPoljeDekriptiranje[brojacDekriptiranje] = porukaUBitovimaDekriptiranje[j];
                    brojacDekriptiranje++;
                }


                int pocetakk = i * 256;
                int krajj;
                if (porukaUBitovima.Length < (i + 1) * 256)
                {
                    krajj = porukaUBitovima.Length;
                }
                else
                {
                    krajj = (i + 1) * 256;
                }
                int brojac = 0;
                pomocnoPolje = new byte[256];
                for (int k = pocetakk; k < krajj; k++)
                {
                    pomocnoPolje[brojac] = porukaUBitovima[k];
                    brojac++;
                }

                string hash = dp.VratiHash(Encoding.UTF8.GetString(pomocnoPolje));
                provjera = provjera && dp.ProvjeraDigitalnogPotpisa(hash, Encoding.UTF8.GetString(pomocnoPoljeDekriptiranje), kljuc);



            }
            if (provjera)
            {
                MessageBox.Show("Poruka nije izmjenjena.");
            }
            else
            {
                MessageBox.Show("Poruka je izmjenjena.");
            }
        }
    }
}
