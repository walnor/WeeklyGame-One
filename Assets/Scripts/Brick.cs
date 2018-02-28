using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    [HideInInspector] public bool m_isActive = true;
    SpriteRenderer m_Renderer;
    BoxCollider2D m_Collider;

    static Color32 DeactiveColor = new Color(250.0f/255.0f, 250.0f / 255.0f, 250.0f / 255.0f, 0.2f);
    static Color32 activeColor = new Color(122.0f / 255.0f, 255.0f / 255.0f, 217.0f / 255.0f, 1.0f);

    // Use this for initialization
    void Start () {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<BoxCollider2D>();


        m_Renderer.color = DeactiveColor;
        m_Collider.enabled = false;
        m_isActive = false;
    }

    // Update is called once per frame
    void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_isActive)
        {
            if (collision.gameObject.tag == "Ball")
            {
                Deactivate(true);
            }
        }
    }

    public void Activate()
    {
        m_Renderer.color = activeColor;// = new Color(122.0f, 255.0f, 217.0f, 1.0f);
        m_Collider.enabled = true;
        m_isActive = true;
    }

    public void Deactivate(bool playSound)
    {
        m_Renderer.color = DeactiveColor;
        m_Collider.enabled = false;
        m_isActive = false;

        if (playSound)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
