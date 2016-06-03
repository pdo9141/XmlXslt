using System;
using System.Collections;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XPathAndXLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            var doc = XElement.Load("Accounts.xml");
            var query = from ele in doc.Elements("account")
                        select ele;

            foreach (var ele in query)
                Console.WriteLine(ele);

            Console.WriteLine("");
            Console.WriteLine(">>>>>>>>>>>>>>>>>> namespaces <<<<<<<<<<<<<<<<<<");

            var docNS = XElement.Load("AccountsNS.xml");
            query = from ele in docNS.Elements("{urn:accounting}account")
                    select ele;

            foreach (var ele in query)
                Console.WriteLine(ele);

            Console.WriteLine("");
            var sum = query.Sum(ele => int.Parse(ele.Value));
            Console.WriteLine("Sum is {0}", sum);

            Console.WriteLine("");
            Console.WriteLine(">>>>>>>>>>>>>>>>>> bad accounts <<<<<<<<<<<<<<<<<<");

            var badDoc = XElement.Load("AccountsBad.xml");

            const string accountNS = "urn:accounting";
            query = from ele in badDoc.Elements(XName.Get("account", accountNS))
                    let p = ele.Parent
                    where p.Name == XName.Get("accounts", accountNS)
                    && p.Parent == null
                    select ele;

            foreach (var ele in query)
                Console.WriteLine(ele);

            Console.WriteLine("");
            Console.WriteLine(">>>>>>>>>>>>>>>>>> bad accounts <<<<<<<<<<<<<<<<<<");

            string filePath = "Accounts.xml";
            string xPathQuery = "//accounts/account//@name";
            
            var testDoc = XDocument.Load(filePath);
            IEnumerable results = (IEnumerable)testDoc.XPathEvaluate(xPathQuery);
            string[] matchingValues = results.Cast<XAttribute>().Select(x => x.Value).ToArray();

            xPathQuery = "//accounts/account/.";
            results = (IEnumerable)testDoc.XPathEvaluate(xPathQuery);

            foreach (var result in results)
                Console.WriteLine(result);

            testDoc = XDocument.Load("Accounts.xml");
            xPathQuery = "/accounts/account/.";
            results = (IEnumerable)testDoc.XPathEvaluate(xPathQuery);

            /*
            XmlReader reader = XmlReader.Create("AccountsNS.xml");
            XmlNameTable nameTable = reader.NameTable;
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(nameTable);
            namespaceManager.AddNamespace(String.Empty, "urn:accounting");
            testDoc = XDocument.Load("AccountsNS.xml");
            xPathQuery = "//accounts/account/.";
            results = (IEnumerable)testDoc.XPathEvaluate(xPathQuery, namespaceManager);
            */

            Console.ReadLine();
        }
    }
}
