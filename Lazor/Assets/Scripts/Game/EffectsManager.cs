using UnityEngine;
using System.Collections;

public class EffectsManager : MonoBehaviour
{
	public static EffectsManager Instance;

	// Effect laser Hit
	[SerializeField] CreateObjectPooling EffectLaserHit;


	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}


	public void PlayEffectLaserHit (Vector3 position)
	{
		EffectLaserHit._ChooseEffect (position);
	}
}
