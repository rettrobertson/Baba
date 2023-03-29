using Microsoft.Xna.Framework;
using System;

namespace Baba.GameComponents
{
	public interface IItem
	{
		void Draw(GameTime gameTime);
		bool Right();
		bool Left();
		bool Up();
		bool Down();
		void AddLocation(IItem[,] grid, int x, int y);
	}
}
