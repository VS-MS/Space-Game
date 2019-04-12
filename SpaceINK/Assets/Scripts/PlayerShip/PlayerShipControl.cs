using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class PlayerShipControl : MonoBehaviour {

    private PlayerShip m_Ship;
    private bool m_Boost;
    private bool m_Fire;

    private void Awake()
    {
        m_Ship = GetComponent<PlayerShip>();
    }

    private void Update()
    {
        /*if (!m_Boost)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Boost = CnInputManager.GetButtonDown("Boost");
            //Debug.Log("Boost");
        }*/
        //Debug.Log(CnInputManager.GetButton("jump"));
        //Debug.Log(CnInputManager.GetButtonDown("Jump"));
        if (CnInputManager.GetButton("Boost"))
        {
            Debug.Log("Boost");
            m_Ship.Boost();
        }


        if (CnInputManager.GetButton("Fire"))
        {
            m_Ship.Shoot();
            //m_Ship.Shoot();
        }

        if (CnInputManager.GetButton("Left"))
        {
            m_Ship.Move(-1);
        }

        if (CnInputManager.GetButton("Right"))
        {
            m_Ship.Move(1);
        }

    }

    private void FixedUpdate()
    {

        //float h = CnInputManager.GetAxis("Horizontal");
        // Pass all parameters to the character control script.
        //m_Ship.Move(h);
    }
}
