using UnityEngine;

public class Logic
{
	public static bool isPAUSE = false;

	public static void PAUSE ()
	{
		if (isPAUSE)
			return;
		Time.timeScale = 0;
		isPAUSE = true;
	}

	public static void UNPAUSE ()
	{
		if (!isPAUSE)
			return;
		isPAUSE = false;
		Time.timeScale = 1;
	}
}
