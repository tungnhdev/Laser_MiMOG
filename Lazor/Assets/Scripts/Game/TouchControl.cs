using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TungDz;
public class TouchControl : MonoBehaviour
{
	public static TouchControl Instance;

	[HideInInspector]
	BoxScript BoxSelected;

	// Check Box Selected?
	bool isBoxSelected = false;

	public List<GroundScript> listGround = new List<GroundScript> ();

	// Awake
	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}

	//  On Start
	void Start ()
	{
		this.gameObject.SetActive (false);
	}

	public void OnSelectedBox (BoxScript boxTemp)
	{
		this.gameObject.SetActive (true);
		this.isBoxSelected = true;
		BoxSelected = boxTemp;
	}

	public void DeselectedBox ()
	{
		this.gameObject.SetActive (false);
		this.isBoxSelected = false;
		// Deselected item
		for (int i = 0; i < listGround.Count; i++) {
			if (listGround [i].selected) {
				BoxSelected.MoveToNewGround (listGround [i].gameObject);
			}
			listGround [i].OnDeselected ();
		}
		//Remove item
		listGround.Clear ();
		LaserControlManager.Instance.ResetAllLasers ();
		CheckPointManager.Instance.RemoveList ();
		this.PostEvent (EventID.OnMovedBox);
	}

	void Update ()
	{
		if (!isBoxSelected)
			return;
		// Get mouse position
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0;
		// Set Object position
		this.transform.position = mousePos;

		if (listGround.Count == 0)
			return;
		this.SelectGround ();
	}
	public void SelectGround ()
	{
		listGround [IndexGoundSelected ()].OnSelected ();
	}
	int IndexGoundSelected ()
	{
		listGround [0].OnDeselected ();
		float distanceMin = Vector3.Distance (listGround [0].transform.position, this.transform.position);
		int index = 0;
		int count = listGround.Count;
		if (count == 1)
			return 0;
		for (int i = 1; i < count; i++) {
			float distanceTemp = Vector3.Distance (listGround [i].transform.position, this.transform.position);
			if (distanceMin > distanceTemp) {
				distanceMin = distanceTemp;
				index = i;
			} else {
				listGround [i].OnDeselected ();
			}
		}
		return index;
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.tag == "GroundI") {
			GroundScript temp = coll.GetComponent<GroundScript> ();
			listGround.Add (temp);
		}
	}

	void OnTriggerExit2D (Collider2D coll)
	{
		if (coll.tag == "GroundI") {
			GroundScript temp = coll.GetComponent<GroundScript> ();
			temp.OnDeselected ();
			listGround.Remove (temp);
		}
	}
}
