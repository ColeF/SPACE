using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    // Inspector Variables
    public Bullet bulletPrefab;
    public ParticleSystem explosion;
    public GameObject shieldPrefab;
    public Text energyText;
    public AudioClip laser, recharge;
    public int lives = 3;
    public float moveSpeed;
    public float padding;
    public float shotDelay;
    public float fireRate;
    public float maxEnergy;
    
    // Private Variables
    private LevelManager levelManager;
    private GameObject shield;
    private AudioSource audioSource;
    private float xMin, xMax, yMin, yMax; // horizontal and vertical limits for player movement
    private float currentEnergy;
    private bool shieldUp;
    private float energyDrain = 66.66f;
    private float energyRegen = 33.33f;
    private bool doubleShot = true;
    public Transform[] guns;

    public float CurrentEnergy
    {
        get { return currentEnergy; }

        set
        {
            if (value > maxEnergy) { currentEnergy = maxEnergy;  }
            else if ( value < 0) { currentEnergy = 0f; }
            else { currentEnergy = value; }
        }
    }

    private void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        audioSource = GetComponent<AudioSource>();

        // Get bounds of the camera
        float zDistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 bottomLeftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zDistance));
        Vector3 topRightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, zDistance));
        xMin = bottomLeftMost.x + padding;
        yMin = bottomLeftMost.y + padding;
        xMax = topRightMost.x - padding;
        yMax = topRightMost.y - padding;

        Vector3 startPosition = new Vector3(0, yMin + padding + 0.5f, transform.position.z);
        transform.position = startPosition;
        CurrentEnergy = maxEnergy;

        //int currentGun = 0;

        //foreach (Transform child in transform)
        //{
        //    if (child.tag == "Gun")
        //    {
        //        guns[currentGun] = child;
        //        currentGun++;
        //    }
        //}
    }

    void Update () {
        // Get player movement distance based on input
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // Move player
        transform.Translate(horizontal, vertical, 0);

        // Clamp position
        Vector3 newPosition = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), 
                                          Mathf.Clamp(transform.position.y, yMin, yMax), 
                                          transform.position.z);
        transform.position = newPosition;

        if (Input.GetButtonDown("Fire1"))
        {
            if (bulletPrefab) { InvokeRepeating("Fire", shotDelay, fireRate); }
            else { Debug.LogError("No bullet prefab assigned to player."); }
        }

        if (Input.GetButtonUp("Fire1")) { CancelInvoke("Fire"); }

        /*************
         *   SHIELD  *
         *************/
        if (Input.GetButtonDown("Shield"))
        {
            shield = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
            shieldUp = true;
            audioSource.clip = recharge;
            audioSource.Play();
        }

        if (Input.GetButtonUp("Shield") || CurrentEnergy <= 0f)
        {
            Debug.Log("shield down");
            Destroy(shield);
            shield = null;
            shieldUp = false;
        }

        if (shieldUp) { CurrentEnergy -= Time.deltaTime * energyDrain; }
        else { CurrentEnergy += Time.deltaTime * energyRegen; }

        energyText.text = CurrentEnergy.ToString("f2");
    }

    private void Fire()
    {
        audioSource.clip = laser;
        audioSource.Play();
        if (doubleShot)
        {
            foreach (Transform gun in guns)
            {
                Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Asteroid asteroid = other.gameObject.GetComponent<Asteroid>();
        if (asteroid)
        {
            if (explosion && !shieldUp) {
                Instantiate(explosion, transform.position, Quaternion.identity);
                lives -= 1;
                if (lives <= 0) { levelManager.LoadNextLevel(); }
                asteroid.Respawn();
                //blink
            }
        }
    }
}
