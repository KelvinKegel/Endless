using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;
    [SerializeField]
    Vector3 direction;
    [SerializeField]
    float velocity;
    [SerializeField]
    GameObject mesh;

    void Start()
    {
        
    }

    void Update()
    {
        running();

        if (Input.GetKeyDown(KeyCode.W))
        {
            teleport();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            rasteirinhaPerigosa();
        }

        mesh.transform.position = transform.position;

/*         {
            // If there are two touches on the device...
            if (Input.touchCount == 2)
            {
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            }
        }
*/
    }

    void running()
    {
        direction = Vector3.right;
        velocity = speed * Time.deltaTime;
        transform.parent.Translate(direction * velocity);
    }

    void teleport()
    {
        if(Collision.gameObject.CompareTag("Ground"))
        {
            transform.parent.Translate(0, 10, 0);
        }
        
    }

    void rasteirinhaPerigosa()
    {
        mesh.transform.localEulerAngles = new Vector3(0, 0, 60);

    }
}
