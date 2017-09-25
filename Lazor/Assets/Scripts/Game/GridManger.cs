using UnityEngine;
using System.Collections;

public class GridManger : MonoBehaviour {
	public GameObject prefBox;
	public int size;
	public Vector3 localPositionStart;
	public Vector3[] gridPivot;
	public string[] infoPivot;
	public float radius = 1f;
	public string txtFile = "";
	void Awake ()
	{
		int indexPivot = 0;
		infoPivot = new string[size * size];
		TextAsset txtAssets = (TextAsset)Resources.Load ("FileText/" + txtFile);
		string str = txtAssets.text;
		string[] strChil = str.Split ('\n');
		for (int i = (size - 1); i >= 0; i--) {
			char[] tmp = strChil [i].Trim ().ToCharArray ();
			for (int j = 0; j < size; j++) {
				infoPivot [indexPivot] = tmp [j].ToString ();
				indexPivot += 1;
			}
		}
		Vector3 temp = Vector3.zero;

		gridPivot = new Vector3[size * size];

		indexPivot = 0;

		for (int i = 0; i < size; i++) {
			temp.z = localPositionStart.z + (i * radius);
			for (int j = 0; j < size; j++) {
				temp.x = localPositionStart.x + (j * radius);
				gridPivot [indexPivot] = temp;
				if (infoPivot [indexPivot] == "0") {
					GameObject _go = (GameObject)Instantiate (prefBox);
					_go.transform.parent = this.transform;
					_go.transform.localPosition = temp;
				}
				indexPivot += 1;
			}
		}
	}

}
