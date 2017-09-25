using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TungDz;
public class UITextManager : MonoBehaviour {

	#region Init, config

	[SerializeField] Text textScore = null;
	void OnValidate(){
		Common.Warning (textScore != null, "UITextManager, missing textScore");
	}
	void Awake(){
		if (textScore == null) {
			DestroyImmediate (this);
		}
	}
	void Start(){
		// register
		this.RegisterListener(EventID.OnAddScore,(param) => OnAddScore());
	}
	#endregion

	#region Event callback
	int _score = 0;
	const string SCORE_TEXT_PREFIX = "SCORE : ";
	void OnAddScore(){
		_score++;
		textScore.text = SCORE_TEXT_PREFIX + _score;
	}
	#endregion
}
