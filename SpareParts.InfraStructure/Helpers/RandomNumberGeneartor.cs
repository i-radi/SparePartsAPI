using System;

namespace SpareParts.InfraStructure
{
    public static class RandomNumberGeneartor
    {
        // Instantiate random number generator.  
        private static readonly Random Random = new Random();

        // Generates a random number within a range.      
        public static int Generate(int min, int max)
        {
            return Random.Next(min, max);
        }
    }
}