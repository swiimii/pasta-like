﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float movementDeadzone = .1f;
    public int maxHealth = 3;
    public int currentHealth;
    public bool paused = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    // FixedUpdate called 60 times per second
    void FixedUpdate()
    {
        if (!paused)
        {
            var movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Mathf.Abs(movementInput.x) > movementDeadzone || Mathf.Abs(movementInput.y) > movementDeadzone)
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

        if (Input.GetButtonDown("Cancel"))
        {
            TogglePauseMenu();
            paused = paused ? false : true;
        }
    }

    public void TogglePauseMenu()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>().TogglePauseMenu();
    }

}
