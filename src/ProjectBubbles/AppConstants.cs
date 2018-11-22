using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Helpers;

namespace ProjectBubbles
{
    public static class AppConstants
    {
        public static readonly List<string> PagesNames = null;

        public static string Page01 = DateHelper.GetUNIVERSALString(DateTime.Now.AddDays(-2));
        public static string Page02 = DateHelper.GetUNIVERSALString(DateTime.Now.AddDays(-1));
        public static string Page03 = DateHelper.GetUNIVERSALString(DateTime.Now.AddDays(2));
        public static string Page04 = DateHelper.GetUNIVERSALString(DateTime.Now.AddDays(3));

        static AppConstants()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                case Device.Android:
                case Device.UWP:
                    PagesNames = new List<string>
                        {
                        DateHelper.GetUNIVERSALString(DateTime.Now),
                        DateHelper.GetUNIVERSALString(DateTime.Now.AddDays(1)),
                        DateHelper.GetUNIVERSALString(DateTime.Now.AddDays(2)),
                        DateHelper.GetUNIVERSALString(DateTime.Now.AddDays(3)),
                        DateHelper.GetUNIVERSALString(DateTime.Now.AddDays(4)),
                        DateHelper.GetUNIVERSALString(DateTime.Now.AddDays(5)),
                        DateHelper.GetUNIVERSALString(DateTime.Now.AddDays(6)),
                        DateHelper.GetUNIVERSALString(DateTime.Now.AddDays(7)),
                        };
                    break;
            }
        }
    }
}
