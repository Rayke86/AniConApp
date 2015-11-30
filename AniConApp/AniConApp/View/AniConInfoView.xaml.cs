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
using System.Diagnostics;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Appointments;
using AniConApp.Model;
using Windows.UI.Core;

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
       // DataTransferManager dataTransferManager;
        Convention con;
        String[] months =  new String[] { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" };


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

            //dataTransferManager = DataTransferManager.GetForCurrentView();
            //dataTransferManager.DataRequested += DataTransferManager_DataRequested;


            

            //TODO remove button
            //button.Visibility = Visibility.Collapsed;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.con= e.Parameter as Convention;
            if (con != null)
            {
                this.location = con.location;
                // textBox.Text += location;
                this.name = con.name;
                this.textBox.Text = con.name + "\n" + con.location;
                //this.con = con;
                getDestination2();
            }
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;


            request.Data.SetText("test");
            request.Data.Properties.Title = name;
            request.Data.Properties.Description = location;

        }

        public async void getDestination()
        {
            Windows.Devices.Geolocation.Geopoint point = new Windows.Devices.Geolocation.Geopoint(new Windows.Devices.Geolocation.BasicGeoposition() { Latitude = 51.58914, Longitude = 4.74304 });

            MapLocationFinderResult result = await Windows.Services.Maps.MapLocationFinder.FindLocationsAsync("Niewegein,Blokhoeve 2", point);
            
            
        }

        public async void getDestination2()
        {
            RouteMap.Children.Clear();
            RouteMap.Routes.Clear();
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

                            

                            MapRouteFinderResult x = await MapRouteFinder.GetDrivingRouteAsync(currentLocation, Destination.Point);
                            Windows.UI.Xaml.Controls.Maps.MapRouteView route = new Windows.UI.Xaml.Controls.Maps.MapRouteView(x.Route);
                           
                            RouteMap.Routes.Add(route);


                            break;
                        }
                }
                    
            }
            catch (Exception ex)
            {

            }



            //Windows.Devices.Geolocation.Geolocator.RequestAccessAsync().Completed( 


            Canvas pin2 = new Canvas();
            Ellipse Ppin2 = new Ellipse() { Width = 20, Height = 20 };
            Ppin2.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            Ppin2.Margin = new Thickness(-12.5, -12.5, 0, 0);
            pin2.Children.Add(Ppin2);



            RouteMap.Children.Add(pin2);
            Windows.UI.Xaml.Controls.Maps.MapControl.SetLocation(pin2, result.Locations[0].Point);
            Windows.UI.Xaml.Controls.Maps.MapControl.SetNormalizedAnchorPoint(pin2, new Point(0.5, 0.5));
            RouteMap.ZoomLevel = 12;
            RouteMap.Center = result.Locations[0].Point;
            
        }



        public void setInformation(Convention con)
        {
            this.location = con.location;
            // textBox.Text += location;
            this.name = con.name;
            this.textBox.Text = con.name + "\n" + con.location;
            this.con = con;
            getDestination2();

        }

        public void setMonthView(AniConMonthView monthView)
        {
            this.monthView = monthView;
        }


        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);
        //    //  this.textBox.Text = this.location;
        //}

        private void button_Click(object sender, RoutedEventArgs e)
        {
           // Window.Current.Content = monthView;
        }
        

        public static Rect GetElementRect(Windows.UI.Xaml.FrameworkElement element)
        {
            Windows.UI.Xaml.Media.GeneralTransform transform = element.TransformToVisual(null);
            Point point = transform.TransformPoint(new Point());
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }

        private  void ShareButton_Click(object sender, RoutedEventArgs e)
        {
            // DataTransferManager.ShowShareUI();
            addApointment(sender);

        }

        private async void addApointment(object sender)
        {
            // String x = await AppointmentManager.ShowAddAppointmentAsync(new Appointment { Location = this.location, Details = this.name, StartTime = new DateTime(2015, 11, 23, 12, 00, 00, 00) },new Rect { Height = RouteMap.Height, Width = RouteMap.Width, X = 0, Y = 0 });

            //AppointmentStore x = await AppointmentManager.RequestStoreAsync(AppointmentStoreAccessType.AllCalendarsReadWrite);
            //x.ShowAddAppointmentAsync(new Appointment { Location = this.location, Details = this.name, StartTime = new DateTime(2015, 11, 23, 12, 00, 00, 00) }, new Rect { Height = RouteMap.Height, Width = RouteMap.Width, X = 0, Y = 0 });
            Appointment appointment = new Windows.ApplicationModel.Appointments.Appointment();
            appointment.Subject = this.name;
            appointment.Location = this.location;
            int month = Array.IndexOf(months, con.month);
            month++;
            appointment.StartTime = new DateTimeOffset(new DateTime(Convert.ToInt32(con.year), month, con.days[0]));

            //AppointmentStore p = await AppointmentManager.RequestStoreAsync(AppointmentStoreAccessType.AllCalendarsReadWrite);

            try {
                await Windows.ApplicationModel.Appointments.AppointmentManager.ShowEditNewAppointmentAsync(appointment);
            }
            catch(Exception ex)
            {

            }

         //   var rect = AniConInfoView.GetElementRect(sender as FrameworkElement);

            //String appointmentId = await AppointmentManager.ShowAddAppointmentAsync(appointment,rect,Windows.UI.Popups.Placement.Default);
            //if (appointmentId != String.Empty)
            //{
            //    //rootPage.NotifyUser("Appointment Id: " + appointmentId, NotifyType.StatusMessage);
            //    int x = 0;
            //}
            //else
            //{
            //    //rootPage.NotifyUser("Appointment not added.", NotifyType.ErrorMessage);
            //    int y = 0;
            //}
        }


    }
}
