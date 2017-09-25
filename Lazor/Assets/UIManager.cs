using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public GameObject uiMenu, uiEndgame;

	public void OnClickMenu ()
	{
		Logic.PAUSE ();
		uiMenu.SetActive (true);
	}

	public void BackMenu ()
	{
		Logic.UNPAUSE ();
		uiMenu.SetActive (false);
	}

	public void OnEndgame ()
	{
		Logic.PAUSE ();
		uiEndgame.SetActive (true);
	}

	public void ToLevel ()
	{
		
	}
}
