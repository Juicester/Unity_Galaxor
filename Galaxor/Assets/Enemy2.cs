using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy2 : Enemy
{
    // offset from off-screen startX by a single sine wave, parameters below
    private float startX;
    private float startY;
    private float targetX;
    private float amplitude;
    private float frequency;

    public override void Start ()
    {
        base.Start();

        // select right or left side of screen to start from
        if (Random.Range(0,100) < 50)
            startX = Main.screenLeft;
        else
            startX = Main.screenRight;

        // calculate weighted average
        // Lerp: linear interpolation. Lerp(a, b, t) = (1-t)*a + t*b
        float percent = 0.1f;
        startY = Mathf.Lerp(Main.screenTop, Main.screenBottom, percent);

        this.transform.position = new Vector3(startX, startY, 0);

        percent = Random.Range(0.2f, 0.8f);
        targetX = Mathf.Lerp(Main.screenLeft, Main.screenRight, percent);

        amplitude = targetX - startX;

        frequency = Random.Range(8.0f, 10.0f);
    }

    public override void Move()
    {
        // calculate offset
        float t = 0.5f * Mathf.PI * localTime / frequency;
        float sine = Mathf.Sin(t);
        float offset = startX + amplitude * sine;

        // create a vector to access and change x coordinate
        Vector3 pos = this.transform.position;
        pos.x = offset;
        this.transform.position = pos;

        // move enemy downwards constantly
        base.Move();

        // animation: spin around z-axis
        this.transform.localRotation = Quaternion.Euler(0, 0, 360 * Mathf.Sin(t));
	}
}
