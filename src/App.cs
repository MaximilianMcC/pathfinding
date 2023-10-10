using SFML.Graphics;
using SFML.Window;

class App
{
	public static RenderWindow Window;

	private Maze maze;

	public void Run()
	{
		// Create the SFML window
		Window = new RenderWindow(new VideoMode(800, 800), "path fin d");
		Window.SetFramerateLimit(60);
		Window.Closed += (sender, e) => Window.Close();


		maze = new Maze(50, 50, null);


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
		Window.Clear(new Color(0xff00ffff));

		maze.Render();

		// Draw the new frame
		Window.Display();
	}
}