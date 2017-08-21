using System;

namespace JJOOxamarinForms.Model.model
{
    public class Olimpiada
    {
        public int id_pais { get; private set; }
        public String nombre_ciudad { get; private set; }
        public int id_ciudad { get; private set; }
        public String nombre_pais { get; private set; }
        public int valor { get; private set; }
        public int n_veces_sede { get; private set; }
        public String tipo { get; private set; }
        public String to_string { get; }

        public Olimpiada(int id_pais, String nombre_ciudad, int id_ciudad, String nombre_pais, int valor, int n_veces_sede, String tipo)
        {

            this.id_pais = id_pais;
            this.nombre_ciudad = nombre_ciudad;
            this.id_ciudad = id_ciudad;
            this.nombre_pais = nombre_pais;
            this.valor = valor;
            this.n_veces_sede = n_veces_sede;
            this.tipo = tipo;
            this.to_string = this.ToString();
        }

        public override String ToString()
        {
            String toret;

            if (n_veces_sede == 0)
            {
                toret = ("País: " + this.nombre_pais + ", con ID " + id_pais + ", en la ciudad de " + this.nombre_ciudad +
                        " \n con valor " + this.valor + ", nunca ha sido sede.");
            }
            else
            {
                toret = ("País: " + this.nombre_pais + ", con ID " + this.id_pais + ", en la ciudad de " + this.nombre_ciudad +
                    " \n con valor " + this.valor + ", ha sido sede " + this.n_veces_sede + " en " + this.tipo + ".");
            }
            return toret;
        }
    }
}
