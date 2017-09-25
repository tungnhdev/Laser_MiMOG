using UnityEngine;
using System.Collections;

public class SelectLevelManager : MonoBehaviour
{
	public GameObject goLevels;
	public GameObject[] btnsLevel;
	int IdMode;


	void Start ()
	{
		for (int i = 0; i < btnsLevel.Length; i++) {
			btnsLevel [i].SetActive (false);
		}
	}

	public void CreateLevel (MODEDATA modeDATA)
	{
		// show PanelLevel;

		goLevels.SetActive (true);

		IdMode = modeDATA.idMode;
		int totalLevel = modeDATA.TotalLevel;
		for (int i = 0; i < btnsLevel.Length; i++) {
			if (i < totalLevel) {
				btnsLevel [i].SetActive (true);
				btnsLevel [i].GetComponent<SelectLevelScript> ().SETUP (modeDATA.NameMode, i, this);
			}
		}
	}

	public void OnSelectLevel (int idLevel)
	{
		ModeLoader.Instance.OnSelectLevel (IdMode, idLevel);
	}

	public void ExitLevel ()
	{
		goLevels.SetActive (false);
	}
}
