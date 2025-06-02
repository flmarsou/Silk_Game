using System;
using System.Collections.Generic;

class	MapGeneration
{
	private const int				_mapSize = 40;
	private const int				_roomAmout = 20;
	private const int				_minRoomSize = 4;
	private const int				_maxRoomSize = 10;
	private readonly List<Room>		_rooms = new List<Room>();
	private readonly static Random	_rng = new Random();

	public static int[,]	map;

	public	MapGeneration()
	{
		map = new int[_mapSize, _mapSize];

		// Fill map with walls
		for (int y = 0; y < _mapSize; y++)
			for (int x = 0; x < _mapSize; x++)
				map[y, x] = '.';

		// Generate rooms
		for (int i = 0; i < _roomAmout; i++)
		{
			// Create a randomly-sized rectangle room
			int	roomWidth = _rng.Next(_minRoomSize, _maxRoomSize);
			int	roomHeight = _rng.Next(_minRoomSize, _maxRoomSize);
			int	posX = _rng.Next(1, _mapSize - roomWidth - 1);
			int	posY = _rng.Next(1, _mapSize - roomHeight - 1);

			Room newRoom = new Room(posX, posY, roomWidth, roomHeight);

			// Check if the new room doesn't overlap with another
			bool	overlaps = false;
			for (int item = 0; item < _rooms.Count; item++)
			{
				if (newRoom.Overlap(_rooms[item]))
				{
					overlaps = true;
					break ;
				}
			}

			// Add the room
			if (!overlaps)
			{
				CreateRoom(newRoom);
				// Create tunnels
				if (_rooms.Count > 0)
				{
					Room	prevRoom = _rooms[_rooms.Count - 1];
					ConnectRooms(prevRoom.Center(), newRoom.Center());
				}
				_rooms.Add(newRoom);
			}
		}
	}

	static void	CreateRoom(Room room)
	{
		for (int y = room.Y1; y < room.Y2; y++)
			for (int x = room.X1; x < room.X2; x++)
				map[y, x] = '#';
	}

	static void	ConnectRooms((int x, int y)prevRoom, (int x, int y)newRoom)
	{
		if (_rng.Next(2) == 0)
		{
			CreateHorizontalTunnel(prevRoom.x, newRoom.x, prevRoom.y);
			CreateVerticalTunnel(prevRoom.y, newRoom.y, newRoom.x);
		}
		else
		{
			CreateVerticalTunnel(prevRoom.y, newRoom.y, prevRoom.x);
			CreateHorizontalTunnel(prevRoom.x, newRoom.x, newRoom.y);
		}
	}

	static void	CreateHorizontalTunnel(int src, int dest, int y)
	{
		for (int x = Math.Min(src, dest); x < Math.Max(src, dest); x++)
			map[y, x] = '#';
	}

	static void	CreateVerticalTunnel(int src, int dest, int x)
	{
		for (int y = Math.Min(src, dest); y < Math.Max(src, dest); y++)
			map[y, x] = '#';
	}

	private class	Room(int posX, int posY, int roomWidth, int roomHeight)
	{
		private const int	_padding = 2;

		public readonly int X1 = posX;
		public readonly int Y1 = posY;
		public readonly int X2 = posX + roomWidth;
		public readonly int Y2 = posY + roomHeight;

		public bool	Overlap(Room other)
		{

			return !(this.X2 + _padding <= other.X1
					|| this.X1 - _padding >= other.X2
					|| this.Y2 + _padding <= other.Y1
					|| this.Y1 - _padding >= other.Y2);
		}

		public (int x, int y) Center()
		{
			return ((X1 + X2) / 2, (Y1 + Y2) / 2);
		}
	}
}
