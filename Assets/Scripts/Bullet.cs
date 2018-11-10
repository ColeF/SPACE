using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public ParticleSystem explosionPrefab;

    [Tooltip("Bullet movement speed.")]
    public float speed;

    private LevelManager levelManager;

    private void Update()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        // Move the bullet up
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Asteroid asteroid = collider.gameObject.GetComponent<Asteroid>();
        if (asteroid)
        {
            // Add explosion
            levelManager.AddPoints(10 * (int)asteroid.gameObject.transform.localScale.x);
            if (explosionPrefab) { Instantiate(explosionPrefab, asteroid.transform.position, Quaternion.identity); }
            else { Debug.Log("No explosion prefab assigned to bullet."); }

            Destroy(gameObject);

            asteroid.Respawn();
        }
    }
}
