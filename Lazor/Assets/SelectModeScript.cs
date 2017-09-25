using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectModeScript : MonoBehaviour
{
	[SerializeField] Text textInfo;
	[SerializeField] Button btn;
	bool unlocked = false;
	string ModeName;
	int TotalLevel;
	int IdMode = 0;

	void Start ()
	{
		btn.onClick.AddListener (delegate {
			OnClick ();
		});
	}

	public void SETUP (MODEDATA TEMP)
	{
		IdMode = TEMP.idMode;
		TotalLevel = TEMP.TotalLevel;
		ModeName = TEMP.NameMode;
		unlocked = PlayerPrefs.GetInt (ModeName + "unlocked", 0) == 0 ? false : true;
		btn.enabled = true;
		textInfo.text = ModeName + "_Levels: " + TotalLevel; 
	}

	public void OnClick ()
	{
		SelectModeManager.Instance.OnSelectMode (IdMode);
	}
}
