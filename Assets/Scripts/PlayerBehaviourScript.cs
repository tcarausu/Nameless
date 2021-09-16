using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerBehaviourScript : MonoBehaviour
{

    public Rigidbody2D rb;
    public Animator animator;


    private Vector2 movement;
    public float moveSpeed = 5f;

    private float moveX, moveY, dirX;
    private Vector3 moveDir;

    private SpriteRenderer sr;
    private AudioSource playerAudioSource;
    public bool isMoving = false;
    // public PlayerShootBow playerShootBowScript;
    // public PlayerMelee playerMeleeScript;

    private const string horizontalConst = "Horizontal";
    private const string verticalConst = "Vertical";
    private const string speedConst = "Speed";

    //run timer
    private float waitTime = 1.0f;
    private float timer = 0.0f;

    //ruuning and stamina
    float staminaValue;
    string staminaValueText;


    float maxStaminaValue;
    private bool isRegenStamina;
    private float staminaRegenRate = 1f;

    // private Inventory inventory;
    public static PlayerBehaviourScript Instance { get; private set; }

    private void Awake()
    {
        Application.targetFrameRate = 120;
        sr = GetComponent<SpriteRenderer>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Instance = this;

        // inventory = new Inventory();
    }
    void Update()
    {
        //if (!PauseMenu.gameisPaused)
        //{
            CharMovement();

            UpdateDirection();
        //}

    }


    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed;
    }


    private void CharMovement()
    {
        movement.x = Input.GetAxisRaw(horizontalConst);
        movement.y = Input.GetAxisRaw(verticalConst);

        moveX = movement.x;
        moveY = movement.y;

        moveDir = new Vector3(moveX, moveY).normalized;
        checkMovementOrAttackSounds();

        Animate();

        run();
    }

    private void checkMovementOrAttackSounds()
    {
        isMoving = rb.velocity.x != 0;

        // if (isMoving)
        // {
        //     if (!playerAudioSource.isPlaying)
        //     {
        //         playerAudioSource.Play();
        //     }
        // }
        // else
        // {
        //     if (!playerShootBowScript.getIsShooting() && !playerMeleeScript.getIsAttacking())
        //     {
        //         playerAudioSource.Stop();
        //     }
        // }
    }

    private void Animate()
    {
        animator.SetFloat(horizontalConst, movement.x);
        animator.SetFloat(verticalConst, movement.y);
        animator.SetFloat(speedConst, movement.sqrMagnitude);
    }

    private void run()
    {

        if (staminaValue < 100)
        {

            if (staminaValue != maxStaminaValue && !isRegenStamina)
            {
                // if (!stamScript.hasAttacked)
                // {
                    // StartCoroutine(RegainStaminaOverTime());
                // }
            }
        }

        moveSpeed = 5f;

    }

    private void timeToRun()
    {
        if (isMoving)
        {
            timer += Time.deltaTime;

            if (timer > waitTime)
            {

                timer -= waitTime;
                float valueInt = staminaValue - 1;

                if (valueInt >= 0)
                {
                    // stamScript.textMesh.text = valueInt.ToString();
                    // stamScript.setStamina(valueInt);

                    Time.timeScale = 1f;

                    //isRunning = true;
                }
            }

        }

    }


    private IEnumerator RegainStaminaOverTime()
    {
        isRegenStamina = true;
        while ((staminaValue < maxStaminaValue)
            //&& !isRunning
            )
        {

            // StaminaReg();

            yield return new WaitForSeconds(staminaRegenRate / 2);
        }
        isRegenStamina = false;
    }

    // public void StaminaReg()
    // {
        // staminaValue += staminaRegenRate;

        // stamScript.textMesh.text = staminaValue.ToString();

        // if (maxStaminaValue > staminaValue)
        // {
            // stamScript.setStamina(staminaValue);
        // }
        // else
            // stamScript.setStamina(maxStaminaValue);

    // }

    private void UpdateDirection()
    {
        if (Input.GetAxis(horizontalConst) < 0)
        {
            sr.flipX = true;
        }
        else if (Input.GetAxis(horizontalConst) > 0)
        {
            sr.flipX = false;
        }
    }

    public Vector3 getPosition()
    {
        return transform.position;
    }


    public void healPlayer(int healAmount)
    {
        //color green the player 
        var color = new Color(0, 1, 0, 1);
        sr.material.SetColor("_TintColor", color);


        HeartsHealthVisual.heartsHealthSystemStatic.heal(healAmount);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Weapon")
        {
            // ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
            // Sprite itemSprite = collider.GetComponent<SpriteRenderer>().sprite;
            //
            // SwapWeaponSprite currentWeapon = gameObject.GetComponentInChildren<SwapWeaponSprite>();

            // if (itemWorld != null)
            // {
            //     if (itemWorld.isWeapon())
            //     {
            //         currentWeapon.setCurrentUsingWeapon(itemWorld.GetItem().getWeaponItem(), true);
            //         inventory.AddItem(itemWorld.GetItem());
            //     }
            //     else if (!itemWorld.isWeapon())
            //     {
            //         healPlayer((int)itemWorld.GetHealingItem().healingAmount);
            //     }
            //
            //
            //     itemWorld.DestroySelf();
            // }
        }
    }
}