using OvenTimer.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OvenTimer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OvenPage2 : ContentPage
    {
        MyTimer[] myTimers = new MyTimer[1];
        private List<Event> AllEvents { get; set; }

        public OvenPage2()
        {
            InitializeComponent();
            Setup();
        }

        private List<Event> GetEvents()
        {
            return new List<Event>()
            {
                new Event{ OvenNo=2,  EventTitle = "10단 쿠키오븐",  BgColor = "#8251AE", Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0, 0, 2, 0).Ticks)},
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
            var button = sender as Button;
            var evt = button.BindingContext as Event;

            AllEvents[0].Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0, 0, 2, 0).Ticks);
            myTimers[0].Start();
        }

        private void btnStop_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var evt = button.BindingContext as Event;

            myTimers[0].Stop();
        }

        private async void TimerSet_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var evt = button.BindingContext as Event;


            Debug.WriteLine(AllEvents[0].Hours);
            Debug.WriteLine(AllEvents[0].Minutes);
            Debug.WriteLine(AllEvents[0].Seconds);

            TimeSpan ts = new TimeSpan(Convert.ToInt32(AllEvents[0].Hours), Convert.ToInt32(AllEvents[0].Minutes), Convert.ToInt32(AllEvents[0].Seconds)); //1시간 5분
            TimerPage tsp = new TimerPage(ts);
            tsp.TimerSetting += Tsp_TimerSetting;

            await PopupNavigation.Instance.PushAsync(tsp, false);
        }


        private void Tsp_TimerSetting(TimeSpan timeSpan)
        {
            AllEvents[0].TimerLabel = timeSpan.ToString();
        }
    }
}