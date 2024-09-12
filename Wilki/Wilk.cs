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
        string imie;
        static int wspolczynnikGlodu = 51;

        public Wilk(string x) 
        {
            this.koordynaty = new Koordynaty();
            this.imie = x;
        }

        public void RunWilk()
        {
            Thread t = new Thread(new ThreadStart(runningWilk));
            barrier.AddParticipant();
            t.Start();
            
        }

        
        void runningWilk()
        {
            while (isZajacLeft)  //wykonuje glowny loop tylko jesli sa zajace do zjedzenia
            {
                this.isGlodny = true;
               
                while(this.isGlodny)  //kazdy wilk ma osobnego loopa ktorego konczy w roznych momentach w zaleznosci po ilu posilkach sie nasyci
                {
                    if(listaZajacy.iloscZajacy()>1) //sprawdzanie w kazdym przejsciu czy zostaly jeszcze jakies zajace do zjedzenia
                    {
                        mutex.WaitOne(); //mutex zeby kazdy wilk odzzielnie wybral cel
                        int zajacDoZabicia = findNajbliszczegoZajaca( 
                                   this.koordynaty.getKoordynaty().Item1, this.koordynaty.getKoordynaty().Item2);
                        while (Targets.IndexOf(zajacDoZabicia) != -1)
                        {
                            zajacDoZabicia = listaZajacy.randomZajac();
                        }//wilki wybieraja najblizszy cel, chyba ze inny wilk juz sobie obral tego zajaca za cel, wtedy biora losowy
                        Targets.Add(zajacDoZabicia);
                        this.tracking = listaZajacy.returnZajac(zajacDoZabicia);
                        mutex.ReleaseMutex();//cele pojedynczo wybrane, wiec zwolnienie mutexa zeby mogly mogly poodazac za swoim celem w tym samym czasie
                        while (odleglosc(this.koordynaty.getKoordynaty().Item1, this.tracking.koordynaty.getKoordynaty().Item1, this.koordynaty.getKoordynaty().Item2, this.tracking.koordynaty.getKoordynaty().Item2) > 3)
                        {
                            Console.WriteLine("Wilk " + this.imie + this.koordynaty.getKoordynaty() + " goni za zajacem na " + this.tracking.koordynaty.getKoordynaty());
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
                        }//gonienie zajaca
                        mutex.WaitOne();//mtex w celu zablokowania wielu wilkow przed modyfikacja tej samej listy
                        tracking.zajacZjedzony();
                        Console.WriteLine("Wilk" + this.imie + "zjadl zajaca, zostalo: " + listaZajacy.iloscZajacy());
                        odpoczywanie();//kazdy wilk odpoczywa po zjedzeniu i wykonuje losowy test zeby sprawdzic czy dalej jest glodny
                       

                    }
                    else
                    {
                        isZajacLeft = false;
                    }
                    
                }

                if(isZajacLeft)//jesli sa jeszcze jakies przy zyciu, czekaja na bariere, ktora doda nowe zajace i pozwoli ponowic lowy
                {
                    barrier.SignalAndWait();
                }
                
            }
        }

        void odpoczywanie()
        {
            mutex.ReleaseMutex();
            Thread.Sleep(1000);
            if(random.Next(101)<=wspolczynnikGlodu)
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
