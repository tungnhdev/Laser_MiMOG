using UnityEngine;
using System.Collections;

public class SelectModeManager : MonoBehaviour
{
	public static SelectModeManager Instance;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}

	public GameObject btnModePref;
	public RectTransform Grid;
	public SelectLevelManager selectLevelManager;


	public void CreateMode (MODEDATA[] TEMPS)
	{
		int totalMode = TEMPS.Length;
		for (int i = 0; i < totalMode; i++) {
			GameObject btnMode = Instantiate (btnModePref) as GameObject;
			btnMode.name = "btnSelectMode_" + i;
			btnMode.transform.SetParent (Grid);
			btnMode.transform.localScale = new Vector3 (1, 1, 1);
			btnMode.GetComponent<SelectModeScript> ().SETUP (TEMPS [i]);
		}
	}

	public void OnSelectMode (int idmode)
	{
		selectLevelManager.CreateLevel (ModeLoader.Instance.MODE_SELECTED (idmode));
	}
}
