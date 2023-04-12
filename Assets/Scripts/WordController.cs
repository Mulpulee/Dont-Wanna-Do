using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordController : MonoBehaviour
{
    [SerializeField] private float m_speed;
    
    private Vector3 m_direction;

    private void Start()
    {
        m_direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg - 90, Vector3.forward);
        m_direction.z = 0;
    }

    private void Update()
    {
        transform.position += m_direction.normalized * m_speed * Time.deltaTime;

        if(transform.position.x < -15 || 15 < transform.position.x || 
            transform.position.y < - 20 || 20 < transform.position.y) Destroy(gameObject);
    }
}
