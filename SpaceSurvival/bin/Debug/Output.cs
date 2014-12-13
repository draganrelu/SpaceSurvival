using System;
using System.IO;

namespace SpaceSurvival
{
	public class Output
	{
		public System.IO.StreamWriter console_file; 
		public System.IO.StreamWriter highscores_file;

		public Output ()
		{
			console_file = new System.IO.StreamWriter("/home/relu/C#_games/SpaceSurvival/SpaceSurvival/Console.txt");
			highscores_file = new System.IO.StreamWriter("/home/relu/C#_games/SpaceSurvival/SpaceSurvival/Highscores.txt");
		}
		public void WriteLine (string s)
		{
			Console.WriteLine (s);
 			console_file.WriteLine (s);

		}

	}
}

