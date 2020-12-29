using OvenTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OvenTimer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OvenPage3 : ContentPage
    {
        MyTimer[] myTimers = new MyTimer[3];
        private List<Event> AllEvents { get; set; }

        public OvenPage3()
        {
            InitializeComponent();
            Setup();
        }
        private List<Event> GetEvents()
        {
            return new List<Event>()
            {
                new Event{ OvenNo=3,  EventTitle = "경주빵 오븐-상", BgColor = "#EB9999", Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0, 0, 1, 0).Ticks)},
                new Event{ OvenNo=4,  EventTitle = "경주빵 오븐-중",  BgColor = "#96338F", Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0, 0, 2, 0).Ticks)},
                new Event{ OvenNo=5,  EventTitle = "경주빵 오븐-하",  BgColor = "#8251AE", Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0, 0, 3, 0).Ticks)},
            };
        }

        private void Setup()
        {
            AllEvents = GetEvents();
            eventList.ItemsSource = AllEvents;
            for (int n = 0; n < 3; n++)
                myTimers[n] = new MyTimer(new TimeSpan(0, 0, 1), AllEvents, AllEvents[n], eventList);
            
            //Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            //{
            //    foreach (var evt in AllEvents)
            //    {
            //        var timespan = evt.Date - DateTime.Now;
            //        evt.Timespan = timespan;
            //    }
            //    eventList.ItemsSource = null;
            //    eventList.ItemsSource = AllEvents;
            //    return true;
            //});
        }

        private void btnStart_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var evt = button.BindingContext as Event;
            //TimerTime.Value

            switch (evt.OvenNo - 3)
            {
                case 0:
                    AllEvents[0].Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0, 0, 1, 0).Ticks);
                    break;
                case 1:
                    AllEvents[1].Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0, 0, 2, 0).Ticks);
                    break;
                case 2:
                    AllEvents[2].Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0, 0, 3, 0).Ticks);
                    break;
            }
            myTimers[evt.OvenNo-3].Start();
        }

        private void btnStop_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var evt = button.BindingContext as Event;

            myTimers[evt.OvenNo - 3].Stop();
        }

        private void btnReset_Clicked(object sender, EventArgs e)
        {
            //TimePickerDialog timePickerDialog = new TimePickerDialog(DateTimePickerDialogActivity.this, android.R.style.Theme_Holo_Light_Dialog, onTimeSetListener, hour, minute, is24Hour);
        }
    }
}
