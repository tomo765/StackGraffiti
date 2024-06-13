using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb2d;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddForce(Vector2.right, ForceMode2D.Impulse);
        }
        if(Input.GetKey(KeyCode.A))
        {
            rb2d.AddForce(-Vector2.right, ForceMode2D.Impulse);
        }
    }
}
