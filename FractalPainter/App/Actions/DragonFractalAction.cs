using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory dragonPainterFactory;
        private readonly Func<DragonSettingsGenerator> dragonSettingsGenerator;

        public DragonFractalAction(IDragonPainterFactory dragonPainterFactory, Func<DragonSettingsGenerator> dragonSettingsGenerator)
        {
            this.dragonPainterFactory = dragonPainterFactory;
            this.dragonSettingsGenerator = dragonSettingsGenerator;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";
        public int Order => 20;

        public void Perform()
        {
            var dragonSettings = dragonSettingsGenerator().Generate();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            // создаём painter с такими настройками
            dragonPainterFactory.CreateDragonPainter(dragonSettings).Paint();
        }
    }
}