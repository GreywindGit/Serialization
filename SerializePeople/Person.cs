﻿using System;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializePeople
{
    [Serializable]
    public class Person : IDeserializationCallback
    {
        [NonSerialized] private int age;
        public enum Genders { Male, Female }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Genders Gender { get; set; }

        public Person(string name = "", Genders gender = Genders.Female, DateTime birthDate = new DateTime())
        {
            this.Name = name;
            this.Gender = gender;
            this.BirthDate = birthDate;
            this.age = DateTime.Now.Year - BirthDate.Year;
        }

        public void Serialize(string outputFile)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(outputFile))
                File.Delete(outputFile);
            using (FileStream stream = new FileStream(outputFile, FileMode.Create))
            {
                try
                {
                    formatter.Serialize(stream, this);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine($"Failed to serialize due to: {e.Message}");
                }
            }
        }

        public static Person Deserialize(string sourceFile)
        {
            Person person = new Person();
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                using (FileStream stream = new FileStream(sourceFile, FileMode.Open))
                {
                    try
                    {
                        person = (Person) formatter.Deserialize(stream);
                    }
                    catch (SerializationException e)
                    {
                        Console.WriteLine($"Failed to deserialize due to: {e.Message}");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
            return person;
        }

        public void OnDeserialization(object sender)
        {
            this.age = DateTime.Now.Year - BirthDate.Year;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Gender: {Gender}, Date of Birth: {BirthDate.ToString("yyyy.MM.dd")}, Age: {age}";
        }
    }
}
