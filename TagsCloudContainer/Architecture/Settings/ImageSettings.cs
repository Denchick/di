using TagsCloudContainer.Architecture.Themes;

namespace TagsCloudContainer.Architecture
{
	public class ImageSettings
	{
		public int Width { get; }
		public int Height { get; }
		public string Filename { get; }
		public bool Gui { get; }
		public ITheme Theme { get; }
		
		public ImageSettings(string imageFilename, int height, int width, ITheme theme, bool gui)
		{
			Filename = imageFilename;
			Height = height;
			Width = width;
			Theme = theme;
			Gui = gui;
		}
	}
}