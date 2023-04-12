using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private float m_range;

    private void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(0, v * m_speed * Time.deltaTime));
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -m_range, m_range), -10);
    }
}
