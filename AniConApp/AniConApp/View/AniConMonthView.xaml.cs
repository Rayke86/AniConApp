﻿using System;
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
        

        private List<Month> _year = new List<Month>();
        public List<Month> Year
        {
            get { return this._year; }
        }


        public AniConMonthView()
        {
            this.InitializeComponent();

            //Resources.Values.ToList();

            aniInfoView.setMonthView(this);
            
            /*
            //ObservableCollection<Month> Years2 = new ObservableCollection<Month>();
            Month August = new Month();
            August.Name = "August";
            Month June = new Month();
            June.Name = "June";
            AniconValues Abunai = new AniconValues("Abunai", "Veldhoven,Locht 117");
            August.Items.Add(Abunai);

            AniconValues con2 = new AniconValues("Con2", "Breda,Abdijstraat 1");
            August.Items.Add(con2);

            AniconValues TsunaCon = new AniconValues("TsunaCon", "Rotterdam,Rembrandtlaan 220");
            August.Items.Add(TsunaCon);
            Year.Add(August);

            AniconValues con3 = new AniconValues("Con3", "Prinsenbeek,Witte Baan 1");
            June.Items.Add(con3);
            Year.Add(June);
            //AniCons.DataContext = August;
            //AniHub.DataContext = Year;
            //AniHub2.DataContext = new CollectionViewSource { Source = Year };
            //this.DataContext = Year;
            //AniHub2.DataContext = Year;

            */


            //var aniCons = (CollectionViewSource)Resources["src"];


            //JsonObject aniConsYear = new JsonObject();
            //JsonArray Year = new JsonArray();
            //aniConsYear.Add("2015", Year);
            //JsonObject August = new JsonObject();
            //Year.Add(August);
            //JsonObject Years = new JsonObject();
            //JsonObject Years = new JsonObject();
            //JsonArray Months = new JsonArray();
            //JsonObject August = new JsonObject();
            ////Years.Add("August", August);
            //Months.Add(August);
            //JsonObject Abunai = new JsonObject();
            //Abunai.Add("Name", JsonValue.CreateStringValue("Abunai"));
            //Abunai.Add("Location", JsonValue.CreateStringValue("Koningshof Veldhoven"));
            //Abunai.Add("Month", JsonValue.CreateStringValue("August"));
            //August.Add("Abunai", Abunai);
            //Years.Add("Months", Months);
            
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
                            Debug.WriteLine(month.Name);
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
                            Debug.WriteLine(month.Name);
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
        {/*
            String s = "test";
            aniInfoView.name = ((AniconValues)e.ClickedItem).Name;
            aniInfoView.location = ((AniconValues)e.ClickedItem).Location;
            System.Diagnostics.Debug.Write(((AniconValues)e.ClickedItem).Name);
            //this.Frame.Navigate(typeof(AniConInfoView));
            */
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedIndex != -1)
            {
                var s = ((sender as ListView).DataContext as Month);
                
                int monthIndex = yearView.Items.IndexOf((sender as ListView).DataContext as Year);

                string p = monthView.Items[0].GetType().ToString();

                //(yearView.Items[0] as monthView).

                //aniInfoView.name = ((sender as ListView).SelectedItem as AniconValues).Name;
                // aniInfoView.location = Year[0].Items[(sender as ListView).SelectedIndex].Location;
                Window.Current.Content = aniInfoView;
                //this.Frame.Navigate(aniInfoView.GetType(),aniInfoView);

                var con = ((sender as ListView).DataContext as Month).Items[(sender as ListView).SelectedIndex];
                
                string location = con.location; 
                string name = con.name;    
                aniInfoView.setInformation(location, name);
            }
           // (sender as ListView).SelectedIndex = -1;
           
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
