using SFML.Graphics;
using SFML.System;
using SFML.Window;

class App
{
	public static RenderWindow Window;

	private Maze maze;
	private Path path;

	public void Run()
	{
		// Create the SFML window
		Window = new RenderWindow(new VideoMode(800, 800), "path fin d");
		Window.SetFramerateLimit(60);
		Window.Closed += (sender, e) => Window.Close();


		maze = new Maze(50, 50, 123);
		path = new Path(maze, new Vector2i(9, 7), new Vector2i(21, 20));


		// Main game loop
		while (Window.IsOpen)
		{
			// Events
			Window.DispatchEvents();

			// Update everything
			Update();

			// Render everything
			Render();
		}
	}


	private void Update()
	{

	}

	private void Render()
	{
		// Clear the window
		Window.Clear(new Color(0x0));

		maze.Render();
		path.Draw();

		// Draw the new frame
		Window.Display();
	}
}