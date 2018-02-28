using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float m_speed = 5.0f;

    private Vector2 m_Direction;

    Rigidbody2D RigBody;

    float m_timeElipsed;

	// Use this for initialization
	void Start () {

        m_Direction = (new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f))).normalized;
        RigBody = gameObject.GetComponent<Rigidbody2D>();
        RigBody.velocity = m_Direction * m_speed;        
	}
	
	// Update is called once per frame
	void Update () {

        m_timeElipsed += Time.deltaTime;

        m_Direction = RigBody.velocity.normalized;
        RigBody.velocity = m_Direction * m_speed;

        if (m_timeElipsed > 1.0f && m_speed < 20.0f)
        {
            m_speed += 0.2f;
            m_timeElipsed = 0.0f;
        }

    }
}
