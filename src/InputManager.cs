using Silk.NET.Windowing;
using Silk.NET.Input;
using System.Numerics;

public class	InputManager
{
	private readonly IWindow		_window;
	private readonly IInputContext	_input;
	private readonly IMouse			_mouse;

	public	InputManager(IWindow window)
	{
		_window = window;
		_input = _window.CreateInput();

		// Keyboard
		for (int i = 0; i < _input.Keyboards.Count; i++)
		{
			_input.Keyboards[i].KeyDown += KeyDown;
			_input.Keyboards[i].KeyUp += KeyUp;
		}

		// Mouse
		if (_input.Mice.Count > 0)
		{
			_mouse = _input.Mice[0];
			_mouse.MouseDown += MouseDown;
			_mouse.MouseUp += MouseUp;
			_mouse.MouseMove += MouseMove;
			_mouse.Scroll += MouseScroll;
		}
	}

	private void	KeyDown(IKeyboard keyboard, Key key, int keyCode)
	{

	}

	private void	KeyUp(IKeyboard keyboard, Key key, int keyCode)
	{

	}

	private void	MouseDown(IMouse mouse, MouseButton mouseButton)
	{

	}

	private void	MouseUp(IMouse mouse, MouseButton mouseButton)
	{

	}

	private void	MouseMove(IMouse mouse, Vector2 position)
	{

	}

	private void	MouseScroll(IMouse mouse, ScrollWheel scroll)
	{

	}
}
