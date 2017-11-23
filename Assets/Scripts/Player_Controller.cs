using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    // Variables 
    public float m_fSpeed = 10;
    public float m_fMaxSpeed = 25;
    public float m_jumpAmount = 6;

    float m_jumpCurrCooldown;
    float m_jumpMaxCooldown;

    bool m_bAlive;
    bool m_canJump;
    bool m_canForward;

    float m_lastY = 0;

    Animator m_animator;


    void Start()
    {
        // Initialize variables on start
        m_bAlive = true;

        m_jumpMaxCooldown = 0.5f;

        m_canForward = true;

        m_animator = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        // If the player is alive...
        if (m_bAlive)
        {
            if (transform.position.y < m_lastY)
                m_animator.SetBool("isJumping", true);
            m_lastY = transform.position.y;


            GetComponent<Rigidbody>().AddForce(new Vector3(0, -1000 * Time.deltaTime, 0));

            m_jumpCurrCooldown -= Time.deltaTime;

            // Update Player X Pos
            if (m_canForward)
            {
                m_fSpeed += (Time.deltaTime / 5);

                if (m_fSpeed > m_fMaxSpeed)
                    m_fSpeed = m_fMaxSpeed;

                transform.Translate(new Vector3(m_fSpeed * Time.deltaTime, 0, 0));
            }


            // On jump update Player Y pos
            if (Input.GetButtonDown("Fire1") && m_canJump && m_jumpCurrCooldown <= 0)
            {
                m_animator.SetBool("isJumping", true);
                GetComponent<Rigidbody>().AddForce(new Vector3(0, (m_jumpAmount * 2000) * Time.deltaTime, 0));
                m_jumpCurrCooldown = m_jumpMaxCooldown;
                m_canJump = false;
            }
            if (transform.position.y <= -10)
            {
                m_bAlive = false;
                GameObject Ref = GameObject.FindGameObjectWithTag("UI");
                Ref.GetComponent<UI>().GameOver = true;
            }
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if (transform.position.x < col.transform.position.x - col.transform.localScale.x / 2)
        {
            m_canForward = false;
            if (transform.position.y - transform.localScale.y / 2 >= col.transform.position.y + col.transform.localScale.y / 2)
            {
                m_canForward = true;
            }
        }
        else
            m_canForward = true;

        if (transform.position.y > col.transform.position.y + col.transform.localScale.y / 2)
        {

            m_canJump = true;
        }

    }

    void OnCollisionStay(Collision col)
    {
        m_animator.SetBool("isJumping", false);
    }

    void OnCollisionExit(Collision col)
    {
        m_animator.SetBool("isJumping", true);
    }
}
