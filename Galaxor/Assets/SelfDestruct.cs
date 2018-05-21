using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// SelfDestruct (int delay)
public class SelfDestruct : MonoBehaviour
{
    public float delay = 1;

	// Use this for initialization
	void Start ()
    {

        Destroy(this.gameObject, delay);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
