﻿using Xamarin.Forms;

// https://github.com/xamarinhq/app-evolve 
namespace SmartHotel220.Clients.Core.Controls
{
    public class ParallaxControl : ScrollView
    {
        public ParallaxControl()
        {
            Scrolled += (sender, e) => Parallax();
        }

        public static readonly BindableProperty ParallaxViewProperty =
            BindableProperty.Create(nameof(ParallaxControl), typeof(View), typeof(ParallaxControl), null);

        public View ParallaxView
        {
            get => (View)GetValue(ParallaxViewProperty);
            set => SetValue(ParallaxViewProperty, value);
        }

        private double _height;
        public void Parallax()
        {
            if (ParallaxView == null 
                || Device.RuntimePlatform == "Windows" 
                || Device.RuntimePlatform == "WinPhone")
                return;

            if (_height <= 0)
                _height = ParallaxView.Height;

            var y = -(int)((float)ScrollY / 2.5f);

            if (y < 0)
            {
                ParallaxView.Scale = 1;
                ParallaxView.TranslationY = y;
            }
            else if (Device.RuntimePlatform == "iOS")
            {
                var newHeight = _height + (ScrollY * -1);
                ParallaxView.Scale = newHeight / _height;
                ParallaxView.TranslationY = -(ScrollY / 2);
            }
            else
            {
                ParallaxView.Scale = 1;
                ParallaxView.TranslationY = 0;
            }
        }
    }
}