using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Path
{
	private Maze maze;
	private Vector2i startPosition;
	private Vector2i endPosition;
	private Dictionary<Vector2i, Vector2i> moves;

	// Create a path
	public Path(Maze maze, Vector2i startPosition, Vector2i endPosition)
	{
		// Assign variables	
		this.maze = maze;
		this.startPosition = startPosition;
		this.endPosition = endPosition;

		// Convert the map to a vector array for easier manipulation
		List<Vector2i> mazeDataList = new List<Vector2i>();
		for (int i = 0; i < maze.MazeData.Length; i++)
		{
			// Check for if the block has a wall or not in it
			if (maze.MazeData[i] == true) continue;

			// Get the X and Y coordinates of the index
			int x = i % maze.Width;
			int y = i / maze.Width;

			// Chuck it into a vector
			mazeDataList.Add(new Vector2i(x, y));
		}
		Vector2i[] mazeData = mazeDataList.ToArray();

		// Store all of the "moves" for drawing
		moves = new Dictionary<Vector2i, Vector2i>();


		/*
			! idk if this actually gonna work idk

			Check for which neighbor has the smallest distance then add the
			smallest one to a list vectors.

			make those new ones also expand until they don't get closer and another
		*/

		Console.WriteLine($"Calculating path from {startPosition} to {endPosition}");

		Vector2i currentPosition = startPosition;
		while (true)
		{
			// Get the neighbors of the current position
			Vector2i[] neighbors = new Vector2i[8];

			// Top row
			neighbors[0] = new Vector2i(currentPosition.X - 1, currentPosition.Y - 1);
			neighbors[1] = new Vector2i(currentPosition.X, currentPosition.Y - 1);
			neighbors[2] = new Vector2i(currentPosition.X + 1, currentPosition.Y - 1);

			// Middle row
			neighbors[3] = new Vector2i(currentPosition.X - 1, currentPosition.Y);
			neighbors[4] = new Vector2i(currentPosition.X + 1, currentPosition.Y);

			// Bottom row
			neighbors[5] = new Vector2i(currentPosition.X - 1, currentPosition.Y + 1);
			neighbors[6] = new Vector2i(currentPosition.X, currentPosition.Y + 1);
			neighbors[7] = new Vector2i(currentPosition.X + 1, currentPosition.Y + 1);



			// Find the neighbor closest to the end position by getting the smallest distance
			//? 8 is neighbors array length and start at 1 because already used first value for initialize smallest distance variable
			int smallestDistance = GetDistance(neighbors[0], endPosition);
			Vector2i smallestNeighbor = neighbors[0];

			for (int i = 1; i < 8; i++)
			{
				int distance = GetDistance(neighbors[i], endPosition);
				if (distance < smallestDistance)
				{
					// TODO: Put in a tuple or something
					smallestDistance = distance;
					smallestNeighbor = neighbors[i];
				}
			}

			// Set the new selected position to the closest neighbor
			moves.Add(currentPosition, smallestNeighbor);
			currentPosition = smallestNeighbor;


			// Check for if we have reached the end of the path
			// Then exit the while loop
			if (currentPosition == endPosition) break;
		}

		Console.WriteLine("Finished calculating path");
	}







	public void Draw()
	{
		// Make a marker to draw the start and end positions
		// TODO: Don't do this every frame
		CircleShape marker = new CircleShape(maze.BlockSize / 2);
		marker.FillColor = new Color(0xff0000ff);

		// Start position
		marker.Position = ((Vector2f)startPosition) * maze.BlockSize;
		App.Window.Draw(marker);

		// End position
		marker.Position = ((Vector2f)endPosition) * maze.BlockSize;
		App.Window.Draw(marker);


		
		// Loop over every single move and draw lines connecting them
		foreach (KeyValuePair<Vector2i, Vector2i> move in moves)
		{
			DrawLine(move.Key, move.Value, Color.Blue);
		}
	}

	// TODO: Add way to change the thickness
	private void DrawLine(Vector2i start, Vector2i end, Color color)
	{	
		// Create the line
		// TODO: Don't do this every frame
		Vertex[] line = new Vertex[2];

		// Set the starting position
		line[0].Position = ((Vector2f)start) * maze.BlockSize;
		line[0].Color = color;

		// Set the ending position
		line[1].Position = ((Vector2f)end) * maze.BlockSize;
		line[1].Color = color;

		// Draw it
		App.Window.Draw(line, PrimitiveType.LineStrip);
	}



	// Get the distance between two vectors
	private int GetDistance(Vector2i position1, Vector2i position2)
	{
		int x = (int)Math.Pow(position2.X - position1.X, 2);
		int y = (int)Math.Pow(position2.Y - position1.Y, 2);

		return (int)Math.Sqrt(x + y);
	}
}