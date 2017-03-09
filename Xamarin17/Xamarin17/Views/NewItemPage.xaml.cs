using System;
using Xamarin.Forms;
using Xamarin17.Models;

namespace Xamarin17.Views
{
    public partial class NewItemPage : ContentPage
    {
        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}