using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ghostController : MonoBehaviour {
    packmanController p;
   
    // Use this for initialization
    public GameObject packMan;
    public AudioClip death;
    AudioSource audio;
    NavMeshAgent Ghost;
    Animator anim;
    void Start () {
        Ghost = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
        Ghost.speed = 30;

        if (packMan == null)
        {
            packMan = GameObject.FindGameObjectWithTag("Player");
        }
    }
	
	// Update is called once per frame
	void Update () {
        Ghost.destination = packMan.transform.position;
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
             audio.PlayOneShot(death);
        }
    }
}
