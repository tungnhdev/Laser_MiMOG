using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	Camera cameraMain;
	float ratio = 0;
	float sizeOr = 0;
	float sizeFix = 2.58f;

	void Awake ()
	{
		cameraMain = Camera.main;
		ratio = cameraMain.aspect;
	}

	public void SETUP (Vector3 pivot, Vector2 sizeCam)
	{
		float tempX = sizeCam.x + sizeFix;
		float tempY = tempX * (1.0f / ratio);
		sizeOr = tempY / 2f;
		cameraMain.transform.position = pivot;
		cameraMain.orthographicSize = sizeOr;
	}
}
