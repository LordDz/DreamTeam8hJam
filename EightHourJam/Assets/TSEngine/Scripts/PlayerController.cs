﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
public class PlayerController : MonoBehaviour {

    public int PlayerNr = 1;
    public bool isKeyboard = true;
    public Team TeamPlr;

    private PlayerController playerToLookAt;
    private PlayerHealthImg PlayerHealthImg;

    public Weapon.DamageType DamageType;

    public float maxHealth;
    public float health;

    public float rotationOffset;
    public float movementSpeed;
    float startSpeed;
    float diagonalSpeed;

    float inputV;
    float inputH;

    Rigidbody2D rb;
    Animator anim;

    public Image healthBarValue;

    public Transform rotationOrigin;
    public float minDistance = 2;

    [HideInInspector]
    public bool frozen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        health = maxHealth;
        startSpeed = movementSpeed;
        diagonalSpeed = startSpeed * 0.66f;

        //Only shooters face their target
        if (this.PlayerNr == 1 || this.PlayerNr == 3)
        {
            PlayerController[] players = FindObjectsOfType<PlayerController>();

            foreach (var player in players)
            {
                if (this.TeamPlr == Team.One && player.PlayerNr == 3)
                {
                    this.playerToLookAt = player;
                    break;
                }
                else if (this.TeamPlr == Team.Two && player.PlayerNr == 1)
                {
                    this.playerToLookAt = player;
                    break;
                }
            }

            PlayerHealthImg[] healthImgs = FindObjectsOfType<PlayerHealthImg>();
            foreach (var img in healthImgs)
            {
                if (this.TeamPlr == Team.One && img.PlayerNr == 1)
                {
                    this.PlayerHealthImg = img;
                    break;
                }
                else if (this.TeamPlr == Team.Two && img.PlayerNr == 3)
                {
                    this.PlayerHealthImg = img;
                    break;
                }
            }
        }
        ShowHealth();
    }

    void FixedUpdate()
    {
        if (!frozen)
        {
            if (playerToLookAt != null && Vector2.Distance(playerToLookAt.transform.position, rotationOrigin.position) > minDistance)
            {
                Vector3 difference = playerToLookAt.transform.position - rotationOrigin.position;
                difference.Normalize();
                float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ + rotationOffset);
            }

            rb.angularVelocity = 0;

            inputV = Input.GetAxis("Vertical" + PlayerNr);
            inputH = Input.GetAxis("Horizontal" + PlayerNr);

            if (inputV != 0 || inputH != 0)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
            if (Mathf.Abs(inputH) > 0 && Mathf.Abs(inputV) > 0)
            {
                movementSpeed = diagonalSpeed;
            }
            else
            {
                movementSpeed = startSpeed;
            }
        }
    }

    void Update()
    {
        if (!frozen)
        {
            rb.AddForce(Vector2.up * inputV * movementSpeed * Time.deltaTime);
            rb.AddForce(Vector2.right * inputH * movementSpeed * Time.deltaTime);
        }
        if (health <= 0)
        {
            WinScreen winScreen = FindObjectOfType<WinScreen>();
            winScreen.Win(this.TeamPlr);
            Destroy(gameObject);
        }
    }

    public void ShowHealth()
    {
        if (healthBarValue != null)
        {
            healthBarValue.fillAmount = Remap(health,0,maxHealth,0,1);
        }
    }

    public void UpdatePlayerUIHealth()
    {
        if (PlayerHealthImg != null)
        {
            PlayerHealthImg.SetHealth(this.maxHealth, this.health);
        }
    }

    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
