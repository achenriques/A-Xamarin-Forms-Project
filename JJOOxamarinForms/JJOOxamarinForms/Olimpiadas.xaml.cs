using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using JJOOxamarinForms.Services.RestConection;
using JJOOxamarinForms.Model.model;
using System.Diagnostics;

namespace JJOOxamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Olimpiadas : ContentPage
    {
        private WebPetition wp;
        private List<Olimpiada> olimp;

        public Olimpiadas()
        {
            wp = new WebPetition();
            InitializeComponent();
            FillListView();
        }

        private async void FillListView()
        {
            try
            {
                olimp = await wp.GetOlimpiadas();
            } catch (Exception e)
            {
                exceptionHandler();
            }

            Header.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            Header.VerticalOptions = LayoutOptions.Center;
            Header.HorizontalOptions = LayoutOptions.Center;
            ListOlimpiadas.VerticalOptions = LayoutOptions.FillAndExpand;
            ListOlimpiadas.HorizontalOptions = LayoutOptions.FillAndExpand;
            BtSedes.Clicked += OnButtonClicked;
            BtWeb.Clicked += OnButtonWebClicked;

            if (olimp != null)
                ListOlimpiadas.ItemsSource = olimp;
            else
            {
                ListOlimpiadas.ItemsSource = new List<Olimpiada>();
                Header.Text = "Ha ocurrido un error. No se ha podido cargar la lista de olimpiadas";
            }
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Sedes());
        }

        async void OnButtonWebClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WebViewer());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                olimp = await wp.GetOlimpiadas();
                ListOlimpiadas.ItemsSource = olimp;
                Header.Text = "";
            }
            catch (Exception e)
            {
                ListOlimpiadas.ItemsSource = new List<Olimpiada>();
                Header.Text = "Ha ocurrido un error. No se ha podido cargar la lista de olimpiadas";
            }

        }

        private void exceptionHandler ()
        {
            Header.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            Header.VerticalOptions = LayoutOptions.Center;
            Header.HorizontalOptions = LayoutOptions.Center;
            ListOlimpiadas.VerticalOptions = LayoutOptions.FillAndExpand;
            ListOlimpiadas.HorizontalOptions = LayoutOptions.FillAndExpand;
            BtSedes.Clicked += OnButtonClicked;
            BtWeb.Clicked += OnButtonWebClicked;
            ListOlimpiadas.ItemsSource = new List<Olimpiada>();
            Header.Text = "Ha ocurrido un error. No se ha podido cargar la lista de olimpiadas";
        }
    }
}