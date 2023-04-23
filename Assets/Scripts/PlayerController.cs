using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private int m_wordCount;
    [SerializeField] private GameObject[] m_wordPrefabs;
    [SerializeField] private float m_range;

    [SerializeField] private Sprite m_normalPlayer;
    [SerializeField] private Sprite m_eatingPlayer;
    [SerializeField] private Sprite m_exautedPlayer;

    private SpriteRenderer m_spriteRenderer;

    private void Start()
    {
        m_wordCount = 0;
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector2(h * m_speed * Time.deltaTime, 0));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -m_range, m_range), transform.position.y);

        if (Input.GetMouseButtonDown(0) && m_wordCount < 4)
        {
            Instantiate(m_wordPrefabs[m_wordCount], transform.position, Quaternion.identity);
            m_wordCount++;
            if (m_wordCount == 4) m_spriteRenderer.sprite = m_exautedPlayer;
        }
    }

    public void EatMeal()
    {
        m_wordCount = 0;
        StartCoroutine(EatMealRoutine());
    }

    private IEnumerator EatMealRoutine()
    {
        m_spriteRenderer.sprite = m_eatingPlayer;
        yield return new WaitForSeconds(0.5f);
        m_spriteRenderer.sprite = m_normalPlayer;
    }
}
