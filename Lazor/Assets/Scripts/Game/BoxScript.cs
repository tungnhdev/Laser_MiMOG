using UnityEngine;
using System.Collections;
using TungDz;
public class BoxScript : MonoBehaviour
{

	public GameObject groundPref;
	public BoxCollider2D boxCollider2D;
	//[HideInInspector]
	[HideInInspector]public GameObject Ground;
	//bool selected = false;
	public SpriteRenderer spr;

	[Header ("Image size 256*256")]
	// Sprite Normal
	public Sprite spNormal;
	public Sprite spSelected;
	[SerializeField]ReturnHit returnHit;
	void Start ()
	{

		Ground = (GameObject)Instantiate (groundPref, transform.position, Quaternion.identity);
		Ground.SetActive (false);
		// SET sprite Normal;
		spr.sprite = spNormal;
	}

	void OnMouseDown ()
	{
	//	selected = true;
		spr.sprite = spSelected;
		TouchControl.Instance.OnSelectedBox (this);
	}
	// Deselect Object On Mouse Up
	void OnMouseUp ()
	{
	//	selected = false;
		spr.sprite = spNormal;
		TouchControl.Instance.DeselectedBox ();
	}

	public void MoveToNewGround (GameObject ground)
	{
		if (Ground != null) {
			Ground.SetActive (true);
			Ground.SendMessage ("OnDeselected");
		}
		this.Ground = ground;
		this.transform.position = Ground.transform.position;
		Ground.SetActive (false);
		returnHit.TakeContactPosition ();
	}
}
