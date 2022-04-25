using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float nextXPosition, nextYPosition, newXPosition,newYPosition,yMin,xMin,yMax,xMax,padding = 0.49f;

    [Header("Player")]
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip playerDeathSFX;
    [SerializeField] [Range(0, 1)] float playerDeathVolume = 0.7f;
    [SerializeField] AudioClip playerShootingSFX;
    [SerializeField] [Range(0, 1)] float playerShootingVolume = 0.1f;
    [SerializeField] GameObject level;
    

    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float firingSpeed = 0.1f;
    [SerializeField]GameObject laserPrefab;

    Coroutine firingCoroutine;
    Camera mainCamera;
   
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void SetUpMoveBoundaries()
    {
        mainCamera = Camera.main;
        yMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        xMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        yMax = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
        xMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
    }

    private void Move()
    {
        nextXPosition = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        nextYPosition = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        newXPosition = Mathf.Clamp(transform.position.x + nextXPosition,xMin,xMax);
        newYPosition = Mathf.Clamp(transform.position.y + nextYPosition,yMin,yMax);
        

        transform.position = new Vector2(newXPosition, newYPosition);
        
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
        
    }
    private IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(playerShootingSFX, Camera.main.transform.position, playerShootingVolume);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(firingSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        int damage = damageDealer.GetDamage();
        health -= damage;
        if(health >= 0)
        {
            damageDealer.Hit();
        }
        if(health <= 0)
        {
            Destroy(this.gameObject);
            AudioSource.PlayClipAtPoint(playerDeathSFX, Camera.main.transform.position, playerDeathVolume);
            level.GetComponent<Level>().LoadGameOverScene();
        }
    }
}
