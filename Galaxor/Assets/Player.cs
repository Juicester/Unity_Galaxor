using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : Ship // : MonoBehavior
{
    public float speed = 6;   // units/sec
    public GameObject shipModel;
    public float rollMult = -30;
    public float pitchMult = 20;

    private float halfHeight;

	public Text healthText;
	public Text gameOverText;

    // Use this for initialization
    public override void Start ()
    {
        base.Start();

        // this should probably be a static method in a Utils class
        Bounds bounds = new Bounds(this.transform.position, Vector3.zero);
        foreach (Renderer childRenderer in this.GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(childRenderer.bounds);
        }
        halfHeight = bounds.extents.y;

		gameOverText.enabled = false;
    }

    // Update is called once per frame
    public override void Update ()
    {
        base.Update();

        // get values from Input class.
        // View default input settings via: Edit > Project Settings > Input
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 playerPosition = this.transform.position;
        playerPosition.x += xAxis * speed * Time.deltaTime;
        playerPosition.y += yAxis * speed * Time.deltaTime;
        this.transform.position = playerPosition;

        // rotate the shipModel, but not the whole object.
        shipModel.transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.05f, 0.95f);
        pos.y = Mathf.Clamp(pos.y, 0.05f, 0.95f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        if (Input.GetKeyDown(KeyCode.Space))
            Fire();

		if (this.health == 2) {
			healthText.text = "Health: 66";
		} else if (this.health == 1) {
			healthText.text = "Health: 33";
		} else if (this.health == 0) {
			healthText.text = "Health: 00";
			gameOverText.enabled = true;
		}
    }

    public override void Fire()
    {
        Weapon bullet = Instantiate(prefabWeapon) as Weapon;
        bullet.transform.position = this.transform.position + new Vector3(0, halfHeight, 0);
        bullet.SetTarget(this.transform.position + new Vector3(0, 2 * halfHeight, 0));
    }

    public override void Die()
    {
        Destroy(this.gameObject);
    }
}
