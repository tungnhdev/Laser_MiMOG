using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class LaserScript : MonoBehaviour
{
	public LineRenderer lineRenderer;
	LayerMask maskCheckPoint;

	void Awake ()
	{
		maskCheckPoint = LayerMask.NameToLayer ("CHECKPOINT");
		lineRenderer.sortingLayerName = "Laser";
		lineRenderer.sortingOrder = 1;
	}


	public void AddPoints (Vector3[] positions)
	{


		Vector3[] temps = new Vector3[positions.Length];
		for (int i = 0; i < positions.Length; i++) {
			temps [i] = this.transform.InverseTransformPoint (positions [i]);
		}
		lineRenderer.SetVertexCount (temps.Length);
		lineRenderer.SetPositions (temps);

		// Check Pointsf
		RaycastHit2D[] hits = Physics2D.LinecastAll (positions [0], positions [1], 1 << maskCheckPoint);
		Debug.DrawLine (positions [0], positions [1], Color.blue);
		if (hits.Length == 0)
			return;
		foreach (RaycastHit2D hit in hits) {
			if (hit.collider != null) {
				if (hit.collider.tag == "CheckPoint") {
					CheckPointScript temp = hit.collider.GetComponent<CheckPointScript> ();
					temp.OnActive ();
					CheckPointManager.Instance.listCheckPoint.Add (temp);
				}
			}
		}

	}


}
