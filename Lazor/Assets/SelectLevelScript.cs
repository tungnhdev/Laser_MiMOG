using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectLevelScript : MonoBehaviour
{
	[SerializeField] Text textInfo;
	[SerializeField] Button btn;
	public int IdLevel;
	public string NameLevel;
	bool unlocked = false;
	SelectLevelManager selectLevelManager;

	void Start ()
	{
		btn.onClick.AddListener (delegate {
			OnClick ();
		});
	}

	public void SETUP (string nameLevel, int idlevel, SelectLevelManager selectLevel)
	{
		selectLevelManager = selectLevel;
		NameLevel = nameLevel + "_" + idlevel;
		IdLevel = idlevel;
		unlocked = PlayerPrefs.GetInt (NameLevel + "unlocked", 0) == 0 ? false : true;
		btn.enabled = true;


		textInfo.text = NameLevel;
	}

	public void OnClick ()
	{
		selectLevelManager.OnSelectLevel (IdLevel);
	}

}
