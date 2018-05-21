using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy1 : Enemy
{
    // offset from mid-screen startX by a repeated sine wave, parameters below
    private float startX;
    private float frequency;
    private float amplitude;

    private float tiltAngle = 30.0f;

    public override void Start()
    {
        base.Start();

        localTime = 0;

        startX = this.transform.position.x;
        frequency = Random.Range(1.5f, 2.5f);
        amplitude = Random.Range(0.5f, 1.0f);
    }

    // Update (from base class) is automatically is called once per frame
    // Update calls Move, which we add extra code to, by overriding here and calling the base class

    public override void Move()
    {
        // calculate offset
        float t = 2 * Mathf.PI * localTime / frequency;
        float sine = Mathf.Sin(t);
        float offset = startX + amplitude * sine;

        // create a vector to access and change x coordinate
        Vector3 pos = this.transform.position;
        pos.x = offset;
        this.transform.position = pos;

        // move enemy downwards constantly
        base.Move();

        // animate: tilt from side to side
        this.transform.localRotation = Quaternion.Euler(0, tiltAngle * sine, 0);
	}
}
