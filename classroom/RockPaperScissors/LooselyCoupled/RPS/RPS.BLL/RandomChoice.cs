using System;

namespace RPS.BLL
{
    public class RandomChoice : IChoiceGetter
    {
        private readonly Random _rng = new Random();
        public Choice GetChoice()
        {
            return (Choice)_rng.Next(0, 5);
        }
    }
}