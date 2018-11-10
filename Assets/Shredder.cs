using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

    private float xMin, xMax, yMin, yMax; // horizontal and vertical limits for player movement
    private float padding = 2.0f;

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

        transform.localScale = new Vector3(Mathf.Abs(xMin) + xMax + (padding * 5), padding);
        Vector3 position= new Vector3();
        if (tag == "TopShredder")
        {
            position = new Vector3(0, yMax + padding + transform.localScale.y, transform.position.z);
        }

        else if (tag == "BottomShredder")
        {
            position = new Vector3(0, yMin - padding - transform.localScale.y, transform.position.z);
        }
        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Asteroid asteroid = other.gameObject.GetComponent<Asteroid>();
        if (!asteroid) { Destroy(other.gameObject); }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
