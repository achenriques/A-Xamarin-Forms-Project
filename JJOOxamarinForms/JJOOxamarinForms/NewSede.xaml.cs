using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using JJOOxamarinForms.Services.RestConection;
using System.Diagnostics;

namespace JJOOxamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewSede : ContentPage
    {
        List<List<String>> ciud;

        public NewSede()
        {
            ciud = null;

            SetProperties();
            InitializeComponent();
        }

        private async void SetProperties()
        {
            WebPetition wp = new WebPetition();

            ciud = await wp.GetCiudades();
            List<String> toShow = new List<String>();
            String[] ep = { "INVIERNO", "VERANO" };

            if (ciud != null)
            {
                for (int i = 0; i < ciud.Count; i++)
                {
                    toShow.Add(ciud[i][1]);
                }

                Ciudades.ItemsSource = toShow;
                Epocas.ItemsSource = ep;
            }
            else
            {
                HeaderNew.Text = "No se ha podido conectar con la base de datos";
                BtNuevo.IsVisible = false;
            }
        }

        private async void OnButtonCanceled(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void OnButtonNew(object sender, EventArgs e)
        {
            int c = -1;
            int ep = -1;

            if (Ciudades.SelectedIndex >= 0)
               c  = int.Parse(ciud[Ciudades.SelectedIndex][0]);
            if (Epocas.SelectedIndex >= 0)
                ep = Epocas.SelectedIndex + 1;

            int a = -1;
            if (int.TryParse(Ano.Text, out a))
            {
                if (a < 0 || a > 2500)
                {
                    await DisplayAlert("Datos incorrectos", "Introduzca un año entre 0 y 2050", null, "Entendido");
                    Ano.Text = "";
                } else
                {
                    if (c < 0 || ep < 0)
                    {
                        await DisplayAlert("Datos incorrectos", "Cubra todos los campos correctamente", null, "Entendido");
                    }
                    else
                    {
                        WebPetition wp = new WebPetition();

                        if (await wp.NewSede(a, c, ep))
                        {
                            await DisplayAlert("Nueva entrada", "Modificación correcta", null, "Ok");
                            await Navigation.PopModalAsync();
                        }
                        else
                        {
                            await DisplayAlert("Nueva entrada", "Error en la petición", null, "Cancel");
                        }
                    }
                }
            }
            else
            {
                await DisplayAlert("Año no admitido", "Introduzca un año entre 0 y 2050", null, "Entendido");
                Ano.Text = "";
            }
        }
    }
}