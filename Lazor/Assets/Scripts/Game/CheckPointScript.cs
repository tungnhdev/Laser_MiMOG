using UnityEngine;
using System.Collections;
using TungDz;
public class CheckPointScript : MonoBehaviour
{
	SpriteRenderer spr;
	// Use this for initialization
	public Sprite spNormal, spActive;

	[HideInInspector]public bool isActive = false;

	void Start ()
	{
		spr = this.GetComponent<SpriteRenderer> ();

		this.ToNormal ();

	}
	// Update is called once per frame
	void Update ()
	{
	}

	public void OnActive ()
	{
		if (isActive)
			return;
		isActive = true;
		spr.sprite = spActive;
		this.PostEvent (EventID.OnAddScore);
	}
	public void ToNormal ()
	{
		if (!isActive)
			return;
		isActive = false;
		spr.sprite = spNormal;
	}
}
