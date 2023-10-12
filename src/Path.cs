using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Path
{

	public Path(Maze maze, Vector2i startPosition, Vector2i endPosition)
	{
		
	}

	public void Draw()
	{

	}







	// TODO: Add way to change the thickness
	private static void DrawLine(Vector2f start, Vector2f end, Color color)
	{	
		// Create the line
		// TODO: Don't do this every frame
		Vertex[] line = new Vertex[2];

		// Set the starting position
		line[0].Position = start;
		line[0].Color = color;

		// Set the ending position
		line[1].Position = end;
		line[1].Color = color;

		// Draw it
		App.Window.Draw(line, PrimitiveType.Lines);
	}
}