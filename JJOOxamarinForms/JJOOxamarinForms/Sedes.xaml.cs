using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using JJOOxamarinForms.Services.RestConection;
using JJOOxamarinForms.Model.model;

namespace JJOOxamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sedes : ContentPage
    {
        private WebPetition wp;
        private List<SedeJJOO> sed;
        public event PropertyChangedEventHandler PropertyChanged;

        public Sedes()
        {
            wp = new WebPetition();
            InitializeComponent();
            FillListView();
        }

        private async void FillListView()
        {
            sed = await wp.GetSedes();

            HeaderSedes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            HeaderSedes.VerticalOptions = LayoutOptions.Center;
            HeaderSedes.HorizontalOptions = LayoutOptions.Center;
            ListSedes.VerticalOptions = LayoutOptions.FillAndExpand;
            ListSedes.HorizontalOptions = LayoutOptions.FillAndExpand;
            ListSedes.RowHeight = 40;
            //Todo ListSelect
            ListSedes.ItemSelected += async (sender, e) => {
                bool ansawer = await DisplayAlert("¡Cuidado!", "¿ Estas seguro de eliminar: " + e.SelectedItem + " ?.", "Yes", "No");
                if (ansawer)
                {
                    if(await wp.DeleteSede("2"))
                    {
                        sed = await wp.GetSedes();
                        ListSedes.ItemsSource = sed;
                    }
                }

            };
            BtNew.Clicked += OnButtonClicked;

            if (sed.Count != 0)
            {
                ListSedes.ItemsSource = sed;
            }
            else
                HeaderSedes.Text = "Ha ocurrido un error. No se ha podido cargar la lista de olimpiadas";
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Sedes());
        }

        /*async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }*/
    }
}