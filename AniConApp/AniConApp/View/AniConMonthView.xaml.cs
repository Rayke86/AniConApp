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
            //ObservableCollection<Month> Years2 = new ObservableCollection<Month>();
            Month August = new Month();
            August.Name = "August";
            Month June = new Month();
            June.Name = "June";
            AniconValues Abunai = new AniconValues("Abunai", "Koningshof Veldhoven");
            August.Items.Add(Abunai);

            AniconValues con2 = new AniconValues("Con2", "Ergens maar nergens");
            August.Items.Add(con2);

            AniconValues TsunaCon = new AniconValues("TsunaCon", "Rotterdam");
            August.Items.Add(TsunaCon);
            Year.Add(August);

            AniconValues con3 = new AniconValues("Con3", "test");
            June.Items.Add(con3);
            Year.Add(June);
            //AniCons.DataContext = August;
            AniHub.DataContext = Year;
            AniHub2.DataContext = new CollectionViewSource { Source = Year };
            this.DataContext = Year;
            //AniHub2.DataContext = Year;



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

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            String s = "test";
            aniInfoView.name = ((AniconValues)e.ClickedItem).Name;
            aniInfoView.location = ((AniconValues)e.ClickedItem).Location;
            System.Diagnostics.Debug.Write(((AniconValues)e.ClickedItem).Name);
            //this.Frame.Navigate(typeof(AniConInfoView));
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedIndex != -1)
            {
                String s = ((sender as ListView).DataContext as Month).Name;
                int index = monthView.Items.IndexOf((sender as ListView).DataContext as Month);
                string p = monthView.Items[0].GetType().ToString();
                //aniInfoView.name = ((sender as ListView).SelectedItem as AniconValues).Name;
                // aniInfoView.location = Year[0].Items[(sender as ListView).SelectedIndex].Location;
                Window.Current.Content = aniInfoView;
                //this.Frame.Navigate(aniInfoView.GetType(),aniInfoView);
                string location = _year[monthView.Items.IndexOf((sender as ListView).DataContext as Month)].Items[(sender as ListView).SelectedIndex].Location;
                string name = _year[monthView.Items.IndexOf((sender as ListView).DataContext as Month)].Items[(sender as ListView).SelectedIndex].Name;
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

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as ListView).SelectedIndex = -1;
        }
    }

    public class AniconValues
    {
        public AniconValues(String Name,String Location)
        {
            this.Name = Name;
            this.Location = Location;
        }

        public string Name { get; set; }
        public string Location { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }


    public class Month
    {
        public Month()
        {
            this.Items = new List<AniconValues>();
        }



        public string Name { get; set; }
        public List<AniconValues> Items { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
