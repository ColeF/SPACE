using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public ParticleSystem explosionPrefab;

    private void OnTriggerEnter(Collider other)
    {
        Asteroid asteroid = other.gameObject.GetComponent<Asteroid>();
        if (asteroid)
        {
            if (explosionPrefab) { Instantiate(explosionPrefab, transform.position, Quaternion.identity); }
            asteroid.Respawn();
        }
    }
}
