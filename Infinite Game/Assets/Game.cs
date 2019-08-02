using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabsCoinSpawn = new GameObject[0];

    [SerializeField]
    private GameObject[] prefabsPlataform;

    [SerializeField]
    int maxPool;

    [SerializeField]
    Vector3 nextPosition;

    Queue <GameObject> fila = new Queue<GameObject>();

    [SerializeField]
    Material[] materials;

    Player playerRef;

    [SerializeField]
    float distanceRespawn = 10;

    void Start()
    {
        if(!playerRef || playerRef == null)
        {
            playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        for (int i = 0; i < maxPool; i++)
        {
            spawnPlataform();
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (checkRespawn())
        {
            respawnPlataform();
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

    public void coinSpawn()
    {
        int coinQuantity = 0;

        for (int i = 0; i < 3; i++)
        {

        }

    }

    public void respawnPlataform()
    {
        GameObject plataforma = fila.Dequeue();
        Vector3 newscale = new Vector3(Random.Range(20f, 50f), Random.Range(10f, 15f), 5);
        plataforma.transform.localScale = newscale;
        Vector3 newPosition = nextPosition;
        newPosition.y += Random.Range(-10, 10);
        newPosition.x += plataforma.transform.localScale.x / 2;
        plataforma.transform.position = newPosition;
        nextPosition.x += plataforma.transform.localScale.x;

        plataforma.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];

        fila.Enqueue(plataforma);
    }

    public void spawnPlataform()
    {
        if (fila.Count >= maxPool) return;
        GameObject plataforma = Instantiate(prefabsPlataform[0], nextPosition, Quaternion.identity);
        Vector3 newscale = new Vector3(Random.Range(20f, 50f), Random.Range(10f, 15f), 5);
        plataforma.transform.localScale = newscale;
        Vector3 newPosition = nextPosition;
        newPosition.y += Random.Range(-10, 10);
        newPosition.x+= plataforma.transform.localScale.x / 2;
        plataforma.transform.position = newPosition;
        nextPosition.x += plataforma.transform.localScale.x;

        plataforma.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];

        fila.Enqueue(plataforma);
    }

}
