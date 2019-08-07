using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject platformRef;
    float rotationSpeed = 180;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 to = new Vector3(90, 0, 0);
        transform.eulerAngles = to;
        Destroy(gameObject, 15);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            transform.position.Set(transform.position.x, transform.position.y + 1, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("ColiderCoinSpike"))
        {
            Destroy(gameObject);
        }
    }
}
