using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializePeople;
using System.IO;

namespace SerializePeopleTests
{
    [TestClass]
    public class PersonTests
    {
        Person testPerson = new Person("Lorea", Person.Genders.Female, new DateTime(1997, 3, 5));

        [TestMethod]
        public void Person_ConstructorWorksCorrectly()
        {
            Assert.AreEqual("Lorea", testPerson.Name);
            Assert.AreEqual(Person.Genders.Female, testPerson.Gender);
            Assert.AreEqual(new DateTime(1997, 3, 5), testPerson.BirthDate);
        }

        [TestMethod]
        public void Person_ToString_ShowsCorrectly()
        {
            string expected = "Name: Lorea, Gender: Female, Date of Birth: 1997.03.05, Age: 20";
            string actual = testPerson.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Person_Serialize_CreatesFile()
        {
            testPerson.Serialize("lorea.dat");
            Assert.IsTrue(File.Exists("lorea.dat"));
        }

        [TestMethod]
        public void Person_Serialization_WorksBothWay()
        {
            testPerson.Serialize("lorea.dat");
            Person deserializedPerson = Person.Deserialize("lorea.dat");
            Assert.AreEqual(testPerson.Name, deserializedPerson.Name);
            Assert.AreEqual(testPerson.Gender, deserializedPerson.Gender);
            Assert.AreEqual(testPerson.BirthDate, deserializedPerson.BirthDate);
        }
    }
}
