using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Enemy : Ship // : MonoBehaviour
{
    // units per second
    public float velocityY = 1f;

    // used for emissive color and tinting particle effect and bullets fired
    public Color emissionColor = Color.white;

    // public int health = 2;
    public GameObject prefabExplosion;

    // damaged appearance textures
    private int maxHealth;
    public Texture baseTexture;
    public Texture damageTexture;

    // used to spawn/destroy when object is completely off-screen 
    protected float halfWidth;
    protected float halfHeight;

    // useful for extending classes
    protected float localTime;

    public float fireDelay = 2;

    private GameObject weaponTarget;

    // note: virtual functions can be overridden by extending classes via: public override void FunctionName()

    // Use this for initialization
    public override void Start ()
    {
        base.Start();

        maxHealth = health;

        Mesh mesh = this.GetComponentInChildren<MeshFilter>().mesh;
        Bounds bounds = mesh.bounds;
        halfWidth = bounds.extents.x * this.transform.localScale.x; // use mesh scale?
        halfHeight = bounds.extents.y * this.transform.localScale.y;

        // for this line of code to work,
        //  the emission default must be not-black to enable.
        this.gameObject.GetComponentInChildren<Renderer>().material.SetColor("_EmissionColor", emissionColor);

        localTime = 0;

        weaponTarget = GameObject.Find("Player");

        Invoke("Fire", fireDelay);
    }
	
	// Update is called once per frame
	public override void Update ()
    {
        base.Update();

        localTime += Time.deltaTime;

        Move();

        // set texture depending on damage
        if (1.0 * health / maxHealth <= 0.5)
            this.gameObject.GetComponentInChildren<Renderer>().material.mainTexture = damageTexture;
        else
            this.gameObject.GetComponentInChildren<Renderer>().material.mainTexture = baseTexture;
    }

    // basic downwards movement
    public virtual void Move()
    {
        Vector3 pos = this.transform.position;
        pos.y -= velocityY * Time.deltaTime;
        this.transform.position = pos;
    }

    public override void Fire()
    {
        if (weaponTarget == null)
            return;

        Weapon bullet = Instantiate(prefabWeapon) as Weapon;
        bullet.GetComponentInChildren<Renderer>().material.color = emissionColor;
        bullet.transform.position = this.transform.position;
        bullet.SetTarget(weaponTarget.transform.position);
        
        Invoke("Fire", fireDelay);
    }

    public override void Die()
    {
        GameObject explosion = Instantiate(prefabExplosion) as GameObject;
        explosion.transform.position = this.transform.position;
        explosion.GetComponent<ParticleSystem>().startColor = this.emissionColor;
        Destroy(this.gameObject);
    }
}
