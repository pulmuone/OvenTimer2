using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OvenTimer.Behaviors
{
    public class NumericEntryBehavior : Behavior<Entry>
    {
        //텍스트가 추가되는 이벤트
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += TextChanged_Handler; //이벤트 핸들러 추가
        }

        private void TextChanged_Handler(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue)) //입력문자가 Null이나 빈칸이면
            {
                //((Entry)sender).Text = 0.ToString();
                return;
            }

            double _;
            if (!double.TryParse(e.NewTextValue, out _)) //숫자가 아니면
                ((Entry)sender).Text = e.OldTextValue; //원래대로 되돌린다.
        }
        //텍스트 추가가 종료되면
        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= TextChanged_Handler;//이벤트 핸들러 제거
        }
    }
}
