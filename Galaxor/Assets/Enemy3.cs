using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy3 : Enemy
{
    private Vector3 previousPoint;
    private Vector3 targetPoint;
    private float tripDuration;

    public void ComputeNewPath()
    {
        previousPoint = this.transform.position;

        float percent, targetX, targetY;

        percent = Random.Range(0.15f, 0.85f);
        targetX = Mathf.Lerp(Main.screenLeft, Main.screenRight, percent);

        percent = Random.Range(0.10f, 0.60f);
        targetY = Mathf.Lerp(Main.screenTop, Main.screenBottom, percent);

        targetPoint = new Vector3(targetX, targetY, 0);

        tripDuration = (targetPoint - previousPoint).magnitude / 2; // speed up
    }

    public override void Start ()
    {
        base.Start();
        ComputeNewPath();
    }
	
	// Update is called once per frame
	public override void Move()
    {
        float easedTime = 0.5f - 0.5f * Mathf.Cos(localTime * Mathf.PI / tripDuration);


        Vector3 lerpPosition = Vector3.Lerp(previousPoint, targetPoint, easedTime);
        this.transform.position = lerpPosition;

        if (localTime >= tripDuration)
        {
            localTime = 0;
            ComputeNewPath();
        }

        // no constant downwards movement -- do not call base.Move()

        // animation: spin like a top (requires pre-existing tilt off of z-axis)
        this.transform.localRotation = Quaternion.Euler(0, 0, 2) * this.transform.localRotation;
	}
}
