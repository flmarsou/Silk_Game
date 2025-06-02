using Silk.NET.Windowing;
using Silk.NET.Input;
using System.Numerics;

class	InputManager
{
	private readonly IInputContext	_input;
	private readonly IMouse			_mouse;

	public	InputManager(IWindow window)
	{
		this._input = window.CreateInput();

		// Keyboard
		for (int i = 0; i < this._input.Keyboards.Count; i++)
		{
			this._input.Keyboards[i].KeyDown += KeyDown;
			this._input.Keyboards[i].KeyUp += KeyUp;
		}

		// Mouse
		if (this._input.Mice.Count > 0)
		{
			_mouse = this._input.Mice[0];
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
