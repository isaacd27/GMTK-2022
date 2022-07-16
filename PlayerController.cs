using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
 

    public float moveSpeed = 5f;
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        // Debug.Log(IsGrounded());

        // if (tilemap.ContainsTile(tilemap.name("Player")))
        // {

        //  }

        transform.position += movement * Time.deltaTime * moveSpeed;
    }
}


    