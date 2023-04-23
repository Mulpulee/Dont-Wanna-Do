using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealController : MonoBehaviour
{
    [SerializeField] private float m_speed;

    private void Update()
    {
        transform.Translate(Vector2.down * m_speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground") Destroy(gameObject);
        else if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().EatMeal();
            Destroy(gameObject);
        }
    }
}
