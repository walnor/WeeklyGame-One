using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

    [SerializeField] Brick[] bricks;
    [SerializeField] int NumToActivate = 20;

    [SerializeField] GameObject m_Player;
    [SerializeField] Transform m_PlayerSpawn;

    [SerializeField] GameObject m_Ball;

    [SerializeField] Transform m_SpawnPoint;

    [SerializeField] int m_SpawnbyPointRate = 20;

    [SerializeField] Text m_text;

    [SerializeField] GameObject m_pauseScreen;

    [SerializeField] Transform m_SpawnFirstBall;

    [SerializeField] GameObject m_WorldScalar;


    int m_numActive = 1;

    float ellipsedTime = 0.0f;

    int points = 0;

    [SerializeField] GameObject m_camAdjust;

	// Use this for initialization
	void Start ()
    {
        Vector3 screenPosition = new Vector3(0, 0, 0);

        m_camAdjust.transform.position = Camera.main.ScreenToWorldPoint(screenPosition);

        float x =  1.0f - m_camAdjust.transform.position.x;
        float y =  1.0f - m_camAdjust.transform.position.y;

        m_WorldScalar.transform.localScale += new Vector3(-m_camAdjust.transform.position.x/ 6.275f, -m_camAdjust.transform.position.y/5.0f, 0);
        m_WorldScalar.transform.position += new Vector3(m_camAdjust.transform.position.x, m_camAdjust.transform.position.y, 0);

        init();
    }

    public void init()
    {
        Time.timeScale = 0;

        List<Brick> unactiveBricks = new List<Brick>();

        
        foreach (Brick b in bricks)
        {
            if (!b.m_isActive)
            {
                unactiveBricks.Add(b);
            }
        }

        while(bricks.Length - unactiveBricks.Count < NumToActivate)
        {
            int ranIndex = (int)Random.Range(0.0f, (float)(unactiveBricks.Count - 1));
            unactiveBricks[ranIndex].Activate();
            unactiveBricks.RemoveAt(ranIndex);
        }
        points = 0;
    }
	
	// Update is called once per frame
	void Update () {
        ellipsedTime += Time.deltaTime;

        if (Time.timeScale == 0)
        {
            if (Input.anyKeyDown)
            {
                Time.timeScale = 1;
                m_pauseScreen.SetActive(false);
            }
            else
                return;
        }

        if (ellipsedTime > 1.0f)
        {
            List<Brick> unactiveBricks = new List<Brick>();

                foreach (Brick b in bricks)
                {
                    if (!b.m_isActive)
                    {
                        unactiveBricks.Add(b);
                    }
                }

                points += NumToActivate - (bricks.Length - unactiveBricks.Count);
            m_text.text = points.ToString();

                while (bricks.Length - unactiveBricks.Count < NumToActivate)
                {
                    int ranIndex = (int)Random.Range(0.0f, (float)(unactiveBricks.Count - 1));
                    unactiveBricks[ranIndex].Activate();
                    unactiveBricks.RemoveAt(ranIndex);
                }

            ellipsedTime = 0.0f;

            if (points > m_numActive * m_numActive * m_SpawnbyPointRate)
            {
                GameObject newBall = Instantiate(m_Ball, m_SpawnPoint);
                m_numActive++;
            }
        }		
	}

    public void GameEnd()
    {
        Ball[] balls = FindObjectsOfType<Ball>();

        foreach (Ball b in balls)
        {
            Destroy(b.gameObject);
        }

        foreach (Brick b in bricks)
        {
            b.Deactivate(false);
        }

        m_Player.SetActive(true);
        m_Player.transform.position = m_PlayerSpawn.position;

        points = 0;

        m_numActive = 1;

        GameObject newBall = Instantiate(m_Ball);
        newBall.transform.position = m_SpawnFirstBall.position;
        m_pauseScreen.SetActive(true);

        init();
    }
}
