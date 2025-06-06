using System;
using System.Collections.Generic;

public partial class	Dungeon
{
	private static Room	FindClosestRoom(List<Room> rooms, Room currentRoom)
	{
		(int x, int y)	currentCenter = currentRoom.Center();
		Room			closestRoom = null;
		double			closestDistance = double.MaxValue;

		for (int i = 0; i < rooms.Count; i++)
		{
			if (rooms[i] == currentRoom || rooms[i].connected == true)
				continue ;

			(int x, int y)	otherCenter = rooms[i].Center();
			double			distance = Math.Sqrt(
				(currentCenter.x - otherCenter.x) * (currentCenter.x - otherCenter.x) +
				(currentCenter.y - otherCenter.y) * (currentCenter.y - otherCenter.y)
				);

			if (distance < closestDistance)
			{
				closestDistance = distance;
				closestRoom = rooms[i];
			}
		}

		return (closestRoom);
	}

	private static void	ConnectRooms(Tile[,] map, (int x, int y)prevRoom, (int x, int y)newRoom)
	{
		if (_rng.Next(2) == 0)
		{
			CreateHorizontalTunnel(map, prevRoom.x, newRoom.x, prevRoom.y);
			CreateVerticalTunnel(map, prevRoom.y, newRoom.y, newRoom.x);
		}
		else
		{
			CreateVerticalTunnel(map, prevRoom.y, newRoom.y, prevRoom.x);
			CreateHorizontalTunnel(map, prevRoom.x, newRoom.x, newRoom.y);
		}
	}

	private static void	CreateHorizontalTunnel(Tile[,] map, int src, int dest, int y)
	{
		int	start = Math.Min(src, dest);
		int	end = Math.Max(src, dest);

		for (int x = start; x <= end; x++)
		{
			if (map[y, x] == Tile.Wall)
				map[y, x] = Tile.Door;
			else
				map[y, x] = Tile.Floor;
		}
	}

	private static void	CreateVerticalTunnel(Tile[,] map, int src, int dest, int x)
	{
		int	start = Math.Min(src, dest);
		int	end = Math.Max(src, dest);

		for (int y = start; y < end; y++)
		{
			if (map[y, x] == Tile.Wall)
				map[y, x] = Tile.Door;
			else
				map[y, x] = Tile.Floor;
		}
	}
}
