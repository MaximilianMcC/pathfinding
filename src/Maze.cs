using SFML.Graphics;
using SFML.System;

class Maze
{
	private Sprite sprite;

	public Maze(uint width, uint height, int? seed)
	{
		// Store map data in bool array
		// TODO: Make this private property
		bool[] mazeData = new bool[width * height];

		// Make new random with the provided seed if one was given
		Random random = seed == null ? new Random() : new Random((int)seed);

		// Get texture ready for drawing
		uint blockSize = App.Window.Size.X / width;
		RenderTexture texture = new RenderTexture(width * blockSize, height * blockSize);
		RectangleShape block = new RectangleShape(new Vector2f(blockSize, blockSize));

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
					mazeData[index] = true;

					// Add a block to the texture
					block.Position = new Vector2f(x * blockSize, y * blockSize);
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