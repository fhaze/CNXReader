using System;
using Gtk;

namespace CNXReader
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Application.Init();
			Settings.Default.SetLongProperty("gtk-button-images", 1, "");
			Settings.Default.SetLongProperty("gtk-menu-images", 1, "");
			MainWindow win = new MainWindow();
			win.Show();
			Application.Run();
		}
	}
}
