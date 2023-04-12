using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkController : MonoBehaviour
{
    [SerializeField] private float m_speed;

    private void Update()
    {
        transform.Translate(Vector2.up * m_speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Word")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.tag == "Ground")
        {
            // 게임오버
            Destroy(gameObject);
        }
    }
}
