using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ReturnHit : MonoBehaviour
{
	[Header("L")]
	public bool _L_ = false;
	[Header("_")]
	public bool I = false;
	float sizePX = 2.56f;
	[HideInInspector]public Vector2[] ContactPOS = new Vector2[4];
	[HideInInspector]public List<string> ListLaserDirection;


	public bool drawLineOnBox = false;
	LaserOnBox laserOnBox;


	public bool isBoxSUM;
	BoxSUM boxSUM;

	public bool NonReflex;



	public bool boxG;
	BoxGScript boxGScipt;


	void Awake ()
	{
		this.TakeContactPosition ();
		if (drawLineOnBox) {
			laserOnBox = this.GetComponent<LaserOnBox> ();
		}
		if (isBoxSUM)
			boxSUM = this.GetComponent<BoxSUM> ();

		if (boxG)
			boxGScipt = this.GetComponent<BoxGScript> ();
	}

	public void TakeContactPosition ()
	{
		ContactPOS [0] = this.transform.TransformPoint (new Vector2 (0, sizePX / 2));
		ContactPOS [1] = this.transform.TransformPoint (new Vector2 (sizePX / 2, 0));
		ContactPOS [2] = this.transform.TransformPoint (new Vector2 (0, -sizePX / 2));
		ContactPOS [3] = this.transform.TransformPoint (new Vector2 (-sizePX / 2, 0));
	}

	public POINTHIT[] PointContact (Vector2 Temp, int indexDirection)
	{

		POINTHIT[] temps;
		float distanceCurrent = Vector2.Distance (Temp, ContactPOS [0]);
		int index = 0;
		for (int i = 1; i < 4; i++) {
			float distanceNew = Vector2.Distance (Temp, ContactPOS [i]);
			if (distanceNew < distanceCurrent) {
				distanceCurrent = distanceNew;
				index = i;
			}
		}
		if (_L_) {
			temps = new POINTHIT[2];
			temps [0] = new POINTHIT ();

			temps [0].CreateNewLine = true;


			temps [0].ContactPoint = ContactPOS [index];
			temps [0].indexContactPoint = index;
			temps [1] = new POINTHIT ();
			index = GetIndexPoint_L_ (indexDirection, index);
			temps [1].ContactPoint = ContactPOS [index];
			temps [1].indexContactPoint = index;
		} else if (I) {
			temps = new POINTHIT[2];
			temps [0] = new POINTHIT ();
			temps [0].CreateNewLine = false;

			temps [0].ContactPoint = ContactPOS [index];
			temps [0].indexContactPoint = index;


			index = GetIndexPointI (indexDirection, index);

			temps [1] = new POINTHIT ();
			temps [1].ContactPoint = ContactPOS [index];
			temps [1].indexContactPoint = index;
			if (drawLineOnBox) {
				Vector3[] points = new Vector3[2];
				points [0] = temps [0].ContactPoint;
				points [1] = temps [1].ContactPoint;
				laserOnBox.DrawLine (points);
			}
		} else {
			temps = new POINTHIT[1];
			temps [0] = new POINTHIT ();
			temps [0].ContactPoint = ContactPOS [index];
			temps [0].indexContactPoint = index;
			temps [0].NonReflex = NonReflex;
			if (isBoxSUM) {
				boxSUM.Active ();
			} else if (boxG) {
				boxGScipt.Active (indexDirection,index);
			}
		}
		return temps;
	}

	public void ClearLaserDirection ()
	{
		ListLaserDirection.Clear ();
	}

	public bool CheckDirectionOverlap (string temp)
	{
		bool overlap = false;
		if (ListLaserDirection.Count == 0)
			return overlap;
		foreach (string tmp in ListLaserDirection) {
			if (temp == tmp) {
				overlap = true;
			}
		}
		return overlap;
	}

	int GetIndexPointI (int index, int indexPointFirst)
	{
		switch (index) {
		case 1:
			if (indexPointFirst == 3)
				return 1;
			else
				return 0;
		case 2:
			if (indexPointFirst == 3)
				return 1;
			else
				return 2;
		case 3:
			if (indexPointFirst == 3)
				return 1;
			else
				return 2;
		case 4:
			if (indexPointFirst == 1)
				return 3;
			else
				return 2;
		case 5:
			if (indexPointFirst == 1)
				return 3;
			else
				return 2;
		case 6:
			if (indexPointFirst == 1)
				return 3;
			else
				return 0;
		case 7:
			if (indexPointFirst == 1)
				return 3;
			else
				return 0;
		case 8:
			if (indexPointFirst == 3)
				return 1;
			else
				return 0;
		default:
			return 0;
		}
	}

	//



	int GetIndexPoint_L_ (int index, int indexPointFirst)
	{
		switch (index) {
		case 1:
			if (indexPointFirst == 3)
				return 0;
			else
				return 1;
		case 2:
			if (indexPointFirst == 3)
				return 2;
			else
				return 1;
		case 3:
			if (indexPointFirst == 3)
				return 2;
			else
				return 0;
		case 4:
			if (indexPointFirst == 1)
				return 2;
			else
				return 3;
		case 5:
			if (indexPointFirst == 1)
				return 2;
			else
				return 3;
		case 6:
			if (indexPointFirst == 1)
				return 0;
			else
				return 3;
		case 7:
			if (indexPointFirst == 1)
				return 0;
			else
				return 3;
		case 8:
			if (indexPointFirst == 3)
				return 0;
			else
				return 1;
		default:
			return 0;
		}
	}
}
