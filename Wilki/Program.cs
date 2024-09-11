using System;

namespace Wilki
{
    class Program
    {
        static void Main(string[] args)
        {
            ListaZajacy listaZajacy = new ListaZajacy();
            Zajac z1 = new Zajac("a");
            Zajac z2 = new Zajac("b");
            Zajac z3 = new Zajac("c");
            Zajac z4 = new Zajac("d");
            Zajac z5 = new Zajac("e");
            z1.runZajace();
            z2.runZajace();
            z3.runZajace();
            z4.runZajace();
            z5.runZajace();
            listaZajacy.dodajZajaca(z1);
            listaZajacy.dodajZajaca(z2);
            listaZajacy.dodajZajaca(z3);
            listaZajacy.dodajZajaca(z4);
            listaZajacy.dodajZajaca(z5);
            Wilk w1 = new Wilk();
            Wilk w2 = new Wilk();
            w1.RunWilk();
            w2.RunWilk();
        }
            
    }
}