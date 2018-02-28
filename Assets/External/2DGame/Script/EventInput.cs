using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInput : MonoBehaviour {

    [SerializeField] Player m_player;

    public void JumpButton()
    {
        m_player.Jump();
    }

    public void LeftButton()
    {
        m_player.moveLeft();
    }

    public void RightButton()
    {
        m_player.moveRight();
    }


}
