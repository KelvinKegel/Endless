using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField]
    Player player_ref;

    [Header("Health Settings")]

    [SerializeField]
    float posY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player_ref.transform.localPosition;
        pos.y = posY;
        //pos.x = player_ref.transform.position.x;
        transform.localPosition = pos;
        
    }
}
