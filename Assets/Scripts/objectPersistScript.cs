using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Musical credit goes to PettertheSturgeon (https://soundcloud.com/petterthesturgeon)

public class objectPersistScript : MonoBehaviour {

    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;

            AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load("Dear Diary") as AudioClip;
            audioSource.loop = true;
            audioSource.Play();
            //Debug.Log("Awake: " + this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
