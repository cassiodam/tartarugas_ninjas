using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimneySpawner : MonoBehaviour
{
    public GameObject chimney;
    public float TempodSpawn = 2;
    private float timer = 0;
    public float alturaMedia = 4;
    void Start()
    {
        SpawnChimney();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < TempodSpawn)
        {
             timer = timer + Time.deltaTime;
        }
        else
        {
        SpawnChimney();
        }
        
    }

    void SpawnChimney()
    {
        float menorAltura = Mathf.Max(transform.position.y - alturaMedia, -4);
        float maiorAltura = Mathf.Min(transform.position.y + alturaMedia, -0.6f);

         Instantiate(chimney, new Vector3(transform.position.x, Random.Range(menorAltura, maiorAltura), 0), transform.rotation);
         timer = 0;
    }
}
