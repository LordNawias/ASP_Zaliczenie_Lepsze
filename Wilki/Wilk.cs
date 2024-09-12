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
        //Zajac zajac = new Zajac();
        bool isGlodny = true;
        Random random = new Random();
        ListaZajacy listaZajacy = new ListaZajacy();
        static Barrier barrier = new Barrier(0, b => { Zajac zajac = new Zajac(); zajac.multiplyZajace(); });
        static bool isZajacLeft = true;
        public Koordynaty koordynaty;
        static List<int> Targets = new List<int>();
        Zajac tracking;
        Wilk wilk;

        public Wilk() 
        {
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
                    if(listaZajacy.iloscZajacy()>1)
                    {
                        mutex.WaitOne();
                        int zajacDoZabicia = findNajbliszczegoZajaca(
                                   this.koordynaty.getKoordynaty().Item1, this.koordynaty.getKoordynaty().Item2);
                        while (Targets.IndexOf(zajacDoZabicia) != -1)
                        {
                            zajacDoZabicia = listaZajacy.randomZajac();
                        }
                        Targets.Add(zajacDoZabicia);
                        this.tracking = listaZajacy.returnZajac(zajacDoZabicia);
                        mutex.ReleaseMutex();
                        while (odleglosc(this.koordynaty.getKoordynaty().Item1, this.tracking.koordynaty.getKoordynaty().Item1, this.koordynaty.getKoordynaty().Item2, this.tracking.koordynaty.getKoordynaty().Item2) > 5)
                        {
                            if (this.koordynaty.getKoordynaty().Item1 < this.tracking.koordynaty.getKoordynaty().Item1)
                            {
                                this.koordynaty.moveX3('r');
                            }
                            else
                            {
                                this.koordynaty.moveX3('l');
                            }
                            if (this.koordynaty.getKoordynaty().Item2 < this.tracking.koordynaty.getKoordynaty().Item2)
                            {
                                this.koordynaty.moveY3('u');
                            }
                            else
                            {
                                this.koordynaty.moveY3('d');
                            }
                        }
                        mutex.WaitOne();
                        tracking.zajacZjedzony();
                        Console.WriteLine("Wilka zjadl zajaca, zostalo: " + listaZajacy.iloscZajacy());
                        odpoczywanie();
                    }
                    else
                    {
                        isZajacLeft = false;
                    }
                    
                }

                if(isZajacLeft)
                {
                    barrier.SignalAndWait();
                }
                
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
                int odlegloscWzor = odleglosc(x, zajac.koordynaty.getKoordynaty().Item1, y, zajac.koordynaty.getKoordynaty().Item2);
                    odleglosci.Add(odlegloscWzor);
                }
                return odleglosci.IndexOf(odleglosci.Min());
        }

        int odleglosc(int x1, int x2, int y1, int y2)
        {
            return (int)Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }
    }
}
