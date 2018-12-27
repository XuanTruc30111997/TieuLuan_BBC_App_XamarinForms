using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using bbc.Droid;
using bbc.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastDroid))]
namespace bbc.Droid
{
    public class ToastDroid : IMessage
    {
        public void LongToast(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortToast(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}