using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // Prefab do item a ser instanciado
    public GameObject itemPrefab;

    // Tempo entre os spawns dos itens
    public float spawnInterval = 2;

    // Temporizador para controlar o intervalo entre os spawns
    private float timer = 0;

    // Altura mínima e máxima para o spawn, ajustáveis no Inspector
    public float minHeight = -4;
    public float maxHeight = -0.6f;

    void Start()
    {
        // Spawn inicial do item
        ItemSpawn();
    }

    void Update()
    {
        // Incrementa o temporizador conforme o tempo passa
        if (timer < spawnInterval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            // Spawna um novo item e reinicia o temporizador
            ItemSpawn();
        }
    }

    // Função responsável por instanciar o item em uma posição aleatória dentro da altura permitida
    void ItemSpawn()
    {
        // Calcula uma posição aleatória dentro dos limites de altura
        float spawnHeight = Random.Range(minHeight, maxHeight);

        // Instancia o item no local calculado
        Instantiate(itemPrefab, new Vector3(transform.position.x, spawnHeight, 0), transform.rotation);

        // Reseta o temporizador
        timer = 0;
    }
}
