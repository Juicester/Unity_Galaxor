using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// alternative approach could involve: void OnBecameInvisible() { Destroy(this.gameObject); }
// however, it fails in the Unity editor because the Scene camera sees all objects

// requires screen bounds calculated by main class.
// assumes camera looks along z-axis, x points right, y points up.

public class DestroyOutsideScene : MonoBehaviour
{
    private float halfWidth;
    private float halfHeight;

    // objects are not destroyed until they have been offscreen for the given delay time
    // use case: objects that spawn off-screen and immediately move on-screen should not be destroyed right away

    public float delay = 0.5f;
    private float timer;

    // Use this for initialization
    void Start()
    {

        // calculate bounds for GameObjects with child meshes attached
        Bounds bounds = new Bounds(this.transform.position, Vector3.zero);
        foreach (Renderer childRenderer in this.GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(childRenderer.bounds);
        }
        halfWidth = bounds.extents.x;
        halfHeight = bounds.extents.y;

        timer = 0;
    }

    bool OffScreen()
    {
        float leftEdge = this.transform.position.x - halfWidth;
        float rightEdge = this.transform.position.x + halfWidth;
        float topEdge = this.transform.position.y + halfHeight;
        float bottomEdge = this.transform.position.y - halfHeight;

        return (topEdge < Main.screenBottom || bottomEdge > Main.screenTop || leftEdge > Main.screenRight || rightEdge < Main.screenLeft);
    }

    // Update is called once per frame
    void Update()
    {
        if (OffScreen())
            timer += Time.deltaTime;
        else
            timer = 0;
            
        if (timer > delay)
            Destroy(this.gameObject);
    }
}
