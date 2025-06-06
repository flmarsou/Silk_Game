using System;
using System.Collections.Generic;

public enum Tile
{
	Empty,
	Floor,
	Wall,
	Door
}

public partial class	Dungeon
{
	// Map Settings
	private const int	_mapSize = 40;
	private const int	_roomAmout = 20;
	private const int	_minRoomSize = 4;
	private const int	_maxRoomSize = 10;

	private readonly static Random	_rng = new Random();

	public static Tile[,]	GenerateMap()
	{
		Tile[,]	map = new Tile[_mapSize, _mapSize];
		List<Room>	rooms = new List<Room>();

		for (int y = 0; y < _mapSize; y++)
			for (int x = 0; x < _mapSize; x++)
				map[y, x] = Tile.Empty;

		GenerateRooms(map, rooms);
		GenerateWalls(map);
		GenerateTunnels(map, rooms);
		GenerateWalls(map);

		return map;
	}

	static void	GenerateRooms(Tile[,] map, List<Room> rooms)
	{
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

			foreach (Room room in rooms)
			{
				if (newRoom.Overlap(room))
				{
					overlaps = true;
					break ;
				}
			}

			if (overlaps)
				continue ;

			// Add the room
			CreateRoom(map, newRoom);
			rooms.Add(newRoom);
		}
	}

	static void	GenerateTunnels(Tile[,] map, List<Room> rooms)
	{
		foreach (Room room in rooms)
		{
			Room	currentRoom = room;
			Room	closestRoom = FindClosestRoom(rooms, currentRoom);

			if (closestRoom != null)
			{
				ConnectRooms(map, currentRoom.Center(), closestRoom.Center());
				room.connected = true;
			}
		}
	}

	static void	GenerateWalls(Tile[,] map)
	{
		// X and Y direction to check for neighbors
		int[]	dx = { -1,  0,  1, -1,  1, -1,  0,  1 };
		int[]	dy = { -1, -1, -1,  0,  0,  1,  1,  1 };

		for (int y = 0; y < _mapSize; y++)
		{
			for (int x = 0; x < _mapSize; x++)
			{
				if (map[y, x] == Tile.Floor || map[y, x] == Tile.Door)
				{
					for (int i = 0; i < 8; i++)
					{
						// Get neighbor position
						int	nx = x + dx[i];
						int	ny = y + dy[i];

						// Replace Empty tiles by Wall tiles within the boundry of the map
						if (nx >= 0 && nx < _mapSize && ny >= 0 && ny < _mapSize && map[ny, nx] == Tile.Empty)
							map[ny, nx] = Tile.Wall;
					}
				}
			}
		}
	}
}
