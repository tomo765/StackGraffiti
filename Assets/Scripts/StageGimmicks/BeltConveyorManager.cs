using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyorManager : MonoBehaviour
{
    public float speed = 3.0f;

    float diffX = 0.0f;
    bool reverseDirection = false; // スイッチによる逆転を制御するフラグ
    readonly List<GameObject> gameObjects = new List<GameObject>();

    void Start()
    {
        diffX = speed * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        foreach (GameObject gameObject in gameObjects)
        {

            // ベルトコンベアの動き
            Vector2 speedVector = new Vector3(reverseDirection ? -diffX : diffX, 0);
            Vector2 newPos = gameObject.GetComponent<Rigidbody2D>().position + speedVector;
            gameObject.GetComponent<Rigidbody2D>().position = newPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // スイッチに触れると逆方向に移動
        if (collision.transform.CompareTag("Switch"))
        {
            reverseDirection = !reverseDirection;
        }
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            gameObjects.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (gameObjects.Contains(collision.gameObject))
        {
            gameObjects.Remove(collision.gameObject);
        }
    }
}
