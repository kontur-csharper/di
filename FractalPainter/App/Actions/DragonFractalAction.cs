using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory painterFactory;
        private readonly Func<DragonSettingsGenerator> settingsGeneratorFactory;

        public DragonFractalAction(IDragonPainterFactory painterFactory,
            Func<DragonSettingsGenerator> settingsGeneratorFactory)
        {
            this.painterFactory = painterFactory;
            this.settingsGeneratorFactory = settingsGeneratorFactory;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var settings = settingsGeneratorFactory.Invoke().Generate();
            SettingsForm.For(settings).ShowDialog();
            painterFactory.CreatePainter(settings).Paint();
        }
    }
}