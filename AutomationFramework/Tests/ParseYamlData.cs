using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationFramework.Common;
using NUnit.Framework;
using System.IO;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using System.Collections;
using YamlDotNet.RepresentationModel;
using static System.Net.Mime.MediaTypeNames;
using System.Net;

namespace AutomationFramework.Tests
{
    public class ParseYamlData
    {
        [Test]
        public void ParseYmlFileTest()
        {
            //String filepath = Application + "/StreamingAssets/SolarSystem/";
            string path = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
            string path1 = AppDomain.CurrentDomain.BaseDirectory;
            string path2 = System.IO.Directory.GetCurrentDirectory();
            Console.WriteLine("path: " + path2 + "..\\..\\Data\\Data.txt");
            var Document1 = path1 + "..\\..\\Data\\Data.txt";

            var yaml1 = @"
Groups:
  - Name: ATeam
    FirstName, LastName, Age, Height:
      - [Joe, Soap, 21, 184]
      - [Mary, Ryan, 20, 169]
      - [Alex, Dole, 24, 174]
";

            var yaml2 = @"
-Label: entry
    - Layer: x
    - id: B35E246039E1CB70
    - Ref: B35E246039E1CB70
 Label: Info
    - Layer: x
    - id: CE0BEFC7022283A6
    - Ref: CE0BEFC7022283A6
 Label: entry
    - Layer: HttpWebRequest
    - id: 6DAA24FF5B777506
          ";

            var Document = @"
            receipt:    Oz-Ware Purchase Invoice
            date:        2007-08-06
            customer:
                given:   Dorothy
                family:  Gale

            items:
                - part_no:   A4786
                  descrip:   Water Bucket (Filled)
                  price:     1.47
                  quantity:  4

                - part_no:   E1628
                  descrip:   High Heeled ""Ruby"" Slippers
                  price:     100.27
                  quantity:  1

            bill-to:  &id001
                street: |
                        123 Tornado Alley
                        Suite 16
                city:   East Westville
                state:  KS

            ship-to:  *id001

            specialDelivery:  >
                Follow the Yellow Brick
                Road to the Emerald City.
                Pay no attention to the
                man behind the curtain.
  ";

var Iconyml = @"
icons:
  - name:       Glass
    id:         glass
    unicode:    f000
    created:    1.0
    filter:
      - martini
      - drink
      - bar
      - alcohol
      - liquor
    categories:
      - Web Application Icons

  - name:       Music
    id:         music
    unicode:    f001
    created:    1.0
    filter:
      - note
      - sound
    categories:
      - Web Application Icons

  - name:       Search
    id:         search
    unicode:    f002
    created:    1.0
    filter:
      - magnify
      - zoom
      - enlarge
      - bigger
    categories:
      - Web Application Icons
";

            Console.WriteLine("Read YAML Data File.");
            // Setup the input
            var input = new StringReader(Document);
            // Load the stream
            var yaml = new YamlStream();
            yaml.Load(input);
            // Examine the stream
            var mapping =
            (YamlMappingNode)yaml.Documents[0].RootNode;
            // Loop trough all child entries and print our keys
            string key = string.Empty;
            foreach (var entry in mapping.Children)
            {
                var myKey = ((YamlScalarNode)entry.Key).Value;
                Console.WriteLine("Print Key: {0}", myKey);
                var receiptNode = new YamlScalarNode("receipt");
                var customerNode = new YamlScalarNode("customer");

                Assert.IsTrue(mapping.Children.ContainsKey(receiptNode), "Document is missing receipt node.");
                if (mapping.Children.ContainsKey(receiptNode))
                {
                    Console.WriteLine("Print root node count: {0}", mapping.Children.Count());
                }
                if (mapping.Children.ContainsKey(customerNode))
                {
                    var customer = mapping.Children[customerNode] as YamlMappingNode;

                    Console.WriteLine("Print customer node children count: {0}", customer.Count());
                }
                //The next line works fine
                //var node = entry.Key as YamlScalarNode;
                //alternative to above line
                var node = (YamlScalarNode)entry.Key;
                if (node != null)
                {
                    Console.WriteLine("Key: {0} {1}", node.Value, entry.Value);
                }

                if (myKey != "items")
                {
                    continue;
                }
                YamlScalarNode myYamlScalarNode = new YamlScalarNode(myKey);
                var tmpItem = mapping.Children[myYamlScalarNode];

                var items = (YamlSequenceNode)tmpItem;
                foreach (YamlMappingNode item in items)
                {
                    Console.WriteLine(
                    "{0}\t{1}",
                    item.Children[new YamlScalarNode("part_no")],
                    item.Children[new YamlScalarNode("descrip")]
                                        );
                }

            }

        }

        [Test]
        public void ParseYmlDeserializeTest()
        {
            Console.WriteLine("Test");
            string filePath = "https://raw.githubusercontent.com/FortAwesome/Font-Awesome/master/src/icons.yml";
            WebClient client = new WebClient();
            string reply = client.DownloadString(filePath);
            //Console.WriteLine("print reply: " + reply);
            var input = new StringReader(reply);
            Deserializer deserializer = new Deserializer();
            //var root = deserializer.Deserialize<Rootobject>(input);
            Rootobject root = deserializer.Deserialize<Rootobject>(input);
            if (root == null)
            {
                Console.WriteLine("Root is null");
            }
            else
            {
                //Console.WriteLine("Root is not null: " + root.icons.Length);
                Console.WriteLine("Root is not null: " + root.icons.Count);
                foreach (var icon in root.icons)
                {
                    //Music
                    if (icon.name == "Glass")
                    {
                        Console.WriteLine("name: " + icon.name);
                        Console.WriteLine("id: " + icon.id);
                        Console.WriteLine("unicode: " + icon.unicode);
                        Console.WriteLine("created: " + icon.created);

                        foreach (var filter in icon.filter)
                        {
                            Console.WriteLine("filter: " + filter.ToString());
                        }

                        foreach (var category in icon.categories)
                        {
                            Console.WriteLine("category: " + category.ToString());
                        }

                    }


                }
            }

        }

