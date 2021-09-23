using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerBehaviourScriptOld : MonoBehaviour
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

    //weapon
    private Transform weaponTransform;
    private Animator weaponAnimator;

    //run timer
    private float waitTime = 1.0f;
    private float timer = 0.0f;
    //private bool isRunning = false;

    //ruuning and stamina
    public StaminaScript stamScript;
    float staminaValue;
    string staminaValueText;


    float maxStaminaValue;
    private bool isRegenStamina;
    private float staminaRegenRate = 1f;

    // private Inventory inventory;
    public static PlayerBehaviourScriptOld Instance { get; private set; }
    public GameObject UI_weapPos1, UI_weapPos2;

    private void Awake()
    {
        //Application.targetFrameRate = -1; is unlimited

        Application.targetFrameRate = 120;
        sr = GetComponent<SpriteRenderer>();
        playerAudioSource = GetComponent<AudioSource>();

        GameObject healthstam = GameObject.Find("HealthAndStamina");

        stamScript = healthstam.GetComponent<StaminaScript>();
        // playerShootBowScript = GetComponent<PlayerShootBow>();
        // playerMeleeScript = GetComponent<PlayerMelee>();

        maxStaminaValue = float.Parse(stamScript.textMesh.text);

        weaponTransform = transform.Find("Aim/Weapon");
        weaponAnimator = weaponTransform.GetComponent<Animator>();
    }

    //void LateUpdate()
    //{

    //    if (playerMeleeScript.isFacingLeft)
    //    {
    //        //weaponAnimator.SetTrigger("swapHandToLeft");
    //        weaponAnimator.SetTrigger("swapHandToRight");
    //    }
    //    else
    //    {
    //        //weaponAnimator.SetTrigger("swapHandToLeft");
    //        weaponAnimator.SetTrigger("swapHandToRight");
    //    }
    //}

    private void Start()
    {
        Instance = this;

        // inventory = new Inventory();
    }
    void Update()
    {
        //if (!PauseMenu.gameisPaused)
        //{
            staminaValueText = stamScript.textMesh.text;
            staminaValue = float.Parse(staminaValueText);


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
        if (rb.velocity.x != 0)
        {
            isMoving = true;
        }
        else isMoving = false;

        if (isMoving)
        {
            if (!playerAudioSource.isPlaying)
            {
                playerAudioSource.Play();
            }
        }
        else
        {
            // if (!playerShootBowScript.getIsShooting() && !playerMeleeScript.getIsAttacking())
            // {
            //     playerAudioSource.Stop();
            // }
        }
    }

    private void Animate()
    {
        animator.SetFloat(horizontalConst, movement.x);
        animator.SetFloat(verticalConst, movement.y);
        animator.SetFloat(speedConst, movement.sqrMagnitude);
    }

    private void run()
    {

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    moveSpeed = 10f;

        //    timeToRun();
        //}
        //else
        //{
        //    isRunning = false;

        if (staminaValue < 100)
        {

            if (staminaValue != maxStaminaValue && !isRegenStamina)
            {
                if (!stamScript.hasAttacked)
                {
                    StartCoroutine(RegainStaminaOverTime());
                }
            }
        }

        moveSpeed = 5f;
        //}

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
                    stamScript.textMesh.text = valueInt.ToString();
                    stamScript.setStamina(valueInt);

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

            StaminaReg();

            yield return new WaitForSeconds(staminaRegenRate / 2);
        }
        isRegenStamina = false;
    }

    public void StaminaReg()
    {
        staminaValue += staminaRegenRate;

        stamScript.textMesh.text = staminaValue.ToString();

        if (maxStaminaValue > staminaValue)
        {
            stamScript.setStamina(staminaValue);
        }
        else
            stamScript.setStamina(maxStaminaValue);

    }

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
            Sprite itemSprite = collider.GetComponent<SpriteRenderer>().sprite;

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