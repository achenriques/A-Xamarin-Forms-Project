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
    public partial class Olimpiadas : ContentPage
    {
        private WebPetition wp;
        private List<Olimpiada> olimp;
        private String[] toShow;

        public Olimpiadas()
        {
            wp = new WebPetition();
            InitializeComponent();
            FillListView();
        }

        private async void FillListView()
        {
            olimp = await wp.GetOlimpiadas();

            toShow = new String[olimp.Count];

            for (int i = 0; i < olimp.Count; i++)
            {
                toShow[i] = olimp[i].ToString();
            }

            ListOlimpiadas.ItemsSource = toShow;
        }


    }
}