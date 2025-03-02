using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float moveSpeedX;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameOverPanel.activeSelf)
        {
            rb.velocity = new Vector2(moveSpeedX, 0);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
