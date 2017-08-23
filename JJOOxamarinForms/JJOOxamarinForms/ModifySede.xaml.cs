using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using JJOOxamarinForms.Model.model;
using JJOOxamarinForms.Services.RestConection;
using System.Diagnostics;

namespace JJOOxamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModifySede : ContentPage
    {
        private SedeJJOO sede;
        private int anoViejo;

        public ModifySede(SedeJJOO s)
        {
            sede = s;
            anoViejo = s.ano;
            InitializeComponent();
            SetProperties();     
        }

        private void SetProperties()
        {
            String[] ep = { "INVIERNO", "VERANO" };
            try
            {
                Anyo.Text = sede.ano.ToString();
                Ciud.Text = sede.ciudad;
                Epoc.ItemsSource = ep;
                Epoc.SelectedIndex = (sede.id_tipo_jjoo - 1);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

        }

        private async void OnButtonCanceled(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void OnButtonModify(object sender, EventArgs e)
        {
            int ep = -1;

            if (Epoc.SelectedIndex >= 0)
                ep = Epoc.SelectedIndex + 1;

            int a = -1;
            if (int.TryParse(Anyo.Text, out a))
            {
                if (a < 0 || a > 2500)
                {
                    await DisplayAlert("Datos incorrectos", "Introduzca un año entre 0 y 2050", null, "Entendido");
                    Anyo.Text = "";
                }
                else
                {
                    if (Anyo.Text == "" || ep < 0)
                    {
                        await DisplayAlert("Datos incorrectos", "Cubra todos los campos correctamente", null, "Entendido");
                    }
                    else
                    {
                        WebPetition wp = new WebPetition();

                        if (await wp.ModifySede(anoViejo, a, ep))
                        {
                            await DisplayAlert("Nueva entrada", "Modificación correcta", null, "Ok");
                            await Navigation.PopModalAsync();
                        }
                        else
                        {
                            await DisplayAlert("Nueva entrada", "El año ya existe o ha habido un error", null, "Cancel");
                        }
                    }
                }
            }
            else
            {
                await DisplayAlert("Año no admitido", "Introduzca un año entre 0 y 2050", null, "Entendido");
                Anyo.Text = anoViejo.ToString();
            }
        }
    }
}