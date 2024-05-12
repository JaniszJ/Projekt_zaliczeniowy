using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_zaliczeniowy
{
    // ENUMERATION PART I
    public enum CriminalStatus { Wanted, Custody, InPrison, Killed, Released }

    // ENUMERATION PART II
    public enum CrimeType { Burglary, Murder, Violence, Drug_dealing, Tax_fraud, Human_trafficking, Corruption, Terrorism, Cyber_crime, Extortion, Vandalism }

    // Interfaces
    public interface ISerializable
    {
        string ToString();
        void FromString(string data);
    }

    public interface ICriminal
    {
        string GetDetails();
    }

    public interface IDataBase
    {
        void SaveCriminals();
        void ReadCriminals();
    }

    public interface ISearch
    {
        ICriminal FindCriminalByName(string name);
    }

    // Helper classes
    public class Metadata
    {
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public char Sex { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public string Features { get; set; }
        public string Photo { get; set; }
    }

    public class Syndicate
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public string[] Location { get; set; }
    }

    // Abstract class
    public abstract class BasicCriminal : ICriminal, ISerializable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Aliases { get; set; }
        public CriminalStatus Status { get; set; }
        public Metadata Metadata { get; set; }
        public CrimeType Specialty { get; set; }
        public string[] KnownLocations { get; set; }
        public Syndicate Syndicate { get; set; }
        public string Verdict { get; set; }

        public abstract string ToString();
        public abstract void FromString(string data);
        public virtual string GetDetails()
        {
            string displayFirstName = string.IsNullOrEmpty(FirstName) ? "Unknown" : FirstName;
            string displayLastName = string.IsNullOrEmpty(LastName) ? "Unknown" : LastName;
            string displayAliases = Aliases == null || Aliases.Length == 0 ? "No aliases" : string.Join(", ", Aliases.Where(a => !string.IsNullOrEmpty(a)));
            return $"{displayFirstName} {displayLastName}, known as {displayAliases}. Status: {Status}.";
        }
    }

    //  Implementations
    public class Murderer : BasicCriminal
    {
        public int KillCount { get; set; }
        public string[] KnownVictims { get; set; }

        public override string ToString()
        {
            return $"Murderer: {FirstName} {LastName}, Kills: {KillCount}";
        }

        public override void FromString(string data)
        {
            
        }
    }

    public class Burglar : BasicCriminal
    {
        public int StolenValue { get; set; }

        public override string ToString()
        {
            return $"Burglar: {FirstName} {LastName}, Stolen Value: {StolenValue}";
        }

        public override void FromString(string data)
        {
            
        }
    }

    // Container class and search functionality
    public class Criminals : IDataBase, ISearch
    {
        private List<ICriminal> criminals = new List<ICriminal>();

        public void AddCriminal(ICriminal criminal)
        {
            criminals.Add(criminal);
        }

        public void RemoveCriminal(ICriminal criminal)
        {
            criminals.Remove(criminal);
        }

        public void SaveCriminals()
        {
            // Simulate saving to a database
        }

        public void ReadCriminals()
        {
            // Simulate reading from a database
        }

        public ICriminal FindCriminalByName(string name)
        {
            return criminals.FirstOrDefault(c => ((BasicCriminal)c).FirstName == name || ((BasicCriminal)c).LastName == name);
        }
    }

    // Factories
    public abstract class CriminalFactory
    {
        public abstract ICriminal Build();
    }

    public class MurdererFactory : CriminalFactory
    {
        public override ICriminal Build()
        {
            return new Murderer();
        }
    }

    public class BurglarFactory : CriminalFactory
    {
        public override ICriminal Build()
        {
            return new Burglar();
        }
    }

    // Example usage




    internal class Program
    {
        static void Main(string[] args)
        {
            var murdererFactory = new MurdererFactory();
            var murderer = murdererFactory.Build() as Murderer;
            murderer.FirstName = "Marcin";
            murderer.LastName = "Choiński";
            Metadata metadata = new Metadata();
            metadata.Height = 175;
            metadata.Weight = 75;
            metadata.DateOfBirth = new DateTime();
            metadata.HairColor = "Black";
            metadata.Sex = 'M';

            murderer.KillCount = 1000;

            var criminals = new Criminals();
            criminals.AddCriminal(murderer);

            var foundCriminal = criminals.FindCriminalByName("Marcin");
            Console.WriteLine(foundCriminal.GetDetails());
          

            Console.ReadKey();
            Console.WriteLine("Koniec programu!");
        }
    }
}
