using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms;
using OvenTimer.Views;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Reflection;


namespace OvenTimer.Models
{
    public class MyTimer
    {
        private readonly TimeSpan timespan;
        private readonly Action callback;
        private List<Event> _allEvents;
        private Event _evt;
        private CollectionView _cv;


        static ISimpleAudioPlayer player;

        private CancellationTokenSource cancellation;
        static private CancellationTokenSource cancelSound;
        System.IO.Stream stream;

        public MyTimer(TimeSpan timespan, List<Event> allEvents, Event evt, CollectionView cv)
        {
            this.timespan = timespan;
            this._allEvents = allEvents;
            this._evt = evt;
            this._cv = cv;
            this.cancellation = new CancellationTokenSource();
            cancelSound = new CancellationTokenSource();

            stream = GetStreamFromFile("beep07.mp3");
            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            player.Load(stream);
        }

        public void Start()
        {
            CancellationTokenSource cts = this.cancellation; // safe copy
            Device.StartTimer(this.timespan,
                () => {
                    if (cts.IsCancellationRequested) 
                        return false;
                    //this.callback.Invoke();
                    var timespan = _evt.Date - DateTime.Now;
                    _evt.Timespan = timespan.TotalSeconds > 0 ? timespan : new TimeSpan(0, 0, 0);

                    _cv.ItemsSource = null;
                    _cv.ItemsSource = _allEvents;

                    //Debug.WriteLine(string.Format("{0}-{1}", _evt.EventTitle, _evt.Timespan.ToString()));
                    if (_evt.Timespan.TotalSeconds <= 0)
                        Task.Factory.StartNew(new Action<object>(PlayFinishedSound), _evt.OvenNo.ToString());

                    return _evt.Timespan.TotalSeconds > 0 ? true : false; 
                    // or true for periodic behavior
            });
        }

        static void PlayFinishedSound(object data)
        {

            CancellationTokenSource cts = cancelSound; // safe copy
            do
            {
                player.Play();

                Thread.Sleep(500);

            } while (!cts.IsCancellationRequested);
        }

        static void Run()
        {
            player.Play();
        }

        public void Stop()
        {
            Interlocked.Exchange(ref this.cancellation, new CancellationTokenSource()).Cancel();
            Interlocked.Exchange(ref cancelSound, new CancellationTokenSource()).Cancel();
        }

        System.IO.Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;

            var stream = assembly.GetManifestResourceStream("OvenTimer." + filename);

            return stream;
        }
    }
}
