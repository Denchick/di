using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
	public class DragonFractalAction : IUiAction
	{
		private IImageHolder imageHolder;
		private IDragonPainterFactory dragonPainterFactory;
		private Func<Random, DragonSettingsGenerator> func;

		public string Category => "Фракталы";
		public string Name => "Дракон";
		public string Description => "Дракон Хартера-Хейтуэя";

		public DragonFractalAction(IDragonPainterFactory dragonPainterFactory, Func<Random, DragonSettingsGenerator> func)
		{
			this.dragonPainterFactory = dragonPainterFactory;
			this.func = func;
		}
		public void Perform()
		{
			var dragonSettings = CreateRandomSettings();
			// редактируем настройки:
			SettingsForm.For(dragonSettings).ShowDialog();
			// создаём painter с такими настройками
			dragonPainterFactory.CreateDragonPainter(dragonSettings).Paint();
		}

		private static DragonSettings CreateRandomSettings()
		{
			return new DragonSettingsGenerator(new Random()).Generate();
		}

	}
}

