using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionInput : MonoBehaviour
{
    [SerializeField] EventInput m_input;
    public delegate void TestDelegate(); 
    public TestDelegate m_event;

    [SerializeField] bool m_moveRight = true;

    bool m_mouseIn = false;

    private void Start()
    {
        if(m_moveRight)
            m_event = m_input.RightButton;
        else
            m_event = m_input.LeftButton;
    }
    public void EnterMouse()
    {
        m_mouseIn = true;
    }

    public void ExitMouse()
    {
        m_mouseIn = false;
    }

    private void Update()
    {
        if (m_mouseIn)
        {
            if (Input.GetMouseButton(0))
            {
                m_event();
            }
        }
    }
}
