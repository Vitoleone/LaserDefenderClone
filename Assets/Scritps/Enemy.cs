using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laser;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject explosionVFX;
    float randomTimeBetweenShots;
    [SerializeField] AudioClip enemyDeathSFX;
    [SerializeField] [Range(0, 1)] float enemyDeathVolume = 0.7f;
    [SerializeField] AudioClip enemyShootingSFX;
    [SerializeField] [Range(0, 1)] float enemyShootingVolume = 0.5f;
    [SerializeField] int scoreValue = 150;

    private void Start()
    {

        randomTimeBetweenShots = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        
        
            CountDownAndShoot();
        
        
    }

    void CountDownAndShoot()
    {
        randomTimeBetweenShots -= Time.deltaTime;
        if(randomTimeBetweenShots <= 0f)
        {
            Fire();
            randomTimeBetweenShots = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    void Fire()
    {
       
        laser = Instantiate(laser, this.transform.position,Quaternion.identity);
        AudioSource.PlayClipAtPoint(enemyShootingSFX, Camera.main.transform.position, enemyShootingVolume);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-projectileSpeed);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();
        if (health >= 0)
        {
            damageDealer.Hit();
        }
        if (health <= 0)
        {
            Die();
        }
           

    }

    void Die()
    {      
            FindObjectOfType<GameSession>().AddToScore(scoreValue);
            GameObject explosion = Instantiate(explosionVFX, gameObject.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
            AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position, enemyDeathVolume);
            Destroy(explosion, 0.5f);
    }
    
}
