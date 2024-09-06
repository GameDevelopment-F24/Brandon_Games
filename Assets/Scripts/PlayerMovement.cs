using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float maximumVelocity = 5f;
    public float acceleration = 4f;
    public float rotationSpeed = 180f;
    private bool isAlive = true;
    private bool isAccelerating = false;

    public Transform bulletSpawn;
    public Rigidbody2D bulletPrefab;
    public float bulletSpeed = 10f;

    public Transform flameSpawn; 
    public Rigidbody2D flamePrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive){
            HandleShipAcceleration();
            HandleShipRotation();
            Shoot();
        }
    }
    
    void FixedUpdate() {
        if(isAccelerating && isAlive){
            rb.AddForce(acceleration * transform.up);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maximumVelocity);
            Boost();
        }
    }

    private void HandleShipAcceleration(){
        isAccelerating = Input.GetKey(KeyCode.W);
    }

    private void HandleShipRotation(){
        if(Input.GetKey(KeyCode.D)){
            transform.Rotate(-rotationSpeed * 1.5f * Time.deltaTime * transform.forward);
        }else if(Input.GetKey(KeyCode.A)){
            transform.Rotate(rotationSpeed* Time.deltaTime * 1.5f *transform.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Asteroid")){
            isAlive = false;

            GameManager gameManager = FindAnyObjectByType<GameManager>();

            Destroy(collision.gameObject);
            Destroy(gameObject);

            gameManager.GameOver();
        }
    }

    private void Shoot(){
        if(Input.GetKeyDown(KeyCode.Space)){

            Rigidbody2D bull = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

            Vector2 shipVel = rb.velocity;
            Vector2 shipDir = transform.up;
            float forwardSpeed = Vector2.Dot(shipVel, shipDir);

            if(forwardSpeed < 0){
                forwardSpeed= 0;
            }
            bull.velocity = shipDir * forwardSpeed;
            bull.AddForce(bulletSpeed * transform.up, ForceMode2D.Impulse);
        }
    }

    private void Boost(){
            // I don't know what works or doesn't work so I'll just leave it like this 
            Rigidbody2D flame = Instantiate(flamePrefab, flameSpawn.position, Quaternion.identity);
            flame.velocity = transform.up;
            flame.AddForce(transform.up, ForceMode2D.Impulse);
            flame.position = transform.position;
    }
}
