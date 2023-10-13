using SFML.Graphics;
using SFML.System;

class Maze
{
	public bool[] MazeData;
	public int Width;
	public int Height; //! height might not be needed (remove)
	public uint BlockSize;

	private Sprite sprite;

	public Maze(int width, int height, int? seed)
	{
		// Store map data in bool array
		// TODO: Make this private property
		MazeData = new bool[width * height];
		Width = width;
		Height = height;

		// Make new random with the provided seed if one was given
		Random random = seed == null ? new Random() : new Random((int)seed);

		// Get texture ready for drawing
		BlockSize = App.Window.Size.X / (uint)width;
		RenderTexture texture = new RenderTexture((uint)width * BlockSize, (uint)height * BlockSize);
		RectangleShape block = new RectangleShape(new Vector2f(BlockSize, BlockSize));

		// Generate the maze data
		// TODO: Generate a proper maze. Not this random garbage
		//! Not 100% solvable
		int index = 0;
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				// Decide if the current cell should be a block or not
				// TODO: Check for what neighboring cells are and use to that to generate paths
				double probability = random.NextDouble();
				if (probability <= 0.5)
				{
					// Add a new block at the current position
					MazeData[index] = true;

					// Add a block to the texture
					block.Position = new Vector2f(x * BlockSize, y * BlockSize);
					texture.Draw(block);
				}

				index++;
			}
		}

		// Flip the texture
		texture.Display();
		sprite = new Sprite(texture.Texture);
	}

	public void Render()
	{
		App.Window.Draw(sprite);
	}
}