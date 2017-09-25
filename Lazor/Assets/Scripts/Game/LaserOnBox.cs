using UnityEngine;
using System.Collections;
using TungDz;
public class LaserOnBox : MonoBehaviour {
	public GameObject prefLaser;
	public int maxLaser = 0;
	LaserScript[] lasers;
	int countLaser = 0;
	void Start(){
		countLaser = 0;
		lasers = new LaserScript[maxLaser];
		for (int i = 0; i < maxLaser; i++) {
			GameObject laser = Instantiate(prefLaser) as GameObject;
			laser.name = "" + i;
			laser.transform.SetParent (this.transform);
			lasers [i] = laser.GetComponent<LaserScript> ();
			laser.SetActive (false);
		}
		this.RegisterListener (EventID.OnMovedBox, (parma) => _ResetLaser ());
	}
	public void DrawLine(Vector3[] temps){
		if (countLaser == maxLaser)
			return;
		lasers [countLaser].AddPoints (temps);
		lasers [countLaser].gameObject.SetActive (true);
		countLaser += 1;
	}
	public void _ResetLaser(){
		countLaser = 0;
		for (int i = 0; i < maxLaser; i++) {
			lasers [i].gameObject.SetActive (false);
		}
	}
}
