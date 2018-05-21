using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject[] prefabEnemies;

    public static float screenTop;
    public static float screenBottom;
    public static float screenLeft;
    public static float screenRight;

    // Use this for initialization
    void Start()
    {
        Vector3 viewportBottomLeft = new Vector3(0, 0, -Camera.main.transform.position.z);
        Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(viewportBottomLeft);
        screenBottom = screenBottomLeft.y;
        screenLeft = screenBottomLeft.x;

        Vector3 viewportTopRight = new Vector3(1, 1, -Camera.main.transform.position.z);
        Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(viewportTopRight);
        screenTop = screenTopRight.y;
        screenRight = screenTopRight.x;

        Invoke("SpawnEnemy", 2.0f);
    }

    public void SpawnEnemy()
    {
        int index = Random.Range(0, prefabEnemies.Length);
        GameObject enemy = Instantiate(prefabEnemies[index]) as GameObject;

        // note: mesh.bounds returns local bounds (not affected by transform)
        // while renderer.bounds returns world bounds (affected by transform)

        // calculate bounds for GameObjects with child meshes attached
        Bounds enemyBounds = new Bounds(enemy.transform.position, Vector3.zero);
        foreach (Renderer childRenderer in enemy.GetComponentsInChildren<Renderer>())
        {
            enemyBounds.Encapsulate(childRenderer.bounds);
        }
        
        float enemyHalfHeight = enemyBounds.extents.y * enemy.transform.localScale.y;
        float enemyX = Random.Range(screenLeft * 0.6f, screenRight * 0.6f);
        float enemyY = screenTop + enemyHalfHeight;
        enemy.transform.position = new Vector3(enemyX, enemyY, 0);

        Invoke("SpawnEnemy", 2.0f); // placing here in case time to spawn changes over time (decreases for difficulty ramp)
    }

    void Awake ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
