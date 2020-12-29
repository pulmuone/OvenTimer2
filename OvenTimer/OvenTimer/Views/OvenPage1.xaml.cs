using OvenTimer.Models;
using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OvenTimer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class OvenPage1 : ContentPage
    {
        MyTimer[] myTimers = new MyTimer[1];
        private List<Event> AllEvents { get; set; }

        public OvenPage1()
        {
            InitializeComponent();

            Setup();
        }
        private List<Event> GetEvents()
        {
            return new List<Event>()
            {
                new Event{ OvenNo=1,  EventTitle = "두꺼운 쿠키오븐", BgColor = "#6787FF", Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0, 0, 1, 0).Ticks)},
            };
        }

        private void Setup()
        {
            AllEvents = GetEvents();
            eventList.ItemsSource = AllEvents;

            for (int n = 0; n < 1; n++)
                myTimers[n] = new MyTimer(new TimeSpan(0, 0, 1), AllEvents, AllEvents[n], eventList);
        }

        private void btnStart_Clicked(object sender, EventArgs e)
        {
            //var button = sender as Button;
            //var evt = button.BindingContext as Event;

            AllEvents[0].Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0, 0, 1, 0).Ticks);
            myTimers[0].Start();
        }

        private void btnStop_Clicked(object sender, EventArgs e)
        {
            //var button = sender as Button;
            //var evt = button.BindingContext as Event;

            myTimers[0].Stop();
        }

        private void btnReset_Clicked(object sender, EventArgs e)
        {

        }
    }

    public class Event
    {
        public int OvenNo { get; set; }
        public DateTime Date { get; set; }
        public string EventTitle { get; set; }
        public TimeSpan Timespan { get; set; }
        public string Days => Timespan.Days.ToString("00");
        public string Hours => Timespan.Hours.ToString("00");
        public string Minutes => Timespan.Minutes.ToString("00");
        public string Seconds => Timespan.Seconds.ToString("00");
        public string BgColor { get; set; }

        public string TimerLabel { get; set; }
    }
}
