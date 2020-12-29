using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OvenTimer.Views
{
    public delegate void TimerSettingEventHandler(TimeSpan timeSpan);

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimerPage : PopupPage
    {
        public event TimerSettingEventHandler TimerSetting;
        public TimerPage(TimeSpan timeSpan)
        {
            InitializeComponent();
            this.timer.Time = timeSpan;
        }

        private async void TimerButton_Clicked(object sender, EventArgs e)
        {
            TimerSetting?.Invoke(this.timer.Time);
            await PopupNavigation.Instance.PopAsync();
        }
    }
}