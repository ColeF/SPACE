using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public float padding;
    public float fallSpeed;

    private float xMin, xMax, yMin, yMax; // horizontal and vertical limits for player movement

    private void Start()
    {
        // Get bounds of the camera
        float zDistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 bottomLeftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zDistance));
        Vector3 topRightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, zDistance));
        xMin = bottomLeftMost.x + padding;
        yMin = bottomLeftMost.y + padding;
        xMax = topRightMost.x - padding;
        yMax = topRightMost.y - padding;
        Respawn();
    }

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        if (transform.position.y <= yMin - padding)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        float newX = Random.Range(xMin, xMax);
        float newY = yMax + 1f;
        Vector3 position = new Vector3(newX, newY, transform.position.z);
        transform.position = position;
        float randomScale = Random.Range(1f, 3f);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }

}
