using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Wilki
{
    class Zajac
    {
        Random random = new Random();
        public Koordynaty koordynaty;
        Thread t;
        bool isAlive = true;
        ListaZajacy listaZajacy = new ListaZajacy();
        string imie;
        public Zajac(string imie)
        {
             koordynaty = new Koordynaty();
            this.imie = imie;
        }

        public void runZajace()
        {
            t = new Thread(new ThreadStart(wanderingZajac));
            t.Start();
        }

        void wanderingZajac()
        {
            while(this.isAlive)
            {
                this.koordynaty.moveX1();
                this.koordynaty.moveY1();
                Thread.Sleep(50);
            }
        }

        public void multiplyZajace() 
        {
            Zajac z1;
            for(int i=0; i<random.Next(5, 16); i++)
            {
                z1= new Zajac("a1");
                listaZajacy.dodajZajaca(z1);
                z1.runZajace();
                Console.WriteLine("Stworzono nowego zajaca");
            }
        }

        public void zajacZjedzony(int index)
        {
            //if(this.koordynaty.getKoordynaty()==new Tuple<int, int>(x, y))
            this.isAlive = false;
            Console.WriteLine("Zabito krolika" + index);
            listaZajacy.removeZajac(index);
        }
    }
}
