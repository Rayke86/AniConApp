using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Data.Json;
using AniConApp.Model;
using System.Diagnostics;
using Windows.UI;
using Windows.UI.Core;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AniConApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AniConMonthView : Page
    {
        //public List<Month> Year = new List<Month>();
        public AniConInfoView aniInfoView = AniConInfoView.Instance;
        public ListView monthView = new ListView();
        public ListView yearView = new ListView();

        


        private List<Convention> conList;
        private List<Year> years;
        private List<Month> monthList = new List<Month>();


        public AniConMonthView()
        {
            this.InitializeComponent();

            //Resources.Values.ToList();
           
            aniInfoView.setMonthView(this);           
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.conList = e.Parameter as List<Convention>;
            if (conList != null)
            {
                years = getCons(conList);
                AniHub2.DataContext = years;
            }
        }      

        
        private List<Year> getCons(List<Convention> completeConList)
        {
            int index;
            Month month;
            List<Month> months2015 = new List<Month>();
            List<Month> months2016 = new List<Month>();
            years = new List<Year>();
            
            foreach (Convention con in completeConList)
            {
                switch(con.year)
                {
                    case "2015":
                        month = new Month();
                        month.Name = con.month;
                        index = months2015.FindIndex(item => item.Name == month.Name);
                        if(index < 0)
                        {
                            month.Items = setConventions(month, completeConList);
                            months2015.Add(month);
                        }
                         
                        break;

                    case "2016":
                        month = new Month();
                        month.Name = con.month;
                        index = months2016.FindIndex(item => item.Name == month.Name);
                        if (index < 0)
                        {
                            month.Items = setConventions(month, completeConList);
                            months2016.Add(month);
                        }
                        break;
                }
            }
            Year year2015 = new Year();
            year2015.Name = "2015";
            year2015.months = months2015;

            Year year2016 = new Year();
            year2016.Name = "2016";
            year2016.months = months2016;

            years.Add(year2015);
            years.Add(year2016);

            return years;
        }

        private List<Convention> setConventions(Month month, List<Convention> completeConList)
        {
            List<Convention> cons = new List<Convention>();
            
            foreach(Convention con in completeConList)
            {
                if(con.month.Equals(month.Name))
                {
                    cons.Add(con);
                }
            }

            return cons;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedIndex != -1)
            {                
                Window.Current.Content = aniInfoView;
                 
                //get selected convention               
                var con = ((sender as ListView).DataContext as Month).Items[(sender as ListView).SelectedIndex];
                
                string location = con.location; 
                string name = con.name;  
                
                aniInfoView.setInformation(location, name);                
            }
           
           
        }

        private void MonthView_Loaded(object sender, RoutedEventArgs e)
        {
            monthView = sender as ListView;
        }

        private void MonthView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            monthView = sender as ListView;
        }

        private void YearView_Loaded(object sender, RoutedEventArgs e)
        {
            yearView = sender as ListView;
        }

        private void YearView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            yearView = sender as ListView;
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as ListView).SelectedIndex = -1;
        }
    }

    //class that wil contain the months for that year
    public class Year
    {
        public Year()
        {
            this.months = new List<Month>();
        }

        public string Name { get; set; }
        public List<Month> months { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    //class that wil contain the conventions for that month
    public class Month
    {
        public Month()
        {
            this.Items = new List<Convention>();
        }

        public string Name { get; set; }
        public List<Convention> Items { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
