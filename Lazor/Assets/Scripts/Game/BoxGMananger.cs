using UnityEngine;
using System.Collections;
using TungDz;
public class BoxGMananger : MonoBehaviour {
	static BoxGMananger s_instance;
	public static BoxGMananger Instance
	{
		get
		{
			// instance not exist, then create new one
			if (s_instance == null)
			{
				// create new Gameobject, and add EventDispatcher component
				GameObject singletonObject = new GameObject();
				s_instance = singletonObject.AddComponent<BoxGMananger>();
				singletonObject.name = "Singleton - BoxGManager";
				Common.Log("Create singleton : {0}", singletonObject.name);
			}
			return s_instance;
		}
		private set { }
	}
	public static bool HasInstance()
	{
		return s_instance != null;
	}

	void Awake ()
	{
		// check if there's another instance already exist in scene
		if (s_instance != null && s_instance.GetInstanceID() != this.GetInstanceID())
		{
			// Destroy this instances because already exist the singleton of EventsDispatcer
			Destroy(gameObject);
		}
		else
		{
			// set instance
			s_instance = this as BoxGMananger;
		}
	}
}
