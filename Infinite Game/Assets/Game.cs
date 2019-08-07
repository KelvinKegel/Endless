﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class Game : MonoBehaviour
{
    float xPos;
    float yPos;

    public int coinCount = 0;
    //public int HPCount = 5;

    public TextMeshProUGUI textoMoedas;
    //public TextMeshProUGUI textoHP;

    [SerializeField]
    private GameObject[] prefabsCoinSpawn = new GameObject[2];

    [SerializeField]
    private GameObject[] prefabsSpikeSpawn = new GameObject[2];

    [SerializeField]
    private GameObject[] prefabsPlataform;

    [SerializeField]
    int maxPool;

    [SerializeField]
    Vector3 nextPosition;

    Queue <GameObject> fila = new Queue<GameObject>();

    [SerializeField]
    Material[] materials;

    [SerializeField]
    Player playerRef;

    [SerializeField]
    float distanceRespawn = 20;

    float timerStartCoin;
    [SerializeField]
    float timerMaxCoin;

    void Start()
    {

        if (!playerRef || playerRef == null)
        {
            playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        for (int i = 0; i < maxPool; i++)
        {
            spawnPlataform(Random.Range(20,30), Random.Range(14,20));
        }
    }
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        textoMoedas.text = "Coins: " + coinCount;
        //textoHP.text = "Hp: " + HPCount;

        if (Time.time >= timerStartCoin + timerMaxCoin)
        {
            timerStartCoin = Time.time;
            //spikeSpawn(1);
            coinSpawn(2, spikeSpawn(1));

        }
        if (checkRespawn())
        {
            respawnPlataform(Random.Range(20, 30), Random.Range(14, 25));
        }
    }

    bool checkRespawn()
    {
        if (fila.Peek().transform.position.x < playerRef.transform.position.x
            &&
            Vector3.Distance(fila.Peek().transform.position, playerRef.transform.position) >= distanceRespawn)
            return true;
        else
        {
            return false;
        }
    }



    public void respawnPlataform(float scaleX, float scaleY)
    {

        GameObject plataforma = fila.Dequeue();
        float posY = Random.Range(-10, playerRef.getjumpHeight() - scaleY );
        Vector3 newscale = new Vector3(scaleX +5, scaleY, 5);
        plataforma.transform.localScale = newscale;
        Vector3 newPosition = nextPosition;
        newPosition.y += posY;
        newPosition.x += plataforma.transform.localScale.x / 2;
        plataforma.transform.position = newPosition;
        nextPosition.x += plataforma.transform.localScale.x;

        plataforma.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];

        fila.Enqueue(plataforma);
    }

    public void spawnPlataform(float scaleX, float scaleY)
    {
        if (fila.Count >= maxPool) return;
        GameObject plataforma = Instantiate(prefabsPlataform[0], nextPosition, Quaternion.identity);
        Vector3 newscale = new Vector3(scaleX, scaleY, 5);
        plataforma.transform.localScale = newscale;
        Vector3 newPosition = nextPosition;
        newPosition.y += Random.Range(-10, 10);
        newPosition.x+= plataforma.transform.localScale.x / 2;
        plataforma.transform.position = newPosition;
        nextPosition.x += plataforma.transform.localScale.x;

        plataforma.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];

        fila.Enqueue(plataforma);
    }

    public GameObject spikeSpawn(int quantidade)
    {
        GameObject g = new GameObject();   
        for(int i = 0; i < quantidade; i++)
        {
            int plataformaSelec = Random.Range(4, fila.Count);

            xPos = 0;

            yPos = 4 + fila.ElementAt(plataformaSelec).transform.localScale.y / 2f;

            g = Instantiate(prefabsSpikeSpawn[Random.Range(0, prefabsSpikeSpawn.Length)], 
                new Vector3((fila.ElementAt(plataformaSelec).transform.position.x + xPos), 
                (fila.ElementAt(plataformaSelec).transform.position.y + yPos), 0), Quaternion.identity);
        }
        return g;

    }

    public void coinSpawn(int quantidade, GameObject reference)
    {
        //int coinQuantity = 0;

        for (int i = 0; i < quantidade; i++)
        {
            int plataformaSelec = Random.Range(4, fila.Count);

            xPos = Random.Range(-3, 6) + reference.transform.localScale.x / 1f;

            yPos = Random.Range(1f, 5f) + reference.transform.localScale.y /1f;

            Instantiate(prefabsCoinSpawn[Random.Range(0, prefabsCoinSpawn.Length)],
                new Vector3((reference.transform.position.x + xPos),
                (reference.transform.position.y + yPos), playerRef.transform.position.z), Quaternion.identity);
            //coinQuantity += 1;
        }

    }
}
