using OvenTimer.Models;
using OvenTimer.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OvenTimer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            var model = button.BindingContext as Oven;

            if (model.OvenNo == 1)
            {
                //중복 클릭 방지
                foreach (var item in Navigation.NavigationStack)
                {
                    if (item.ToString().EndsWith("OvenPage1"))
                        return;
                }

                await Navigation.PushAsync(new OvenPage1());
            }
            else if (model.OvenNo == 2)
            {
                //중복 클릭 방지
                foreach (var item in Navigation.NavigationStack)
                {
                    if (item.ToString().EndsWith("OvenPage2"))
                        return;
                }

                await Navigation.PushAsync(new OvenPage2());
            }
            else if (model.OvenNo == 3)
            {
                //중복 클릭 방지
                foreach (var item in Navigation.NavigationStack)
                {
                    if (item.ToString().EndsWith("OvenPage3"))
                        return;
                }

                await Navigation.PushAsync(new OvenPage3());
            }
            else if (model.OvenNo == 4)
            {
                //중복 클릭 방지
                foreach (var item in Navigation.NavigationStack)
                {
                    if (item.ToString().EndsWith("OvenPage4"))
                        return;
                }

                await Navigation.PushAsync(new OvenPage4());
            }
        }
    }
}
