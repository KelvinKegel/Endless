using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    Vector3 direction;
    [SerializeField]
    float velocity;

    void Start()
    {
        
    }

    void Update()
    {
        running();
    }

    void running()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        velocity = speed * Time.deltaTime;
        transform.Translate(direction * velocity);
    }
}
