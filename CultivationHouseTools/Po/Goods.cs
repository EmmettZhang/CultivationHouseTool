using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultivationHouseTools
{
    public class Goods
    {
        public string name;

        public int index;

        public int weight;

        public Goods(string name, int index, int weight)
        {
            this.name = name;
            this.index = index;
            this.weight = weight;
        }

        public Goods(int weight)
        {
            this.weight = weight;
        }

        public override string ToString()
        {
            return $"{index}、name: {name}, weight: {weight}";
        }
    }
}
