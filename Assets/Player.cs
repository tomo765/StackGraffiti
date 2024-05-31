using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpPower;

    public float linearDrag = 1.0f;
    public float angularDrag = 1.0f;

    private Rigidbody2D rb2d;
    private bool isGrounded; // 地面に接地しているかどうかを判定するフラグ
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.drag = linearDrag;
        rb2d.angularDrag = angularDrag;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveX, 0);
        rb2d.AddForce(movement * moveSpeed);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGrounded = false; // ジャンプしたら接地していない状態にする
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面に接触したらisGroundedをtrueにする
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // 地面から離れたらisGroundedをfalseにする
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
