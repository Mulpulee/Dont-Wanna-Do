using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private int m_wordCount;
    [SerializeField] private GameObject[] m_wordPrefabs;

    private void Start()
    {
        m_wordCount = 0;
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector2(h * m_speed * Time.deltaTime, 0));

        if(Input.GetMouseButtonDown(0) && m_wordCount < 4)
        {
            Instantiate(m_wordPrefabs[m_wordCount], transform.position, Quaternion.identity);
            m_wordCount++;
        }
    }

    public void ResetCount()
    {
        m_wordCount = 0;
    }
}
