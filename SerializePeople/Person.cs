using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace SerializePeople
{
    [Serializable]
    class Person
    {
        public enum Genders { Male, Female }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Genders Gender { get; set; }
        public int Age => DateTime.Now.Year - BirthDate.Year;

        public void Serialize(string outputFile)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            PropertyInfo[] properties = typeof(Person).GetProperties();
            Dictionary<string, object> personMap = new Dictionary<string, object>();
            foreach (PropertyInfo property in properties)
            {
                personMap[property.Name] = property.GetValue(this);
            }
            if (File.Exists(outputFile))
                File.Delete(outputFile);
            using (FileStream stream = new FileStream(outputFile, FileMode.Create))
            {
                try
                {
                    formatter.Serialize(stream, personMap);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine($"Failed to serialize due to: {e.Message}");
                }
            }
        }

        public override string ToString()
        {
            return $"Name: {Name}, Gender: {Gender}, Date of Birth: {BirthDate.ToString("yyyy.MM.dd")}, Age: {Age}";
        }
    }
}
