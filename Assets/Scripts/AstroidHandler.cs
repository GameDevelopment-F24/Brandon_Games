using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidHandler : MonoBehaviour
{
    public int size = 5;
    [SerializeField] private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = 0.1f * size * Vector3.one;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float speed = Random.Range(5f - size, 10f - size);
        rb.AddForce(direction*speed,ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Bullet")){
            gameManager = FindAnyObjectByType<GameManager>();
            gameManager.astroidCount--;

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
