using System;
using Silk.NET.Core;
using Silk.NET.Maths;
using Silk.NET.Windowing;

class	Program
{
	private static IWindow		_window;

	private static void	Main()
	{
		// Window Configuration
		WindowOptions	options = WindowOptions.Default with
		{
			Size = new Vector2D<int>(800, 600),
			Title = "Silk Game",
			API = new GraphicsAPI(ContextAPI.OpenGL, ContextProfile.Core, ContextFlags.Debug, new APIVersion(4, 6)),
		};

		// Window Creation
		_window = Window.Create(options);

		_window.Load += OnLoad;

		_window.Run();
	}

	private static void	OnLoad()
	{
		_ = new InputManager(_window);

		Tile[,] map = Dungeon.GenerateMap();

		for (int y = 0; y < 40; y++)
		{
			for (int x = 0; x < 40; x++)
			{
				switch (map[y, x])
				{
					case Tile.Empty:
						Console.Write('.');
						break ;
					case Tile.Floor:
						Console.Write(' ');
						break ;
					case Tile.Wall:
						Console.Write('#');
						break ;
					case Tile.Door:
						Console.Write('D');
						break ;
					default:
						Console.Write('?');
						break ;
				}
				Console.Write(" ");
			}
			Console.WriteLine();
		}
	}
}
