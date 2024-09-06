using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameEffect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float lifetime = 0.1f;
    public float size = 1;

    void Start()
    {
        Despawn();
    }

    private void Despawn(){
        Destroy(gameObject, lifetime);
    }
}
