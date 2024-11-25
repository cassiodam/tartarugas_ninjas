using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    
    public float Speed = 5;
    public float Despawn = -15;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left  * Speed) * Time.deltaTime;
        
        if(transform.position.x < Despawn)
        {
            Debug.Log("Objeto excluido");
            Destroy(gameObject);
        }
    }
}
