using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;

class	Program
{
	private static IWindow			_window;
	private static IInputContext	_input;

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
		_input = _window.CreateInput();
		for (int i = 0; i < _input.Keyboards.Count; i++)
			_input.Keyboards[i].KeyDown += KeyDown;
	}

	private static void	KeyDown(IKeyboard keyboard, Key key, int keyCode)
	{
		if (key == Key.Escape)
			_window.Close();
	}
}
