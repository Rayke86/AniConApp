using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AniConApp.Model
{
    class ConventionRepo
    {
        public List<Convention> conList;

        public ConventionRepo(String xmlFile)
        {
            conList = new List<Convention>();

            XmlReader reader = XmlReader.Create(xmlFile);

            int count = 0;
            while (reader.Read())
            {
                if(reader.NodeType == XmlNodeType.Element && reader.Name == "cons")
                {
                    Debug.WriteLine("Found List................");
                }
                else //if(reader.NodeType == XmlNodeType.Element && reader.Name == "con")
                {
                    switch(reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            Debug.WriteLine("Element : " + reader.Name);
                            Debug.WriteLine("");
                            break;
                        case XmlNodeType.Text:
                            Debug.WriteLine("Text : " + reader.Value);
                            Debug.WriteLine("---------------------");
                            break;
                    }
                    /*count++;
                    if(reader.Name == "name")
                    {

                        Debug.WriteLine("name of con = " + reader.Value);
                    }
                    Debug.WriteLine("found con.......nr of cons = " + count);*/
                }
            }
        }

    }
}
