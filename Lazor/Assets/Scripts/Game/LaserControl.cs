using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class POINTHIT
{
	public Vector2 ContactPoint = Vector2.zero;
	public int indexContactPoint = 0;
	public bool CreateNewLine = false;
	public bool NonReflex = false;
}

public class LaserControl : MonoBehaviour
{


	[Header ("Laser Prefab")]
	[SerializeField]GameObject laserPrefab = null;
	[Header ("Number Laser")]
	[SerializeField]int maxNumberLaser = 0;
	int indexLaser = 0;
	[Header ("Index Direction Start")][Range (1, 8)]
	public int indexDirection = 1;
	LayerMask maskBOX;

	// DIRECTION ==================
	Vector2[] TempPoints = new Vector2[4];
	Vector2[] Directions = new Vector2[8];


	int indexDirectionStart = 0;

	public Vector2 CurrentDirection = Vector2.zero;

	LaserScript[] lasersScript;

	LaserScript CurrentLaser;

	Vector2 CurrentPointStart;

	public string nameLaserPrefix;


	List<ReturnHit> listReturnHits = new List<ReturnHit> ();



	[Header ("LASER CONTROL PREFABS")]
	public GameObject laserControlPref;

	bool isDivision = false;


	public GameObject imgStart;


	[HideInInspector]public Transform transBoxLatter;

	public void Clear ()
	{

		for (int i = 0; i < maxNumberLaser; i++) {
			Destroy (lasersScript [i].gameObject);
		}
		if (listReturnHits.Count != 0) {
			foreach (ReturnHit hit in listReturnHits) {
				hit.ClearLaserDirection ();
			}
		}


		listReturnHits.Clear ();

		Destroy (this.gameObject);  
	}

	void CreateLaserLine ()
	{
		lasersScript = new LaserScript[maxNumberLaser];

		for (int i = 0; i < maxNumberLaser; i++) {
			GameObject temp = (GameObject)Instantiate (laserPrefab);
			temp.name = nameLaserPrefix + "_" + i;
			temp.SetActive (false);
			lasersScript [i] = temp.GetComponent<LaserScript> ();
		}
		CurrentLaser = lasersScript [indexLaser];
		CurrentLaser.gameObject.SetActive (true);
		CurrentPointStart = this.transform.position;
	}


	// SET ON START

	float sizePX = 2.58f;
	public void SetOnDraw (LASERINFO TEMP)
	{
		this.transform.position = TEMP.position;
		indexDirection = TEMP.indexDirection;
		this.transform.position = POINTSTART (TEMP.indexStartPoint);
		imgStart.SetActive (true);
		this._Awake ();
	}

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

	/// <summary>
	///  Awake là cái gì vậy?
	/// </summary>
	void _Awake ()
	{

		indexLaser = 0;

		isDivision = false;

		this.CreateLaserLine ();
		maskBOX = LayerMask.NameToLayer ("BOX");


		TempPoints [0] = new Vector2 (0, 1);
		TempPoints [1] = new Vector2 (1, 0);
		TempPoints [2] = new Vector2 (0, -1);
		TempPoints [3] = new Vector2 (-1, 0);

		Directions [0] = TakeDirection (2, 1);
		Directions [1] = TakeDirection (0, 1);
		Directions [2] = TakeDirection (3, 2);
		Directions [3] = TakeDirection (1, 2);
		Directions [4] = TakeDirection (0, 3);
		Directions [5] = TakeDirection (2, 3);
		Directions [6] = TakeDirection (1, 0);
		Directions [7] = TakeDirection (3, 0);


		indexDirectionStart = indexDirection;

		CurrentDirection = Directions [indexDirectionStart - 1];

	}

	Vector2 TakeDirection (int start, int end)
	{
		Vector2 temp = TempPoints [end] - TempPoints [start];
		return temp;
	}

	RaycastHit2D CheckDirection (Vector2 origin, Vector2 diretion)
	{
		RaycastHit2D hit = Physics2D.Raycast (origin, diretion, 100, 1 << maskBOX);
		return hit;

	}

