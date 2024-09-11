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
                isGlodny = true;
                while(this.isGlodny)
                {
                    mutex.WaitOne();
                    Console.WriteLine(listaZajacy.iloscZajacy());
                    if(listaZajacy.iloscZajacy()>0)
                    {
                        int zajacDoZabicia = listaZajacy.randomZajac();
                        zajac = listaZajacy.returnZajac(zajacDoZabicia);
                        zajac.zajacZjedzony(zajacDoZabicia);
                        odpoczywanie();
                    }
                    else
                    {
                        isZajacLeft=false;
                        odpoczywanie();
                        isGlodny=false;
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
            if(random.Next(101)<=50)
            {
                this.isGlodny = false;
            }
        }


    }
}
