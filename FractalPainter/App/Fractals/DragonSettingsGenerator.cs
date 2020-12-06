using System;

namespace FractalPainting.App.Fractals
{
    public class DragonSettingsGenerator
    {
        private readonly Random random;

        public DragonSettingsGenerator(Random random)
        {
            this.random = random;
        }

        public DragonSettings Generate()
        {
            return new DragonSettings
            {
                Angle1 = random.NextDouble() / 2 + 0.5,
                Angle2 = 3 * (random.NextDouble() / 2 + 0.5),
                ShiftX = 1,
                ShiftY = 0,
                Scale = (float) (1 / Math.Sqrt(2) + (random.NextDouble() - 0.5) / 10)
            };
        }
    }
}