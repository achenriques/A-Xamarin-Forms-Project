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
    public partial class Sedes : ContentPage
    {
        private WebPetition wp;
        private List<SedeJJOO> sed;

        public Sedes()
        {
            wp = new WebPetition();
            InitializeComponent();
            FillListView();
        }

        private async void FillListView()
        {
            try
            {
                sed = await wp.GetSedes();
            } catch (Exception e)
            {
                exceptionHandler();
            }

            HeaderSedes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            HeaderSedes.VerticalOptions = LayoutOptions.Center;
            HeaderSedes.HorizontalOptions = LayoutOptions.Center;
            ListSedes.VerticalOptions = LayoutOptions.FillAndExpand;
            ListSedes.HorizontalOptions = LayoutOptions.FillAndExpand;
            ListSedes.RowHeight = 40;
            BtNew.Clicked += OnButtonNewClicked;

            if (sed != null)
            {
                ListSedes.ItemsSource = sed;
            }
            else
            {
                HeaderSedes.Text = "Ha ocurrido un error. No se ha podido cargar la lista de sedes";
                ListSedes.ItemsSource = new List<SedeJJOO>();
                BtNew.IsVisible = false;
            }
        }

        async void OnButtonNewClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync (new NewSede());
        }

        async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender).CommandParameter;

            bool ansawer = await DisplayAlert("¡Cuidado!", "¿ Estas seguro de eliminar: " + (SedeJJOO)mi + " ?.", "Yes", "No");
            if (ansawer)
            {
                if (await wp.DeleteSede(((SedeJJOO) mi).ano))
                {
                    sed = await wp.GetSedes();
                    ListSedes.ItemsSource = sed;
                }
            }
        }

        async void OnModify(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender).CommandParameter;

            await Navigation.PushModalAsync(new ModifySede(((SedeJJOO)mi)));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                sed = await wp.GetSedes();
                ListSedes.ItemsSource = sed;
                HeaderSedes.Text = "";
            } catch (Exception e)
            {
                HeaderSedes.Text = "Ha ocurrido un error. No se ha podido cargar la lista de sedes";
                ListSedes.ItemsSource = new List<SedeJJOO>();
                BtNew.IsVisible = false;
            }

        }

        private void exceptionHandler ()
        {
            HeaderSedes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            HeaderSedes.VerticalOptions = LayoutOptions.Center;
            HeaderSedes.HorizontalOptions = LayoutOptions.Center;
            ListSedes.VerticalOptions = LayoutOptions.FillAndExpand;
            ListSedes.HorizontalOptions = LayoutOptions.FillAndExpand;
            ListSedes.RowHeight = 40;
            HeaderSedes.Text = "Ha ocurrido un error. No se ha podido cargar la lista de sedes";
            ListSedes.ItemsSource = new List<SedeJJOO>();
            BtNew.IsVisible = false;
        }

    }
}