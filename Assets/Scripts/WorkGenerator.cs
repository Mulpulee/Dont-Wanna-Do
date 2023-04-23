using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkGenerator : MonoBehaviour
{
    [SerializeField] private float m_delay;
    [SerializeField] private float m_spawnRange;
    [SerializeField] private GameObject[] m_workPrefab;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_delay);
            Instantiate(m_workPrefab[Random.Range(0, m_workPrefab.Length)], 
                new Vector3(Random.Range(-m_spawnRange, m_spawnRange), transform.position.y), Quaternion.identity);
        }
    }

    public void AddDelay(float _value)
    {
        if (m_delay > _value) m_delay += _value;
    }
}
