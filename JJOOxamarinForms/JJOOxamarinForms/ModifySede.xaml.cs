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
            Debug.WriteLine(s.to_string);
            Debug.WriteLine("ENTRO MODIFY SEDE");
            sede = s;
            anoViejo = s.ano;
            SetProperties(s.id_tipo_jjoo);
            InitializeComponent();
        }

        private void SetProperties(int i)
        {
            Debug.WriteLine("ENTRO MODIFY PROPERTIES");
            String[] ep = { "INVIERNO", "VERANO" };
            Debug.WriteLine("ENTRO MODIFY PROPERTIESX");
            An.Text = sede.ano.ToString();
            Debug.WriteLine("ENTRO MODIFY PROPERTIES2");
            Ciudad.Text = sede.ciudad;
            Debug.WriteLine("ENTRO MODIFY PROPERTIES3");
            Epocas.ItemsSource = ep;
            Debug.WriteLine("ENTRO MODIFY PROPERTIES4");
            Epocas.SelectedIndex = (i - 1);
            Debug.WriteLine("ENTRO MODIFY PROPERTIES5");
        }

        private async void OnButtonCanceled(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void OnButtonModify(object sender, EventArgs e)
        {
            int ep = -1;

            if (Epocas.SelectedIndex >= 0)
                ep = Epocas.SelectedIndex + 1;

            int a = -1;
            if (int.TryParse(An.Text, out a))
            {
                if (a < 0 || a > 2500)
                {
                    await DisplayAlert("Datos incorrectos", "Introduzca un año entre 0 y 2050", null, "Entendido");
                    An.Text = "";
                }
                else
                {
                    if (An.Text == "" || ep < 0)
                    {
                        await DisplayAlert("Datos incorrectos", "Cubra todos los campos correctamente", null, "Entendido");
                    }
                    else
                    {
                        WebPetition wp = new WebPetition();

                        if (await wp.ModifySede(a, anoViejo, ep))
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
                An.Text = "";
            }
        }
    }
}