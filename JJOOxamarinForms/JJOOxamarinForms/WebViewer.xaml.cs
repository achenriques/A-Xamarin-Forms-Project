using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JJOOxamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebViewer : ContentPage
    {
        public WebViewer()
        {
            InitializeComponent();

            WebV.VerticalOptions = LayoutOptions.FillAndExpand;
            WebV.Source = new UrlWebViewSource
            {
                Url = "http:/172.26.80.77/WebClientOlimpiada/front/html/index.html"
            };
        }
    }
}