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
        if (CnInputManager.GetButton("Boost"))
        {
            m_Ship.Boost();
        }


        if (CnInputManager.GetButton("Fire"))
        {
            m_Ship.Shoot();
        }

        if (CnInputManager.GetButton("Left"))
        {
            m_Ship.Move(1);
        }

        if (CnInputManager.GetButtonUp("Left"))
        {
            m_Ship.RotationTime = 0;
        }

        if(CnInputManager.GetButtonUp("Right"))
        {
            m_Ship.RotationTime = 0;
        }

        if (CnInputManager.GetButton("Right"))
        {
            m_Ship.Move(-1);
        }

        // Just use CnInputManager. instead of Input. and you're good to go
        var inputVector = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));
        m_Ship.Boost(inputVector.magnitude);
        if (inputVector.sqrMagnitude > 0.001f)
        {
            m_Ship.Move(inputVector);
        }   

    }

    private void FixedUpdate()
    {

    }
}
