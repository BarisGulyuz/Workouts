using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Workouts.XML
{
    public class XMLWorkout
    {
        #region XMLSTRING
        const string xmlString =
                            @"<?xml version=""1.0"" encoding=""utf-8""?>
                            <body>
                                <Customers>
                                <Client>
                                    <Firstname Value=""Baris"" />
                                    <LastName Value=""GULYUZ"" />
                                    <PhoneNumber Value=""123456"" />
                                    <Address Value=""Yalova/Sugören"" />
                                    <City Value=""Yalova"" />
                                    <State Value=""True"" />
                                </Client>
                                </Customers>
                            </body>";

        #endregion

        private XElement xmlDocument;
        public XMLWorkout()
        {
            LoadXML();
        }
        private void LoadXML()
        {
            xmlDocument = XElement.Parse(xmlString);
        }

        public bool IsNameExist(string name)
        {
            var value = xmlDocument.Elements("Customers")
            .Elements("Client")
            .Where(c => c.Elements("Firstname").Any(f => f.Attribute("Value").Value == name));

            return value.Any();
        }

    }
}
