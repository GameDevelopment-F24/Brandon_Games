using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapEffect : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        Vector3 moverAmt = Vector3.zero;
        if(viewportPosition.x < 0){
            moverAmt.x += 1;
        } else if(viewportPosition.x > 1){
            moverAmt.x -=1;
        } else if(viewportPosition.y < 0){
            moverAmt.y += 1;
        } else if(viewportPosition.y > 1){
            moverAmt.y -= 1;
        }

        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition + moverAmt);
    }
}
