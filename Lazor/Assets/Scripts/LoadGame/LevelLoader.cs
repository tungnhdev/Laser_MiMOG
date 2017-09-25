using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
	public string textLevel = "";
	public GameObject groundPref;
	// hang, cot
	public int row = 0, col = 0;
	// so laser start, so check point
	public int numberLaser, numberCheckPoint;
	int numberGround = 0;
	float unit = 2.58f;
	Vector2 posStart = Vector2.zero;
	List<Vector2> positions = new List<Vector2> ();
	List<Vector2> indexPositions = new List<Vector2> ();
	public GameObject[] _ObjectsBox;
	public GameObject[] _GameObjcts;


	public LaserControlManager laserControlManager;

	int[] listGridIndex;
	int[] laserInfo;
	int[] checkPointInfo;


	[Header ("TEST GAME")]
	public bool isTEST = false;

	[Header ("CAMERA")]
	public CameraControl cameraControl;

	public void ReadLevelTxt ()
	{
		string[] temps = textLevel.Split (' ');

		string levels = temps [0];
		levels.TrimEnd (',');
		string hints = "";
		for (int i = 1; i < temps.Length; i++) {
			if (temps [i].Trim (' ') != "") {
				hints = temps [i];
				break;
			}
		}
		// ReadObject
		string[] texts = levels.Split (',');

		row = int.Parse (texts [0]);
		col = int.Parse (texts [1]);
		numberLaser = int.Parse (texts [2]);
		// laser Info
		laserInfo = new int[numberLaser * 3];

		numberCheckPoint = int.Parse (texts [3]);
		// number Checkpoint

		checkPointInfo = new int[numberCheckPoint * 3];

		// ground 
		numberGround = row * col;

		listGridIndex = new int[numberGround];

		// Ground Info

		int countInfo = 0;
		int countLaserInfo = 0;
		int countCheckPointInfo = 0;


		for (int i = 4; i < texts.Length - 1; i++) {
			if (countInfo < numberGround) {
				listGridIndex [countInfo] = int.Parse (texts [i]);
				countInfo++;
			} else {
				if (countLaserInfo < laserInfo.Length) {
					laserInfo [countLaserInfo] = int.Parse (texts [i]);
					countLaserInfo++;
				} else {
					checkPointInfo [countCheckPointInfo] = int.Parse (texts [i]);
					countCheckPointInfo++;
				}
			}
		}

		// Create Grid
		this.CreateGrid ();

		/// create check Point
		this.CreatCheckPoint ();

		// Create Laser
		this.CreateLaser ();

		//laserControlManager.crea
		// den cho tao laser + check point
	}

	/// <summary>
	/// Creates the laser.
	/// </summary>
	public void CreateLaser ()
	{
		LASERINFO[] LASERTEMPS = new LASERINFO[laserInfo.Length / 3];
		for (int i = 0; i < LASERTEMPS.Length; i++) {
			LASERTEMPS [0] = new LASERINFO ();
			LASERTEMPS [i].indexDirection = laserInfo [(i * 3)];
			LASERTEMPS [i].indexStartPoint = GETIndexPointStart (LASERTEMPS [i].indexDirection);
			Vector2 temp = new Vector2 (laserInfo [(i * 3) + 1], laserInfo [(i * 3) + 2]);
			LASERTEMPS [i].position = positions [GetIndexPositionGrid (temp)];
		}
		laserControlManager.CreateLaserControl (LASERTEMPS);
	}

	public void CreatCheckPoint ()
	{
		CHECKPOINTINFO[] _TEMPS = new CHECKPOINTINFO[checkPointInfo.Length / 3];
		for (int i = 0; i < _TEMPS.Length; i++) {

			_TEMPS [i] = new CHECKPOINTINFO ();

			_TEMPS [i].indexPoint = checkPointInfo [i * 3];
			Vector2 temp = new Vector2 (checkPointInfo [(i * 3) + 1], checkPointInfo [(i * 3) + 2]);
			_TEMPS [i].position = positions [GetIndexPositionGrid (temp)];
		}
		CheckPointManager.Instance.CreateCheckPoint (_TEMPS);
	}

	int GetIndexPositionGrid (Vector2 temp)
	{
		int index = 0;
		for (int i = 0; i < indexPositions.Count; i++) {
			if (temp == indexPositions [i]) {
				index = i;
			}
		}
		return index;
	}

	int GETIndexPointStart (int indexDirection)
	{
		int indexPointStart = 0;
		switch (indexDirection) {
		case 1:
			indexPointStart = 2;
			break;
		case 2:
			indexPointStart = 0;
			break;
		case 3:
			indexPointStart = 3;
			break;
		case 4:
			indexPointStart = 1;
			break;
		case 5:
			indexPointStart = 0;
			break;
		case 6:
			indexPointStart = 2;
			break;
		case 7:
			indexPointStart = 1;
			break;
		case 8:
			indexPointStart = 3;
			break;
		}
		return indexPointStart;
	}

	#region ONstartGame

	[HideInInspector] public INFO_LEVEL curINFO_LEVEL = new INFO_LEVEL ();

	void Awake ()
	{
		// GET DATA LEVEL
		if (!isTEST) {
			curINFO_LEVEL = ModeLoader.Instance.CurrentINFO_LEVEL;
			textLevel = curINFO_LEVEL.textCurrentLevel;
		}

	}

	void Start ()
	{
		this.ReadLevelTxt ();
		//this.CreateGrid ();
	}

	#endregion

	public void CreateGrid ()
	{
		// Take posCamera
		Vector2 sumTemp = Vector2.zero;
		Vector2 sizeCam = Vector2.zero;
		int count = 0;
		for (int i = 0; i < col; i++) {
			posStart.y = -i * unit;
			for (int j = 0; j < row; j++) {
				posStart.x = j * unit;
				count++;
				sumTemp.x += posStart.x;
				sumTemp.y += posStart.y;
				positions.Add (posStart);
				indexPositions.Add (new Vector2 (j, i));
			}
		}

		sizeCam.x = row * unit;
		sizeCam.y = col * unit;


		Vector3 posCam = new Vector3 (sumTemp.x / count, sumTemp.y / count, -10);
		cameraControl.SETUP (posCam, sizeCam);
		this.CreateBox ();
	}

	public void CreateBox ()
	{
		for (int i = 0; i < numberGround; i++) {
			if (listGridIndex [i] != 0) {
				GameObject boxTemp = Instantiate (ObjectBox (listGridIndex [i]), positions [i], Quaternion.identity) as GameObject;
			}
		}
	}

	public GameObject ObjectBox (int value)
	{
		int index = 0;
		switch (value) {
		case 1:
			index = 13;
			break;
		case 2:
			index = 0;
			break;
		}
		return _ObjectsBox [index];

	}
}
