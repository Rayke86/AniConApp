using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AniConApp.Model
{
    class ConventionRepo
    {
        public List<Convention> conList { get; set; }

        public ConventionRepo(String xmlFile)
        {
            conList = new List<Convention>();
            this.conList = this.getConList(xmlFile);
        }

        private List<Convention> getConList(String xmlFile)
        {
            XDocument doc = XDocument.Load(xmlFile);

            foreach (XElement element in doc.Descendants("cons").Nodes())
            {
                String name = element.Element("name").Value;
                String month = element.Element("month").Value;
                String year = element.Element("year").Value;
                String location = element.Element("location").Value;
                String days = element.Element("days").Value;

                Convention con = new Convention(name, month,year,location,days);

                conList.Add(con);              
            }

            return conList;
        }

    }
}
