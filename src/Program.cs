using System;
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
		InputManager	inputManager = new InputManager(_window);
	}
}
