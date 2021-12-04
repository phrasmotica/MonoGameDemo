namespace MonoGameDemo.Data
{
    public class Attack
    {
        public Attack(string name, int power)
        {
            Name = name;
            Power = power;
        }

        public string Name { get; }
        public int Power { get; }
    }
}
