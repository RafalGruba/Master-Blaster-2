using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Shooting")]
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject port;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    float shotCounter;

    [Header("Sound Effects")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;
    private SpriteRenderer sr;
    public bool isBossAlive = true;
    int playerHealth;

        // Start is called before the first frame update
        void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
        playerHealth = FindObjectOfType<Player>().GetHealth();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }


    private void Fire()
    {
        if (isBossAlive == true)
        {
            GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f), -projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);

        void ProcessHit(DamageDealer DamageDealer)
        {
            health -= damageDealer.GetDamage();
            DamageDealer.Hit();
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        GameObject portal = Instantiate(port, new Vector2(0, 13), Quaternion.identity) as GameObject;
        isBossAlive = false;
        FindObjectOfType<GameSession>().AddToScore(scoreValue * (playerHealth / 100));
        sr.enabled = false;
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, 10f);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
}
