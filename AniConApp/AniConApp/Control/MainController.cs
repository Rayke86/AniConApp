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

        public MainController()
        {
            Debug.WriteLine("MainController started..........................");
            ConventionRepo repo = new ConventionRepo("./assets/cons.xml");
        }
    }
}
