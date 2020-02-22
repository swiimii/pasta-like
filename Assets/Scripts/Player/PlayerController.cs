using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float movementDeadzone = .1f;

    // FixedUpdate called 60 times per second
    void FixedUpdate()
    {
        var movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if ( Mathf.Abs(movementInput.x) > movementDeadzone || Mathf.Abs(movementInput.y) > movementDeadzone )
        {
            // Vector magnitude won't exceed 1
            if (movementInput.magnitude > 1)
            {
                movementInput.Normalize();
            }

            // Send to playerbehavior
            GetComponent<PlayerBehavior>().Move(movementInput);
        }
    }

    // Called once every frame
    void Update()
    {
        
    }



}

