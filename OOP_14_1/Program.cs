using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using Newtonsoft.Json;

namespace OOP_14_1
{
    class Program
    {
        static void Main()
        {
            Characters player1 = new Characters(231, 123, "2020_sucks", "Огненный молот");

            #region BINARY
            //Двоичная сериализация/десериализация
            Console.WriteLine("\nДвоичная сериализация/десериализация:");
            BinaryFormatter BinF = new BinaryFormatter();
            using (FileStream fstream = new FileStream(@"..\Players_bin.dat", FileMode.OpenOrCreate))
            {
                BinF.Serialize(fstream, player1);
            }

            using (FileStream fstream = new FileStream(@"..\Players_bin.dat", FileMode.Open))
            {
                Characters deserialCharacter = (Characters)BinF.Deserialize(fstream);
                Console.WriteLine(deserialCharacter);
            }
            #endregion

            #region SOAP
            //SOAP сериализация/десериализация
            Console.WriteLine("\nSOAP сериализация/десериализация:");
            SoapFormatter soapF = new SoapFormatter();
            using (FileStream fstream = new FileStream(@"..\Players_soap.dat", FileMode.OpenOrCreate))
            {
                soapF.Serialize(fstream, player1);
            }

            using (FileStream fstream = new FileStream(@"..\Players_soap.dat", FileMode.Open))
            {
                Characters deserialCharacter = (Characters)soapF.Deserialize(fstream);
                Console.WriteLine(deserialCharacter);
            }
            #endregion

            #region JSON
            //json сериализация/десериализация
            File.WriteAllText(@"..\Players_json.json", JsonConvert.SerializeObject(player1));
            var jsonCharacters = JsonConvert.DeserializeObject<Characters>(File.ReadAllText(@"..\Players_json.json"));
            Console.WriteLine("\nJSON сериализация/десериализация:\n" + jsonCharacters);
            #endregion

            #region XML
            //xml сериализация/десериализация
            Console.WriteLine("\nxml сериализация/десериализация:");
            XmlSerializer xmlF = new XmlSerializer(typeof(Characters));
            using (FileStream fstream = new FileStream(@"..\Players_xml.xml", FileMode.OpenOrCreate))
            { xmlF.Serialize(fstream, player1); }

            using (FileStream fstream = new FileStream(@"..\Players_xml.xml", FileMode.Open))
            {
                Characters deserialCharacter = (Characters)xmlF.Deserialize(fstream);
                Console.WriteLine(deserialCharacter);
            }
            #endregion

            #region COLLECTION SERIALIZATION
            Console.WriteLine("\nList<Characters> сериализация/десериализация:");
            List<Characters> characters = new List<Characters>
            {
                new Characters(123,456,"2021_make_sense","Огненный молот"), new Characters(444,555,"Obsidian","Лавовый монстр"),
                new Characters(3,97,"Cockroach","Троица"), new Characters(4, 2, "Big", "Duck")
            };

            XmlSerializer listXmlS = new XmlSerializer(typeof(List<Characters>));
            using (FileStream fstream = new FileStream(@"..\CharactersList_xml.xml", FileMode.OpenOrCreate))
            {
                listXmlS.Serialize(fstream, characters);
            }

            using (FileStream fstream = new FileStream(@"..\CharactersList_xml.xml", FileMode.Open))
            {
                List<Characters> deserialCharacters = (List<Characters>)listXmlS.Deserialize(fstream);
                foreach (var item in characters)
                    Console.WriteLine(item);
            }
            #endregion

            #region XPath
            Console.WriteLine("\nXPath:");
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@"..\CharactersList_xml.xml");

            XmlElement xRoot = xDoc.DocumentElement;
            XmlNodeList childnodes = xRoot.SelectNodes("//Characters/Attack");
            foreach (XmlNode node in childnodes)
                Console.WriteLine(node.InnerText);

            Console.WriteLine();
            Console.WriteLine("Персонажи с атакой более 100: ");
            XmlNodeList StrongCharacters = xRoot.SelectNodes("Characters[Attack >'100']");
            foreach (XmlNode node in StrongCharacters)
            {
                Console.WriteLine("\n");
                foreach (XmlNode i_node in node.ChildNodes)
                    Console.WriteLine(i_node.Name + ": " + i_node.InnerText);
            }

            XmlNodeList ProtectiveCharacters = xRoot.SelectNodes("Characters[Defence >'100']");
            Console.WriteLine("\nПерсонажи с защитой более 100: ");
            foreach (XmlNode node in ProtectiveCharacters)
            {
                Console.WriteLine("\n");
                foreach (XmlNode i_node in node.ChildNodes)
                    Console.WriteLine(i_node.Name + ": " + i_node.InnerText);
            }

            #endregion

            #region LINQ to XML
            Console.WriteLine("\nПерсонажи с атакой более 100: ");

            XDocument xdoc = XDocument.Load(@"..\CharactersList_xml.xml");
            var items = from i in xdoc.Element("ArrayOfCharacters").Elements("Characters")
                        where Convert.ToInt32(i.Element("Attack").Value) > 100
                        select i;
            Console.WriteLine("Суперспособности персонажей со значением атаки более 100: ");
            foreach (XElement item in items)
                Console.WriteLine($"\n {item.Element("SuperPower").Value}");
            #endregion


        }
    }
}
