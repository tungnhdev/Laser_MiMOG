using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TungDz;
[System.Serializable]
public class InfoG{
	public int index = 0;
	public int indexContact = 0;
	public int indexDirectionLaserNew  = 0;
	public int indexLaserOnBoxNew = 0;
}
public class BoxGScript : MonoBehaviour {

	public BoxGScript boxGContact;
	public GameObject[] _lasersOnBox;
	List<GameObject> listLaser = new List<GameObject>();
	ReturnHit returnHit;
	List<int> indexsActive = new List<int>();

	void Start(){

		returnHit = GetComponent<ReturnHit> ();
		this.RegisterListener (EventID.OnMovedBox,(parma) => DisableLaser());

	}
	public void Active(int directionLaser,int indexContact){
		InfoG temp = getIndex (directionLaser,indexContact);
		_lasersOnBox [temp.index].SetActive (true);
		listLaser.Add (_lasersOnBox[temp.index]);
		_lasersOnBox [8].SetActive (true);
		//
		boxGContact.OnContact (temp);
	}
	// Tra ve Index cua temp
	public InfoG getIndex(int directionLaser,int indexContact){

//		print (indexContact+"  "+ directionLaser);

		InfoG temp = new InfoG ();

		switch (indexContact) {
		case 0:
			temp.indexContact = 2;
			if (directionLaser == 3 || directionLaser == 2) {
				// 0-2
				temp.index = 1;
				temp.indexDirectionLaserNew = 2;

				temp.indexLaserOnBoxNew = 5;


			} else if (directionLaser == 4 || directionLaser == 5) {
				// 0-1
				temp.index = 0;
				temp.indexDirectionLaserNew = 5;

				temp.indexLaserOnBoxNew = 4;
			}
			return temp;
		case 1:
			temp.indexContact = 3;
			if(directionLaser == 5 || directionLaser == 4){
				//1- 1
				temp.index = 3;
				temp.indexDirectionLaserNew = 4;

				temp.indexLaserOnBoxNew = 6;

			}
			else if(directionLaser == 6 || directionLaser == 7){
				//1 -2
				temp.index = 2;
				temp.indexDirectionLaserNew = 7;

				temp.indexLaserOnBoxNew = 7;
			}

			return temp;
		case 2:
			temp.indexContact = 0;
			if(directionLaser == 7 || directionLaser == 6){
				//2 - 1

				temp.index = 5;
				temp.indexDirectionLaserNew = 6;

				temp.indexLaserOnBoxNew = 1;

			}
			else if(directionLaser == 8 || directionLaser == 1){
				//2 -2
				temp.index = 4;
				temp.indexDirectionLaserNew = 1;

				temp.indexLaserOnBoxNew = 0;
			}

			return temp;
		case 3:
			temp.indexContact = 1;
			if(directionLaser == 1 || directionLaser == 8){
				//3-1
				temp.index = 7;
				temp.indexDirectionLaserNew = 8;


				temp.indexLaserOnBoxNew = 3;
			}
			else if(directionLaser == 2 || directionLaser == 3){
				//3 -2
				temp.index = 6;
				temp.indexDirectionLaserNew = 3 ;


				temp.indexLaserOnBoxNew = 2;
			}
			return temp;
		default:
			return temp;
		}
	}


	// On Disable
	public void DisableLaser(){

		indexsActive.Clear ();

		if (listLaser.Count == 0)
			return;
		foreach (GameObject temp in listLaser) {
			temp.SetActive (false);
		}
		listLaser.Clear ();
		_lasersOnBox [8].SetActive (false);
	}
	// Call from Contact
	public void OnContact(InfoG temps){
		foreach (int i in indexsActive) {
			if (i == temps.indexContact) {
				return;
			}
		}
		indexsActive.Add (temps.indexContact);

		_lasersOnBox [temps.indexLaserOnBoxNew].SetActive (true);
		listLaser.Add (_lasersOnBox[temps.indexLaserOnBoxNew]);
		_lasersOnBox [8].SetActive (true);
		// Create New Laser
		LaserControlManager.Instance.CreateNewLaser (temps.indexDirectionLaserNew, returnHit.ContactPOS [temps.indexContact], this.transform);
	}
}
