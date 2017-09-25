using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MODEDATA
{
	public string NameMode;
	public int idMode;
	public int TotalLevel;
	public string[] TextLevel;
}

public class ModeLoader : MonoBehaviour
{


	public const string path = "LEVELS";
	public List<MODEDATA> ListModeData = new List<MODEDATA> ();

	private static ModeLoader instance;

	public static ModeLoader Instance {
		get { 
			return instance;
		}
	}

	void Awake ()
	{		
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	void Start ()
	{
		ModeContainer mc = ModeContainer.Load (path);
		int totalMode = mc.modes.Count;
		int count = 0;
		foreach (Mode mode in mc.modes) {
			MODEDATA temp = new MODEDATA ();
			temp.idMode = count;
			temp.NameMode = mode.nameMode;
			temp.TotalLevel = mode.texts.Count;
			temp.TextLevel = mode.texts.ToArray ();
			ListModeData.Add (temp);
			count++;
//			foreach (string text in mode.texts) {
//			}
		}
		SelectModeManager.Instance.CreateMode (ListModeData.ToArray ());
	}

	public MODEDATA MODE_SELECTED (int idmode)
	{
		return ListModeData [idmode];
	}



	// Load Level
	[HideInInspector]public INFO_LEVEL CurrentINFO_LEVEL = new INFO_LEVEL ();

	public void OnSelectLevel (int IdMode, int IdLevel)
	{
		// Load Scene Level;
		MODEDATA Temp = MODE_SELECTED (IdMode);
		CurrentINFO_LEVEL.textCurrentLevel = Temp.TextLevel [IdLevel];
		CurrentINFO_LEVEL.idCurrentLevel = IdLevel;
		CurrentINFO_LEVEL.idCurrentMode = IdMode;
		CurrentINFO_LEVEL.maxLevelCurrentMode = Temp.TotalLevel;
		this.LoadSceneLevel ();
	}

	public void LoadSceneLevel ()
	{
		SceneManager.LoadScene (1);
	}
}

[System.Serializable]
public class INFO_LEVEL
{
	public string textCurrentLevel = "";
	public int idCurrentLevel = 0;
	public int idCurrentMode = 0;
	public int maxLevelCurrentMode = 0;

}