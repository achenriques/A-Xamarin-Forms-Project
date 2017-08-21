using System;

namespace JJOOxamarinForms.Model.model
{
    public class SedeJJOO
    {
        public int ano { get; private set; }
        public int id_tipo_jjoo { get; private set; }
        public int sede { get; private set; }
        public String ciudad { get; private set; }
        public String epoca { get; private set; }
        public String to_string { get; }

        public SedeJJOO(int ano, int sede, String ciudad, int id_tipo_jjoo, String epoca)
        {
            this.ano = ano;
            this.sede = sede;
            this.ciudad = ciudad;
            this.id_tipo_jjoo = id_tipo_jjoo;
            this.epoca = epoca;
            this.to_string = ToString();
        }

        public override String ToString()
        {
            String toret = ("Año: " + this.ano + ",en la ciudad de: " + this.ciudad + " hubo juegos de: " + this.epoca + "");
            return toret;
        }
    }
}
