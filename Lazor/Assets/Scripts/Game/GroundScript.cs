using UnityEngine;
using System.Collections;

public class GroundScript : MonoBehaviour
{
	SpriteRenderer spr;
	[Header ("Image Normal & Selected")]
	public Sprite spNormal, spSelect;
	[HideInInspector]public bool selected = false;

	void Start ()
	{
		spr = GetComponent<SpriteRenderer> ();
		spr.sprite = spNormal;
	}

	public void OnSelected ()
	{
		if (selected)
			return;
		selected = true;
		spr.sprite = spSelect;
	}

	public void OnDeselected ()
	{
		if (!selected)
			return;
		selected = false;
		spr.sprite = spNormal;
	}
}
