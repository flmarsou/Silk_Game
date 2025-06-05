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
				_rooms.Add(newRoom);
			}
		}

		// Generate tunnels
		for (int i = 0; i < _rooms.Count; i++)
		{
			Room	currentRoom = _rooms[i];
			Room	closestRoom = FindClosestRoom(currentRoom);

			if (closestRoom != null)
			{
				ConnectRooms(currentRoom.Center(), closestRoom.Center());
				_rooms[i].connected = true;
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
		int	start = Math.Min(src, dest);
		int	end = Math.Max(src, dest);

		for (int x = start; x <= end; x++)
			map[y, x] = '#';
	}

	static void	CreateVerticalTunnel(int src, int dest, int x)
	{
		int	start = Math.Min(src, dest);
		int	end = Math.Max(src, dest);

		for (int y = start; y < end; y++)
			map[y, x] = '#';
	}

	private class	Room(int posX, int posY, int roomWidth, int roomHeight)
	{
		private const int	_padding = 2;

		public readonly int X1 = posX;
		public readonly int Y1 = posY;
		public readonly int X2 = posX + roomWidth;
		public readonly int Y2 = posY + roomHeight;
		public bool			connected = false;

		public bool				Overlap(Room other)
		{

			return !(this.X2 + _padding <= other.X1
					|| this.X1 - _padding >= other.X2
					|| this.Y2 + _padding <= other.Y1
					|| this.Y1 - _padding >= other.Y2);
		}

		public (int x, int y)	Center()
		{
			return ((X1 + X2) / 2, (Y1 + Y2) / 2);
		}

		public (int x, int y)	Exit()
		{
			int	x = _rng.Next(X1 + 1, X2 - 1);
			int	y = _rng.Next(Y1 + 1, Y2 - 1);

			return (x, y);
		}
	}

	private Room	FindClosestRoom(Room currentRoom)
	{
		(int x, int y)	currentCenter = currentRoom.Center();
		Room			closestRoom = null;
		double			closestDistance = double.MaxValue;

		for (int i = 0; i < _rooms.Count; i++)
		{
			if (_rooms[i] == currentRoom || _rooms[i].connected == true)
				continue ;

			(int x, int y)	otherCenter = _rooms[i].Center();
			double			distance = Math.Sqrt(
				(currentCenter.x - otherCenter.x) * (currentCenter.x - otherCenter.x) +
				(currentCenter.y - otherCenter.y) * (currentCenter.y - otherCenter.y)
				);

			if (distance < closestDistance)
			{
				closestDistance = distance;
				closestRoom = _rooms[i];
			}
		}

		return (closestRoom);
	}
}
