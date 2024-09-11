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
        Zajac zajac = new Zajac();
        bool isGlodny = true;
        Random random = new Random();
        ListaZajacy listaZajacy = new ListaZajacy();
        static Barrier barrier = new Barrier(0, b => { Zajac zajac = new Zajac(); zajac.multiplyZajace(); });
        static bool isZajacLeft = true;
        public Koordynaty koordynaty;
        static List<int> Targets = new List<int>();

        public Wilk() 
        {
            koordynaty = new Koordynaty();
            Console.WriteLine(this.koordynaty.getKoordynaty());
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
                /*this.isGlodny = true;
                while(this.isGlodny)
                {
                    mutex.WaitOne();
                    if(listaZajacy.iloscZajacy()>1)
                    {
                        int zajacDoZabicia = findNajbliszczegoZajaca(
                            this.koordynaty.getKoordynaty().Item1, this.koordynaty.getKoordynaty().Item2);
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
                    barrier.SignalAndWait();*/
                mutex.WaitOne();
                int zajacDoZabicia = findNajbliszczegoZajaca(
                           this.koordynaty.getKoordynaty().Item1, this.koordynaty.getKoordynaty().Item2);
                while(Targets.IndexOf(zajacDoZabicia)!=-1)
                {
                    zajacDoZabicia = listaZajacy.randomZajac();
                }
                Targets.Add(zajacDoZabicia);
                mutex.ReleaseMutex();
                
            }
        }

        void odpoczywanie()
        {
            mutex.ReleaseMutex();
            Thread.Sleep(1000);
            if(random.Next(101)<=50)
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
