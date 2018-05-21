using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Ship : MonoBehaviour
{
    public int health = 3;

    // attach a GameObject with a Weapon script attached
    public Weapon prefabWeapon;

    // move fireDelay here too?
    // method in update : if fireReady then { Fire(); fireReady = false; Invoke("CanFireAgain", fireDelay); }
    // CanFireAgain() { fireReady = true; }

	// Use this for initialization
	public virtual void Start ()
    {
	
	}
	
	// Update is called once per frame
	public virtual void Update ()
    {
		if (health <= 0) {
			Die ();
		}
    }

    // abstract methods must be implemented by extending class.

    // instatiate weapon and set target
    public abstract void Fire();

    // destroy ships with no health left
    public abstract void Die();
}
