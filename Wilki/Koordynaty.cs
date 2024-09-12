using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wilki
{
    class Koordynaty
    {
        int x;
        int y;
        Tuple<int, int> boxArea = new Tuple<int, int>(50, 50);
        Random random = new Random();

        public Koordynaty()
        {
            this.x = random.Next(1, boxArea.Item1);
            this.y = random.Next(1, boxArea.Item2);
        }

        public void moveX1()
        {
            if (random.Next(0, 101) < 50)
            {
                if (this.x > 0)
                {
                    this.x--;
                }
                else
                {
                    this.x++;
                }
            }
            else
            {
                if (this.x < boxArea.Item1)
                {
                    this.x++;
                }
                else
                {
                    this.x--;
                }
            }
        }

        public void moveY1()
        {
            if (random.Next(0, 101) < 50)
            {
                if (this.y > 0)
                {
                    this.y--;
                }
                else
                {
                    this.y++;
                }
            }
            else
            {
                if (this.y < boxArea.Item2)
                {
                    this.y++;
                }
                else
                {
                    this.y--;
                }
            }
        }

        public void moveX3(char dir)
        {
            if (dir == 'l')
                this.x -= 3;
            if (dir == 'r')
                this.x += 3;

        }

        public void moveY3(char dir)
        {
            if (dir == 'u')
                this.y += 3;
            if (dir == 'd')
                this.y -= 3;
        }
        public Tuple<int, int> getKoordynaty()
        {
            return new Tuple<int, int> (this.x, this.y);
        }
    }
}
