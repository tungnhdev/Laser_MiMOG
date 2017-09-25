using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CHECKPOINTINFO
{
	public Vector3 position;
	public int indexPoint;
}

public class CheckPointManager : MonoBehaviour
{
	//public static CheckPointManager Instance;
	public List<CheckPointScript> listCheckPoint = new List<CheckPointScript> ();
	public GameObject checkPointPref;
	int totalCheckpoint = 0;
	void Awake ()
	{
//		if (Instance == null)
//			Instance = this;
	}

	void Start ()
	{
		
	}

	void Update ()
	{
	}

	public void RemoveList ()
	{
		foreach (CheckPointScript temp in listCheckPoint) {
			temp.ToNormal ();
		}
	}

	public void CreateCheckPoint (CHECKPOINTINFO[] TEMPS)
	{
		totalCheckpoint = TEMPS.Length;
		for (int i = 0; i < totalCheckpoint; i++) {
			GameObject ins = Instantiate (checkPointPref) as GameObject;
			ins.name = "Checkpoint_" + i;
			this.transform.position = TEMPS [i].position;
			ins.transform.position = POINTSTART (TEMPS [i].indexPoint);
			listCheckPoint.Add (ins);
		}
	}
	float sizePX = 2.58f;
	Vector2 POINTSTART (int index)
	{
		Vector2 temp = Vector2.zero;
		if (index == 0)
			temp = this.transform.TransformPoint (new Vector2 (0, sizePX / 2));
		else if (index == 1)
			temp = this.transform.TransformPoint (new Vector2 (sizePX / 2, 0));
		else if (index == 2)
			temp = this.transform.TransformPoint (new Vector2 (0, -sizePX / 2));
		else if (index == 3)
			temp = this.transform.TransformPoint (new Vector2 (-sizePX / 2, 0));
		return temp;
	}
}
