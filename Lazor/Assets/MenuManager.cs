using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	public GameObject goMode;

	public void OnClickPlay ()
	{
		goMode.SetActive (true);
	}

	public void Back_Mode ()
	{
		goMode.SetActive (false);
	}
}
