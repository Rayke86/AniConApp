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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AniConApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AniConInfoView : Page
    {

        static readonly AniConInfoView _instance = new AniConInfoView();

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

        public void setInformation(string location,string name)
        {
            this.location = location;
            // textBox.Text += location;
            this.textBox.Text = name + "/" + location;

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
