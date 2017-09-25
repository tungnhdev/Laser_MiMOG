using UnityEngine;
using System.Collections;
using TungDz;
// Làm đàn ông phải giống thằng đàn ông, đừng như thằng đàn bà =))

public class BoxSUM : MonoBehaviour {
	public GameObject _Collider;
	bool isActive = false;

	LayerMask maskCheck;

	void Start(){
		maskCheck = LayerMask.NameToLayer ("CHECKPOINT");
		this.RegisterListener (EventID.OnMovedBox, (parma) => Disable());
	}
	public void Active(){
		if (isActive)
			return;
		this.CheckPointWin ();
		isActive = true;
		_Collider.SetActive (true);
	}
	public void Disable(){
		if (!isActive)
			return;
		isActive = false;
		_Collider.SetActive (false);	
	}
	public void CheckPointWin(){
		// Lam viec de quen e :D
		RaycastHit2D[] hits = Physics2D.BoxCastAll (this.transform.position, new Vector2 (2.58f, 2.58f), 0, Vector2.zero, 1f, 1 << maskCheck);
		if (hits.Length == 0) {
			return;
		}
		foreach (RaycastHit2D hit in hits) {
			if (hit.collider != null) {
				if (hit.collider.tag == "CheckPoint") {
					CheckPointScript temp = hit.collider.GetComponent<CheckPointScript> ();
					temp.OnActive ();
					//CheckPointManager.Instance.AddCheckPoint.Add (temp);
				}
			}
		}

	}
}
