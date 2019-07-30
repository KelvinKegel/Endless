using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    void Start()
    {
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        //sua bunda() = comi o cu de quem ta lendo
    }

    public void loadGameScene()
    {
        print("ansdhbdvsf");
        SceneManager.LoadScene("Game");
    }
}
