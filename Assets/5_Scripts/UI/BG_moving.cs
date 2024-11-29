using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_moving : MonoBehaviour
{
    Material mat;
    float distancia;

    [Range(0f,0.5f)]
    public float velocidade = 5;


    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        distancia += Time.deltaTime*velocidade;
        mat.SetTextureOffset("_MainTex", Vector2.right * distancia);
    }
}
