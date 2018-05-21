using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// rename: PlayerWeapon
public class Weapon : MonoBehaviour
{
    public float speed = 4;

    public GameObject prefabSparks;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void SetTarget(Vector3 target)
    {
        Vector3 direction = target - this.transform.position;
        direction.Normalize();
        this.GetComponent<Rigidbody>().velocity = direction * speed;
    }

    public void OnTriggerEnter(Collider other)
    {
        Ship ship = other.gameObject.GetComponentInParent<Ship>();

        if (ship != null)
        {
            ship.health -= 1;

            GameObject spark = Instantiate(prefabSparks) as GameObject;
            Vector3 sparkPosition = this.transform.position;
            spark.GetComponent<ParticleSystem>().startColor = this.GetComponent<Renderer>().material.color;
            spark.transform.position = sparkPosition;
            spark.transform.parent = other.transform;

            Destroy(this.gameObject);
        }
    }
    

}
