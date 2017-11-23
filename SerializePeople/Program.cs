using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializePeople
{
    class Program
    {
        static void Main(string[] args)
        {
            Person Lorea = new Person();
            Lorea.Name = "Lorea";
            Lorea.BirthDate = new DateTime(1997, 3, 5);
            Lorea.Gender = Person.Genders.Female;
            Lorea.Serialize("lorea-data.dat");
            Console.ReadKey();
        }
    }
}