	bool drawed = false;

	void Update ()
	{
//		if (Input.GetKeyDown (KeyCode.Space)) {
//			//this.CheckDirection (newVerticies [countVerticles], CurrentDirection);
//		}
		if (!drawed)
			this.DrawLine ();
		else {
			
		}
	}

	public void ResetLaser ()
	{

		countContact = 0;

		drawed = true;
		for (int i = 0; i < lasersScript.Length; i++) {
			lasersScript [i].gameObject.SetActive (false);
			lasersScript [i].lineRenderer.SetVertexCount (0);
		}

		indexLaser = 0;
		CurrentLaser = lasersScript [indexLaser];
		CurrentLaser.gameObject.SetActive (true);

		CurrentPointStart = this.transform.position;
		CurrentDirection = Directions [indexDirectionStart - 1];

		indexDirection = indexDirectionStart;


		if (listReturnHits.Count != 0) {
			foreach (ReturnHit hit in listReturnHits) {
				hit.ClearLaserDirection ();
			}
		}
		listReturnHits.Clear ();

		StartCoroutine (delayResetLasers ());
	}


	IEnumerator delayResetLasers ()
	{
		yield return new  WaitForSecondsRealtime (0.25f);
		drawed = false;
	}


	// cảm thấy code tù vl ~~
	public void GiveIndexDirectionNext (int indexContactPoint)
	{
		switch (indexDirection) {
		case 1:
			if (indexContactPoint == 3) {
				indexDirection = 7;				
			} else if (indexContactPoint == 2) {
				indexDirection = 2;			
			} 
			break;
		case 2:
			if (indexContactPoint == 3) {
				indexDirection = 4;
			} else if (indexContactPoint == 0) {
				indexDirection = 1;
			}
			break;
		case 3:
			if (indexContactPoint == 3) {
				indexDirection = 4;
			} else if (indexContactPoint == 0) {
				indexDirection = 1;
			}
			break;
		case 4:

			if (indexContactPoint == 1) {
				indexDirection = 3;
			} else if (indexContactPoint == 0) {
				indexDirection = 6;
			} 
			break;
		case 5:
			if (indexContactPoint == 0) {
				indexDirection = 6;
			} else if (indexContactPoint == 1) {
				indexDirection = 3;
			}
			break;
		case 6:
			if (indexContactPoint == 2) {
				indexDirection = 5;
			} else if (indexContactPoint == 1) {
				indexDirection = 8;
			} 
			break;
		case 7:
			if (indexContactPoint == 2) {
				indexDirection = 5;
			} else if (indexContactPoint == 1) {
				indexDirection = 8;
			} 
			break;
		case 8:
			if (indexContactPoint == 3) {
				indexDirection = 7;				
			} else if (indexContactPoint == 2) {
				indexDirection = 2;			
			} 
			break;
		default:
			
			break;
		}
	}