        [Test]
        public void ParseYmlIcons()
        {
            var Iconyml = @"
icons:
  - name:       Glass
    id:         glass
    unicode:    f000
    created:    1.0
    filter:
      - martini
      - drink
      - bar
      - alcohol
      - liquor
    categories:
      - Web Application Icons

  - name:       Music
    id:         music
    unicode:    f001
    created:    1.0
    filter:
      - note
      - sound
    categories:
      - Web Application Icons

  - name:       Search
    id:         search
    unicode:    f002
    created:    1.0
    filter:
      - magnify
      - zoom
      - enlarge
      - bigger
    categories:
      - Web Application Icons
";

            // Setup the input
            var input = new StringReader(Iconyml);
            //var input = new StringReader(Iconyml);
            //String yamlText = File.ReadAllText(Iconyml);
            // Load the stream
            //var yaml = new YamlStream();
            //yaml.Load(input);
            Deserializer deserializer = new Deserializer();
            //Rootobject root = deserializer.Deserialize<Rootobject>(new StringReader(Iconyml));
            Rootobject root = deserializer.Deserialize<Rootobject>(input);
            //ReDocument root = deserializer.Deserialize<ReDocument>(input);
            if (root == null)
            {
                Console.WriteLine("Root is null");
            }
            else
            {
                Console.WriteLine("Root is not null: " + root.icons.Count);
            }
        }

        [Test]
        public void ParseYmlReceipts()
        {
            var receiptsyml = @"
receipts:
  - receipt:    Oz-Ware Purchase Invoice
    date:        2007-08-06
    customer:
      given:   Dorothy
      family:  Gale
    items:
        - part_no:   A4786
          descrip:   Water Bucket (Filled)
          price:     1.47
          quantity:  4
        - part_no:   E1628
          descrip:   High Heeled Ruby Slippers
          price:     100.27
          quantity:  1
    billto:  &id001
        street: |
                123 Tornado Alley
                Suite 16
        city:   East Westville
        state:  KS
    shipto:  ""*id001""
    specialDelivery:  >
        Follow the Yellow Brick
        Road to the Emerald City.
        Pay no attention to the
        man behind the curtain.
";

            // Setup the input
            var input = new StringReader(receiptsyml);
            Deserializer deserializer = new Deserializer();
            //Console.WriteLine("input: " + input.ToString());
            //ReDocument root = deserializer.Deserialize<ReDocument>(new StringReader(receiptsyml));
            ReDocument root = deserializer.Deserialize<ReDocument>(input);
            if (root == null)
            {
                Console.WriteLine("Root is null");
            }
            else
            {
                Console.WriteLine("Root is not null: " + root.receipts.Count);
                foreach (var receipt in root.receipts)
                {
                    Console.WriteLine("receipt: " + receipt.receipt);
                    Console.WriteLine("date: " + receipt.date);
                    Console.WriteLine("customer: " + receipt.customer.Count);
                    //get given name
                    var given = receipt.customer["given"];
                    //get family name
                    var family = receipt.customer["family"];
                    //print details
                    Console.WriteLine("given: " + given);
                    Console.WriteLine("family: " + family);
                    //get family name
                    //Console.WriteLine("key: " + cust.Key);
                    //Console.WriteLine("value: " + cust.Value);
                    foreach (var item in receipt.items)
                    {
                            Console.WriteLine("part_no: " + item.part_no);
                            Console.WriteLine("descrip: " + item.descrip);
                            Console.WriteLine("price: " + item.price);
                            Console.WriteLine("quantity: " + item.quantity);
                        }
                    //print billto address
                    //Console.WriteLine("billto: " + receipt.billto["billto"]);
                    Console.WriteLine("street: " + receipt.billto["street"]);
                    Console.WriteLine("city: " + receipt.billto["city"]);
                    Console.WriteLine("state: " + receipt.billto["state"]);
                    //print shipto
                    Console.WriteLine("shipto: " + receipt.shipto);
                    //print specialDelivery
                    Console.WriteLine("specialDelivery: " + receipt.specialDelivery);

                }

            }

        }

        public class Rootobject
        {
            //public Icon[] icons { get; set; }
            public List<Icon> icons { get; set; }
        }

        public class Icon
        {
            public string[] categories { get; set; }
            public object created { get; set; }
            public string[] filter { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string unicode { get; set; }
            public string[] aliases { get; set; }
            public string[] label { get; set; }
            public string[] code { get; set; }
            public string url { get; set; }
        }
        public class ReDocument
        {
            public List<Receipt> receipts { get; set; }
        }

        public class Receipt
        {
            public string receipt { get; set; }
            public string date { get; set; }
            //public string[] customer { get; set; }
            public IDictionary<string, String> customer { get; set; }
            public List<Item> items { get; set; }
            public IDictionary<string, String> billto { get; set; }
            public string shipto { get; set; }
            public string specialDelivery { get; set; }

        }

        public class Item
        {
            public string part_no { get; set; }
            public string descrip { get; set; }
            public string price { get; set; }
            public string quantity { get; set; }
            //public IDictionary<string, String> part_no { get; set; }
            //public IDictionary<string, String> descrip { get; set; }
            //public IDictionary<string, String> price { get; set; }
            //public IDictionary<string, String> quantity { get; set; }
        }
        public class BillTo
        {
            public string street { get; set; }
            public string city { get; set; }
            public string state { get; set; }

        }
    }
}




