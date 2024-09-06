using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float lifetime = 2f;
    void Start(){
        Despawn();
    }

    private void Despawn(){
        Destroy(gameObject, lifetime);
    }
}