	public void DrawLine ()
	{
		if (indexLaser >= maxNumberLaser - 1)
			return;

		RaycastHit2D hit = CheckDirection (CurrentPointStart, CurrentDirection);

		Debug.DrawRay (CurrentPointStart, CurrentDirection, Color.red);

		if (hit.collider != null) {
			if (hit.collider.tag == "BOX_II") {
//				bool isContact = false;
//				if (Vector2.Distance (hit.point, CurrentPointStart) < 0.645f) {
//					isContact = true;
//				} else {
//					isContact = false;
//				}




				ReturnHit Hit = hit.collider.GetComponent<ReturnHit> ();
				indexLaser += 1;
				Vector3[] temps = new Vector3[2];
				temps [0] = CurrentPointStart;

				POINTHIT[] pointHit = Hit.PointContact (hit.point, indexDirection);


				int Length = pointHit.Length;
				if (Length == 1) {
					// Set Point End Laser
					temps [1] = pointHit [0].ContactPoint;

					EffectsManager.Instance.PlayEffectLaserHit (temps [1]);


					if (pointHit [0].NonReflex) {
						drawed = true;
					}
			
					CurrentLaser.AddPoints (temps);

					CurrentLaser = lasersScript [indexLaser];
					CurrentLaser.gameObject.SetActive (true);
				


					// Set Point Start New

					CurrentPointStart = temps [1];

					this.GiveIndexDirectionNext (pointHit [0].indexContactPoint);

//					if (Vector3.Distance (temps [0], temps [1]) < 0.25f) {
//						drawed = true;
//					}
				} else if (Length == 2) {
					if (pointHit [0].CreateNewLine) {

						isDivision = true;
						// Set Point End Laser
						temps [1] = pointHit [1].ContactPoint;

						EffectsManager.Instance.PlayEffectLaserHit (pointHit [0].ContactPoint);


						CurrentLaser.AddPoints (temps);


						CurrentLaser = lasersScript [indexLaser];
						CurrentLaser.gameObject.SetActive (true);




						// Set Point Start New
						CurrentPointStart = pointHit [0].ContactPoint;


						// Create Laser II
						this.CreateLaserNew (indexDirection, pointHit [1].ContactPoint, hit.collider.transform);


						this.GiveIndexDirectionNext (pointHit [0].indexContactPoint);
				
					} else {
						isDivision = false;
						temps [1] = pointHit [0].ContactPoint;

						EffectsManager.Instance.PlayEffectLaserHit (temps [1]);



						CurrentLaser.AddPoints (temps);

						CurrentLaser = lasersScript [indexLaser];
						CurrentLaser.gameObject.SetActive (true);


						// Set Point Start New
						CurrentPointStart = pointHit [1].ContactPoint;
						this.GiveIndexDirectionNext (pointHit [1].indexContactPoint);

					}

				}


				// add To List

				// Take Direction Next

				CurrentDirection = Directions [indexDirection - 1];

				string directionTemp = "" + pointHit [0].indexContactPoint + "_" + indexDirection;


				if (!Hit.CheckDirectionOverlap (directionTemp)) {
					listReturnHits.Add (Hit);
					Hit.ListLaserDirection.Add (directionTemp);
				} else {
					drawed = true;
				}

//				if (pointHit.indexContactPoint == 3) {
//					
//				} else if (pointHit.indexContactPoint == 2) {
//					
//				} else if (pointHit.indexContactPoint == 1) {
//					
//				} else {
//					
//				}

				// Check Contact
				if (transBoxLatter != null) {
					//  YM WTF?
					if (Vector3.Distance (transBoxLatter.position, hit.collider.transform.position) <= 2.86f) {
						countContact += 1;
						if (countContact == 2) {
							drawed = true;
							countContact = 0;
							return;
						}
						//	drawed = true;
						//	return;
					}
				}
				transBoxLatter = hit.collider.transform;

				//his.DrawLine ();
			}
		} else {
			drawed = true;
			Vector3[] temps = new Vector3[2];
			temps [0] = CurrentPointStart;
			temps [1] = new Vector3 (temps [0].x, temps [0].y, 0) + new Vector3 (CurrentDirection.x, CurrentDirection.y, 0) * 15f;
			CurrentLaser.AddPoints (temps);

		}
	}

	int countContact = 0;

	public void EnableLineRendererLaser (bool status)
	{
		for (int i = 0; i < lasersScript.Length; i++) {
			lasersScript [i].lineRenderer.enabled = status;
		}
	}


	public void CreateLaserNew (int _directionStart, Vector2 pointStart, Transform boxLater)
	{
		GameObject LaserGo = (GameObject)Instantiate (laserControlPref, pointStart, Quaternion.identity);
		LaserGo.name = "LaserControlNew";
		LaserControl newTemp = LaserGo.GetComponent<LaserControl> ();
		newTemp.SETUP (_directionStart, boxLater);
		LaserControlManager.Instance.Lasers.Add (newTemp);
	}

	public void SETUP (int _directionStart, Transform boxLatter)
	{
		this.transBoxLatter = boxLatter;
		this.indexDirectionStart = _directionStart;
		this._Awake ();
	}
}
