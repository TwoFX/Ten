using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Ten
{
	class Tile
	{
		public const int TILE_BOUNDS = 5;

		private Tile(bool[,] Contention, Color Color)
		{
			this.Contention = Contention;
			this.Color = Color;
		}

		public bool[,] Contention
		{
			get;
			private set;
		}

		public Color Color
		{
			get;
			private set;
		}

		private static bool[,] contentionFromString(string s)
		{
			if (s == null)
				throw new ArgumentNullException(nameof(s));

			if (s.Length != TILE_BOUNDS * TILE_BOUNDS)
				throw new ArgumentOutOfRangeException(nameof(s));

			bool[,] result = new bool[TILE_BOUNDS, TILE_BOUNDS];

			for (int i = 0; i < TILE_BOUNDS; i++)
			{
				for (int j = 0; j < TILE_BOUNDS; j++)
				{
					result[i, j] = s[i * TILE_BOUNDS + j] == '1';
				}
			}

			return result;
		}

		public static ReadOnlyCollection<Tile> All { get; } = Array.AsReadOnly(new[]
		{
			new Tile(contentionFromString("1000000000000000000000000"), Color.FromRgb(116, 134, 210)), // 1x1 Block
			new Tile(contentionFromString("1100011000000000000000000"), Color.FromRgb(152, 220, 85)), // 2x2 Block
			new Tile(contentionFromString("1110011100111000000000000"), Color.FromRgb(70, 211, 173)), // 3x3 Block

			new Tile(contentionFromString("1111100000000000000000000"), Color.FromRgb(220, 101, 85)), // Horizontal 5x1
			new Tile(contentionFromString("1000010000100001000010000"), Color.FromRgb(220, 101, 85)), // Vertical 1x5

			new Tile(contentionFromString("1111000000000000000000000"), Color.FromRgb(228, 89, 116)), // Horizontal 4x1
			new Tile(contentionFromString("1000010000100001000000000"), Color.FromRgb(228, 89, 116)), // Vertical 1x4

			new Tile(contentionFromString("1110000000000000000000000"), Color.FromRgb(237, 149, 74)), // Horizontal 3x1
			new Tile(contentionFromString("1000010000100000000000000"), Color.FromRgb(237, 149, 74)), // Vertical 1x3

			new Tile(contentionFromString("1100000000000000000000000"), Color.FromRgb(254, 198, 60)), // Horizontal 2x1
			new Tile(contentionFromString("1000010000000000000000000"), Color.FromRgb(254, 198, 60)), // Vertical 1x2

			new Tile(contentionFromString("1100010000000000000000000"), Color.FromRgb(89, 203, 134)), // Small corner, bottom right missing
			new Tile(contentionFromString("1100001000000000000000000"), Color.FromRgb(89, 203, 134)), // Small corner, bottom left missing
			new Tile(contentionFromString("0100011000000000000000000"), Color.FromRgb(89, 203, 134)), // Small corner, top left missing
			new Tile(contentionFromString("1000011000000000000000000"), Color.FromRgb(89, 203, 134)), // Small corner, top right missing

			new Tile(contentionFromString("1110010000100000000000000"), Color.FromRgb(92, 190, 228)), // Large corner, bottom right missing
			new Tile(contentionFromString("1110000100001000000000000"), Color.FromRgb(92, 190, 228)), // Large corner, bottom left missing
			new Tile(contentionFromString("0010000100111000000000000"), Color.FromRgb(92, 190, 228)), // Large corner, top left missing
			new Tile(contentionFromString("1000010000111000000000000"), Color.FromRgb(92, 190, 228)), // Large corner, top right missing
		});
	}
}
