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
        public Zajac()
        {
             koordynaty = new Koordynaty(); //ustawienie loswych koordynatow dla zajaca znajdujacych sie w srodku okreslonej strefy
             listaZajacy.dodajZajaca(this);
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
                if (listaZajacy.iloscZajacy() == 1) //jesli zostaje pojedynczy zajac, wymiera
                {
                    this.isAlive = false;
                    break;
                }
                this.koordynaty.moveX1();
                this.koordynaty.moveY1();
            }
        }

        public void multiplyZajace() //po dotarciu wilkiow do bariery, dodaje losowa ilosc nowych zajacy
        {
            Zajac z1;
            for(int i=0; i<random.Next(5, 16); i++)
            {
                z1= new Zajac();
                listaZajacy.dodajZajaca(z1);
                z1.runZajace();
                Console.WriteLine("Nowy zajac narodzony");
            }
        }

        public void zajacZjedzony()
        {
            this.isAlive = false;
            listaZajacy.removeZajac(this);
        }
    }
}
