using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Game game_ref;

    [SerializeField]
    float downSpeed;
    [SerializeField]
    float speed = 15f;
    [SerializeField]
    Vector3 direction;
    [SerializeField]
    float velocity;
    [SerializeField]
    GameObject mesh;
    float jumpHeight = 15f;

    [SerializeField]
    bool isGrounded;

    [SerializeField]
    bool isDead = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (!isDead)
        {
            running();

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.W))
            {
                teleport();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                rasteirinhaPerigosa();
                StartCoroutine(CooldownRasteira());
            }
#endif

#if UNITY_ANDROID
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
#endif
        }

        mesh.transform.position = transform.position;
         
    }

    void running()
    {
        direction = Vector3.right;
        velocity = speed * Time.deltaTime;
        transform.parent.Translate(direction * velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.transform.CompareTag("Ground")) == true)
        {
            isGrounded = true;
        }
        if (collision.transform.CompareTag("SpikePoint"))
        {
            isDead = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if ((collision.transform.CompareTag("Ground")) == true)
        {
            isGrounded = false;
        }
    }

    void teleport()
    {
        if(isGrounded)
        {
            transform.parent.Translate(0, jumpHeight, 0);
        }
        
    }

    public float getjumpHeight()
    {
        return jumpHeight;
    }

    IEnumerator CooldownRasteira()
    {
        yield return new WaitForSeconds(1.5f);
        mesh.transform.localEulerAngles = new Vector3(0, 0, 0);
        GetComponent<BoxCollider>().size = Vector3.one ;
    }

    void rasteirinhaPerigosa()
    {
        if(isGrounded == false)
        {
            // GetComponent<Rigidbody>().AddForce(Vector3.down * downSpeed);
            GetComponent<Rigidbody>().velocity += Vector3.down * downSpeed;
        }
        mesh.transform.localEulerAngles = new Vector3(0, 0, 60);
        BoxCollider box = GetComponent<BoxCollider>();
        Vector3 resize = new Vector3(box.size.x, box.size.y / 2, box.size.z);
        GetComponent<BoxCollider>().size = resize;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Coin"))
        {
            GameObject.Destroy(other.gameObject);
            game_ref.coinCount++;
            game_ref.textoMoedas.text = "Coins: " + game_ref.coinCount;
        }
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
