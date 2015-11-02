using AniConApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniConApp.Control
{
    class MainController
    {
        public List<Convention> conList;
        public ConventionRepo repo;

        public MainController()
        {
            Debug.WriteLine("MainController started..........................");
            repo = new ConventionRepo("./assets/cons.xml");
            conList = repo.conList;
        }

        public List<Convention> getConventions()
        {
            return conList;
        }
    }
}
