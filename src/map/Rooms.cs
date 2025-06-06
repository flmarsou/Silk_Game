public partial class	Dungeon
{
	private class	Room
	{
		private const int	_padding = 3;

		public int	x { get; }
		public int	y { get; }
		public int	width { get; }
		public int	height { get; }
		public bool	connected { get; set; }

		public	Room(int posX, int poxY, int roomWidth, int roomHeight)
		{
			x = posX;
			y = poxY;
			width = posX + roomWidth;
			height = poxY + roomHeight;
			connected = false;
		}

		public bool	Overlap(Room other)
		{

			return !(this.width + _padding <= other.x
					|| this.x - _padding >= other.width
					|| this.height + _padding <= other.y
					|| this.y - _padding >= other.height);
		}

		public (int x, int y)	Center()
		{
			return ((x + width) / 2, (y + height) / 2);
		}
	}

	private static void	CreateRoom(Tile[,] map, Room room)
	{
		for (int y = room.y; y < room.height; y++)
			for (int x = room.x; x < room.width; x++)
				map[y, x] = Tile.Floor;
	}
}
