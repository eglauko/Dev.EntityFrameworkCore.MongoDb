namespace Dev.Temp.Model.Empresas
{
    public class Empresa
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Apelido { get; set; }

        public override string ToString()
        {
            return $"Empresa [{Id}] {Nome}, {Apelido}";
        }
    }
}
