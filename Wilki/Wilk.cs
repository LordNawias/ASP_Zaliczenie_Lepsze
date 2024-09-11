using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wilki
{
    class Wilk
    {
        
        static Mutex mutex = new Mutex();
        Zajac zajac = new Zajac("");
        bool isGlodny = true;
        Random random = new Random();
        ListaZajacy listaZajacy = new ListaZajacy();
        static Barrier barrier = new Barrier(0, b => { Zajac zajac = new Zajac(""); zajac.multiplyZajace(); });
        static bool isZajacLeft = true;
        public Koordynaty koordynaty;
        string imie;


        public Wilk(string a) 
        {
            this.imie = a;
            koordynaty = new Koordynaty();
        }

        public void RunWilk()
        {
            Thread t = new Thread(new ThreadStart(runningWilk));
            barrier.AddParticipant();
            t.Start();
            
        }

        
        void runningWilk()
        {
            while (isZajacLeft)
            {
                this.isGlodny = true;
                while(this.isGlodny)
                {
                    mutex.WaitOne();
                    Console.WriteLine(listaZajacy.iloscZajacy());
                    if(listaZajacy.iloscZajacy()>1)
                    {
                        int zajacDoZabicia = findNajbliszczegoZajaca(
                            this.koordynaty.getKoordynaty().Item1, this.koordynaty.getKoordynaty().Item2);
                        // zajacDoZabicia = listaZajacy.randomZajac();
                        zajac = listaZajacy.returnZajac(zajacDoZabicia);
                        zajac.zajacZjedzony(zajacDoZabicia);
                        odpoczywanie();
                    }
                    else
                    {
                        isZajacLeft=false;
                        odpoczywanie();
                        this.isGlodny=false;
                        break;
                    }
                }
                if(isZajacLeft)
                    barrier.SignalAndWait();
            }
        }

        void odpoczywanie()
        {
            mutex.ReleaseMutex();
            Thread.Sleep(1000);
            if(random.Next(101)<=10)
            {
                this.isGlodny = false;
            }
        }

        int findNajbliszczegoZajaca(int x, int y)
        {
                List<int> odleglosci = new List<int>();
                foreach (Zajac zajac in listaZajacy.lista())
                {
                    int odlegloscWzor = (int)Math.Sqrt(Math.Pow((zajac.koordynaty.getKoordynaty().Item1 - x), 2) + Math.Pow((zajac.koordynaty.getKoordynaty().Item2 - y), 2));
                    odleglosci.Add(odlegloscWzor);
                }
                return odleglosci.IndexOf(odleglosci.Min());
        }
    }
}
