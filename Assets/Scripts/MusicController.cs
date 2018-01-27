using UnityEngine;

public class MusicController : MonoBehaviour {
	
	private static MusicController instance = null;
	
	public static MusicController Instance {
		get { return instance; }
	}
	
	void Awake() 
	{
		if (instance != null && instance != this) 
		{
			if(instance.GetComponent<AudioSource>().clip != GetComponent<AudioSource>().clip)
			{
				instance.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;
				instance.GetComponent<AudioSource>().volume = GetComponent<AudioSource>().volume;
				instance.GetComponent<AudioSource>().Play();
			}
			
			Destroy(this.gameObject);
			return;
		} 
		instance = this;
		GetComponent<AudioSource>().Play ();
		DontDestroyOnLoad(this.gameObject);
	}
}
