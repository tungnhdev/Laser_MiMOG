using UnityEngine;
using System.Collections;

public class EffectScript : MonoBehaviour
{
	ParticleSystem pars;

	void OnEnable ()
	{
		if (pars == null)
			pars = this.GetComponent<ParticleSystem> ();
	}

	void OnDisable ()
	{
		pars.Clear ();
	}

	void Update ()
	{
		if (pars.isStopped) {
			gameObject.SetActive (false);
		}
	}
}
