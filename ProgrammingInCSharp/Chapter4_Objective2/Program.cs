namespace Chapter4_Objective2
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Transactions;
    using System.Xml;

    public class Program
    {
        public static void Main(string[] args)
        {
        }

        private static void UseXPathQUery()
        {
            var reader = new StreamReader(@"../../people.xml");
            var xml = reader.ReadToEnd();

            var doc = new XmlDocument();
            doc.LoadXml(xml);

            var navigator = doc.CreateNavigator();
            var xquery = "//people/person[@firstname='Jane']";
            var iterator = navigator.Select(xquery);

            while (iterator.MoveNext())
            {
                var firstname = iterator.Current.GetAttribute("firstname", string.Empty);
                var lastname = iterator.Current.GetAttribute("lastname", string.Empty);
                Console.WriteLine($"Name: {firstname} {lastname}");
            }
        }

        private static void UseXmlDocument()
        {
            var reader = new StreamReader(@"../../people.xml");
            var xml = reader.ReadToEnd();

            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var nodes = doc.GetElementsByTagName("person");

            foreach (XmlNode node in nodes)
            {
                var firstname = node.Attributes["firstname"].Value;
                var lastname = node.Attributes["lastname"].Value;
                Console.WriteLine($"Name: {firstname} {lastname}");
            }

            var newNode = doc.CreateNode(XmlNodeType.Element, "person", string.Empty);

            var firstnameAttribute = doc.CreateAttribute("firstname");
            firstnameAttribute.Value = "Shmuley";

            var lastnameAttribute = doc.CreateAttribute("lastname");
            lastnameAttribute.Value = "Boteach";

            newNode.Attributes.Append(firstnameAttribute);
            newNode.Attributes.Append(lastnameAttribute);

            doc.DocumentElement.AppendChild(newNode);
            doc.Save(Console.Out);
        }

        private static void UseXmlWriter()
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("people");
                writer.WriteStartElement("person");
                writer.WriteAttributeString("firstname", "Shmuley");
                writer.WriteAttributeString("lastname", "Boteach");
                writer.WriteStartElement("contactDetails");
                writer.WriteElementString("email", "shmuley@shmuleyboteach.com");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Flush();
            }

            Console.WriteLine(stream.ToString());
        }

        private static void UseXmlReader()
        {
            using (StreamReader streamReader = new StreamReader(@"../../people.xml"))
            {
                using (XmlReader xmlReader = XmlReader.Create(streamReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement("people");

                    var firstName = xmlReader.GetAttribute("firstname");
                    var lastName = xmlReader.GetAttribute("lastname");

                    Console.WriteLine($"Person: {firstName} {lastName}");

                    xmlReader.ReadStartElement("person");

                    Console.WriteLine("\tContact Details:");

                    xmlReader.ReadStartElement("contactdetails");
                    var emailAddress = xmlReader.ReadElementContentAsString();

                    Console.WriteLine($"\t\tEmail address: {emailAddress}");
                }
            }
        }

        private static async void UseTransaction()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var command1 = new SqlCommand("INSERT INTO customers([FirstName], [LastName]) VALUES('Shmuley', 'Boteach'", connection);
                    var command2 = new SqlCommand("INSERT INTO customers([FirstName], [LastName]) VALUES('Dildo', 'Schwaggins'", connection);

                    await command1.ExecuteNonQueryAsync();
                    await command2.ExecuteNonQueryAsync();
                }

                transaction.Complete();
            }
        }

        private static async Task InsertRowWithParameterizedQuery()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("INSERT INTO customers([FirstName], [LastName]) VALUES(@firstName, @lastName", connection);
                await connection.OpenAsync();

                command.Parameters.AddWithValue("@firstName", "Shmuley");
                command.Parameters.AddWithValue("@lastName", "Boteach");

                var numberOfInsertedRows = await command.ExecuteNonQueryAsync();
                Console.WriteLine($"{numberOfInsertedRows} rows inserted");
            }
        }

        private static async Task UpdateRows()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("UPDATE Customers SET FirstName='Shmuley'", connection);

                await connection.OpenAsync();
                var numberOfRowsUpdated = command.ExecuteNonQueryAsync();
                Console.WriteLine($"{numberOfRowsUpdated} rows updated");
            }
        }

        private static async Task SelectDataFromTable()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT * FROM customers", connection);
                await connection.OpenAsync();

                var dataReader = await command.ExecuteReaderAsync();

                while (await dataReader.ReadAsync())
                {
                    dataReader.GetString(1);
                }

                dataReader.Close();
            }
        }

        private static void GetConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            Console.WriteLine($"Opening connection for {connectionString}");
            Thread.Sleep(2000);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.OpenAsync();
                Console.WriteLine("Connection opened");

                Console.WriteLine("Executing transaction...");
                Thread.Sleep(4000);

                Console.WriteLine("Transaction complete");
                Console.WriteLine("Closing connection");
            }
        }
    }
}
