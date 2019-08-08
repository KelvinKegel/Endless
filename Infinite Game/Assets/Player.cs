using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Game game_ref;

    [SerializeField]
    private float downSpeed;

    [SerializeField]
    private float speed = 15f;

    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float velocity;

    [SerializeField]
    private GameObject mesh;

    private float jumpHeight = 15f;

    [SerializeField]
    private bool isGrounded;

    [SerializeField]
    private bool isDead = false;

    private void Start()
    {
    }

    private void Update()
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
            if (Input.touches.Length == 1) //se tem apenas um toque na tela
            {
                if (Input.touches[0].phase == TouchPhase.Began) //se recém tocou na tela e não moveu
                    {

                    if (Input.touches[0].position.x > Screen.width / 2) // se a posição no x for maior que a metade da tela
                    {
                        teleport();
                    }
                    else
                    {
                        rasteirinhaPerigosa();
                        StartCoroutine(CooldownRasteira());
                    }
                }
            }
#endif
        }

        mesh.transform.position = transform.position;
    }

    private void running()
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
            game_ref.gameOver();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if ((collision.transform.CompareTag("Ground")) == true)
        {
            isGrounded = false;
        }
    }

    private void teleport()
    {
        if (isGrounded)
        {
            transform.parent.Translate(0, jumpHeight, 0);
        }
    }

    public float getjumpHeight()
    {
        return jumpHeight;
    }

    private IEnumerator CooldownRasteira()
    {
        yield return new WaitForSeconds(1.5f);
        mesh.transform.localEulerAngles = new Vector3(0, 0, 0);
        GetComponent<BoxCollider>().size = Vector3.one;
    }

    private void rasteirinhaPerigosa()
    {
        if (isGrounded == false)
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