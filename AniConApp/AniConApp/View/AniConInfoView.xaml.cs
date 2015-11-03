using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Navigation;
using Windows.Services.Maps;
using Windows.Devices.Geolocation;
using System.Threading.Tasks;
using System.Threading;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AniConApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AniConInfoView : Page
    {

        static readonly AniConInfoView _instance = new AniConInfoView();
        MapLocation Destination;
        Geopoint currentLocation;
        

        public static AniConInfoView Instance
        {
            get
            {
                return _instance;
            }
        }

        
        

        public String name { get; set; }
        public String location { get; set; }
        public AniConMonthView monthView;

        public AniConInfoView()
        {
            this.InitializeComponent();
            location = "test";
            
            
            
        }

        public async void getDestination()
        {
            Windows.Devices.Geolocation.Geopoint point = new Windows.Devices.Geolocation.Geopoint(new Windows.Devices.Geolocation.BasicGeoposition() { Latitude = 51.58914, Longitude = 4.74304 });

            MapLocationFinderResult result = await Windows.Services.Maps.MapLocationFinder.FindLocationsAsync("Niewegein,Blokhoeve 2", point);
            
            
        }

        public async void getDestination2()
        {
            Windows.Devices.Geolocation.Geopoint point = new Windows.Devices.Geolocation.Geopoint(new Windows.Devices.Geolocation.BasicGeoposition() { Latitude = 51.58914, Longitude = 4.74304 });

            MapLocationFinderResult result = await Windows.Services.Maps.MapLocationFinder.FindLocationsAsync(this.location, point);
            Destination = result.Locations[0];

            Canvas pin = new Canvas();
            Ellipse Ppin = new Ellipse() { Width = 25, Height = 25 };
            Ppin.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            Ppin.Margin = new Thickness(-12.5, -12.5, 0, 0);
            pin.Children.Add(Ppin);

            try
            {
                // Windows.Devices.Geolocation.Geolocator.RequestAccessAsync().Completed()
                var accesStatus = await Geolocator.RequestAccessAsync();

                switch(accesStatus)
                {
                    case GeolocationAccessStatus.Allowed:
                        {


                            Geolocator geolocator = new Geolocator { DesiredAccuracy = 0 };

                            Geoposition pos = await geolocator.GetGeopositionAsync();
                            RouteMap.Children.Add(pin);
                            Windows.UI.Xaml.Controls.Maps.MapControl.SetLocation(pin, new Geopoint(new BasicGeoposition() { Latitude = pos.Coordinate.Latitude, Longitude = pos.Coordinate.Longitude }));
                            Windows.UI.Xaml.Controls.Maps.MapControl.SetNormalizedAnchorPoint(pin, new Point(0.5, 0.5));

                            currentLocation =  new Geopoint(new BasicGeoposition() { Latitude = pos.Coordinate.Latitude, Longitude = pos.Coordinate.Longitude });
                            break;
                        }
                }
                    
            }
            catch (Exception ex)
            {

            }



            //Windows.Devices.Geolocation.Geolocator.RequestAccessAsync().Completed( 



            MapRouteFinderResult x = await MapRouteFinder.GetDrivingRouteAsync(currentLocation, Destination.Point);
            Windows.UI.Xaml.Controls.Maps.MapRouteView route = new Windows.UI.Xaml.Controls.Maps.MapRouteView(x.Route);
            RouteMap.Routes.Add(route);

            RouteMap.Children.Add(pin);
            Windows.UI.Xaml.Controls.Maps.MapControl.SetLocation(pin, result.Locations[0].Point);
            Windows.UI.Xaml.Controls.Maps.MapControl.SetNormalizedAnchorPoint(pin, new Point(0.5, 0.5));
            RouteMap.ZoomLevel = 12;
            RouteMap.Center = result.Locations[0].Point;
            
        }



        public void setInformation(string location,string name)
        {
            this.location = location;
            // textBox.Text += location;
            this.textBox.Text = name + "/" + location;
            getDestination2();

        }

        public void setMonthView(AniConMonthView monthView)
        {
            this.monthView = monthView;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.textBox.Text = this.location;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Window.Current.Content = monthView;
        }
    }
}
