using System;

namespace MonoGameDemo.Data
{
    public class Health
    {
        public Health(int max)
        {
            Max = max;
            Current = max;
        }

        public int Max { get; }
        public int Current { get; set; }

        public int Damage(int amount)
        {
            var actualAmount = Math.Min(amount, Current);
            Current -= actualAmount;
            return actualAmount;
        }
    }
}
