using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Serializacao
{
    class Program
    {
        static void Main(string[] args)
        {
            var cao = new Animal(Guid.NewGuid(), "Cão", "Roberto Inácio", 30m, 0.8m);

            //binário
            serializaDeserealizaBinario(cao);


            //xml
            serializaDeserealizaXml(cao);

            //json
            serializaDeserializaJson(cao);

            Console.ReadLine();
        }

        private static void serializaDeserializaJson(Animal cao)
        {
            cao.Especie = "Cão Json";
            string jsonData = JsonConvert.SerializeObject(cao);

            using (TextWriter tw = new StreamWriter("DadosAnimal.json"))
            {
                tw.Write(jsonData);
            }

            cao = null;

            cao = JsonConvert.DeserializeObject<Animal>(jsonData);

            Console.WriteLine(cao.ToString());
        }

        private static void serializaDeserealizaXml(Animal cao)
        {
            cao.Especie = "Cão XML";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Animal));

            using (TextWriter tw = new StreamWriter("DadosAnimal.xml"))
            {
                xmlSerializer.Serialize(tw, cao);
            }

            cao = null;

            XmlSerializer xmlDeSerializer = new XmlSerializer(typeof(Animal));

            TextReader textReader = new StreamReader("DadosAnimal.xml");


            var animal = xmlDeSerializer.Deserialize(textReader);

            cao = (Animal)animal;
            textReader.Close();

            Console.WriteLine(cao.ToString());
        }

        private static void serializaDeserealizaBinario(Animal cao)
        {
            cao.Especie = "Cão DAT";
            Stream stream = File.Open("DadosAnimal.dat", FileMode.Create);

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, cao);
            stream.Close();

            cao = null;

            stream = File.Open("DadosAnimal.dat", FileMode.Open);
            bf = new BinaryFormatter();

            cao = (Animal)bf.Deserialize(stream);

            stream.Close();

            Console.WriteLine(cao.ToString());
        }
    }

    [Serializable]
    public class Animal : ISerializable
    {

        public Guid Id { get; set; }
        public string Especie { get; set; }
        public string Nome { get; set; }
        public decimal Peso { get; set; }
        public decimal Altura { get; set; }

        public Animal()
        {

        }

        public Animal(Guid id, string especie, string nome, decimal peso, decimal altura)
        {
            Id = id;
            Nome = nome;
            Peso = peso;
            Altura = altura;
            Especie = especie;
        }

        public Animal(SerializationInfo info, StreamingContext context)
        {
            Id = (Guid)info.GetValue("Id", typeof(Guid));
            Nome = (string)info.GetValue("Nome", typeof(string));
            Peso = (decimal)info.GetValue("Peso", typeof(decimal));
            Altura = (decimal)info.GetValue("Altura", typeof(decimal));
            Especie = (string)info.GetValue("Especie", typeof(string));
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id);
            info.AddValue("Nome", Nome);
            info.AddValue("Peso", Peso);
            info.AddValue("Altura", Altura);
            info.AddValue("Especie", Especie);
        }


        public override string ToString()
        {
            return $"ID {Id} - O {Especie} {Nome} pesa {Peso.ToString()}kg e tem {Altura.ToString()} m";
        }
    }
}
