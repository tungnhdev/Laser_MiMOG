using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LASERINFO
{
	public Vector3 position;
	public int indexStartPoint;
	public int indexDirection;
}

public class LaserControlManager : MonoBehaviour
{
	public static LaserControlManager Instance;
	[Header ("LASER MAIN")]
	public LaserControl[] Laserfirst;
	public GameObject laserControlPref;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}

	public List<LaserControl> Lasers;

	public void ResetAllLasers ()
	{
		this.ClearAllLasers ();

		for (int i = 0; i < Laserfirst.Length; i++) {
			Laserfirst [i].ResetLaser ();
		}
	}

	public void ClearAllLasers ()
	{
		for (int i = 0; i < Lasers.Count; i++) {
			Lasers [i].Clear ();
		}
		Lasers.Clear ();
	}

	public void CreateNewLaser (int direction, Vector2 pivot, Transform box)
	{
		Laserfirst [0].CreateLaserNew (direction, pivot, box);
	}
	/// <summary>
	/// Creates the laser control.
	/// </summary>
	/// <param name="TEMPS">TEMP.</param>
	public void CreateLaserControl (LASERINFO[] TEMPS)
	{
		Laserfirst = new LaserControl[TEMPS.Length];
		for (int i = 0; i < TEMPS.Length; i++) {
			GameObject LASER = Instantiate (laserControlPref) as GameObject;
			LASER.name = "Laser main +";
			Laserfirst [i] = LASER.GetComponent<LaserControl> ();
			Laserfirst [i].SetOnDraw (TEMPS [i]);
		}
	}
}
