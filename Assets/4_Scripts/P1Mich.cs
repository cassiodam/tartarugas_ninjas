using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michelangelo
public class P1Mich : MonoBehaviour

{

    public Rigidbody2D P1Rigidbody;
    public float JumpStrength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
        P1Rigidbody.velocity = Vector2.up * JumpStrength;
        }
    }
}
