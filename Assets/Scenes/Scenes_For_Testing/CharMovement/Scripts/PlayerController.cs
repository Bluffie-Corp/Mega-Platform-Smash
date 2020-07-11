using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    public PlayerStats stats;
    private bool gameOver = false;
    private Animator blackScreenAnimator;

    private static bool flip = true;

    private SaveManager saveManager;

    private bool isInDangerPlatform = false;
    private bool isInHealPlatform = false;
    public AudioSource oofSound;
    public AudioSource healSound;

    public int playerMaxHealth = 100;
    public int playerMaxShield = 100;
    public HealthBar hpBar;
    public int currentHealth;
    public int currentShield;

    public float speed;
    public float jumpForce;
    private float moveInput;

    private static Rigidbody2D rb2d;
    private bool facingRight;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;

    private void Awake()
    {
        if (transform.localScale.x > 0)
        {
            facingRight = true;
        }
        saveManager = FindObjectOfType<SaveManager>();
        blackScreenAnimator = FindObjectOfType<Canvas>().GetComponentInChildren<Animator>();
        if (!saveManager.GetSaveExisting()) return;
        saveManager.Load();

        currentHealth = stats.health;
        currentShield = stats.shield;
    }

    private void Start()
    {
        extraJumps = extraJumpsValue;
        rb2d = GetComponent<Rigidbody2D>();

        currentHealth = playerMaxHealth;
        hpBar.SetMaxHealth(playerMaxHealth);
        stats.maxHealth = playerMaxHealth;
    }

    public Animator anim;

    private void TakeDamage(int dmgAmount)
    {
        hpBar.TakeDamage(dmgAmount);
        currentHealth = hpBar.GetHealth();

        if (hpBar.HasShieldActive()) return;
        oofSound.Play(0);
    }

    public void Heal(int healAmount)
    {
        currentHealth = hpBar.GetHealth();
        hpBar.Heal(healAmount);
        healSound.Play(0);
    }

    public void GameOver()
    {
        rb2d.mass = 10000000;
        rb2d.gravityScale = 1000000;
        SceneManager.LoadScene("GameOverScene");
    }

    public void SetMaxHP(int maxHP)
    {
        hpBar.SetMaxHealth(maxHP);
    }

    private void FixedUpdate()
    {
        currentHealth = hpBar.GetHealth();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded && rb2d.velocity.y < -30)
        {
            TakeDamage(-(int)Math.Round(rb2d.velocity.y) - 10);
        }

        //moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(GameManager.GM.left))
        {
            moveInput = -1 * speed;
        }
        else if (Input.GetKey(GameManager.GM.right))
        {
            moveInput = 1 * speed;
        }
        else moveInput = 0;

        rb2d.velocity = new Vector2(moveInput, rb2d.velocity.y);

        if (gameOver) return;
        if (isInSomeMenu) return;
        if (!flip) return;
        switch (facingRight)
        {
            case false when moveInput > 0:
            case true when moveInput < 0:
                Flip();
                break;
        }
    }

    public void OnCollisionEnter2D(Collision2D targetObj)
    {
        if(targetObj.gameObject.tag.Equals("Danger"))
        {
            TakeDamage(10);
            isInDangerPlatform = true;
        }

        if (targetObj.gameObject.tag.Equals("Heal"))
        {
            if (currentHealth >= 100) return;
            Heal(10);
            isInHealPlatform = true;
        }

        // Debug.Log(rb2d.velocity.y);
        // if (targetObj.gameObject.layer.Equals(8) && rb2d.velocity.y < -10)
        // {
        //     
        //     TakeDamage(-(int)Math.Round(rb2d.velocity.y) * 2);
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.tag.Equals("Shield")) return;
        Debug.Log("Shield Power-UP Picked!");
        hpBar.Shield(99, playerMaxShield);
        Destroy(other.gameObject);
    }

    public void OnCollisionExit2D(Collision2D targetObj)
    {
        if (targetObj.gameObject.tag.Equals("Danger"))
        {
            TakeDamage(10);
            isInDangerPlatform = false;
        }

        if (!targetObj.gameObject.tag.Equals("Heal")) return;
        Heal(10);
        isInHealPlatform = false;
    }
    
    private int timeToTakeDamage = 30;
    private int timeToHeal = 30;
    private static bool isInSomeMenu;

    // Update Region
    #region Update Function
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SavePlayer();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadPlayer();
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentHealth < 100)
            {
                healSound.Play(0);
                hpBar.Heal(10);
            }
        }
        
        if (currentHealth <= 0)
        {
            gameOver = true;
            rb2d.isKinematic = true;
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            anim.Play("LoseAnim");
            return;
        }

        currentHealth = hpBar.GetHealth();

        if (isInDangerPlatform)
        {
            timeToTakeDamage--;
            if (timeToTakeDamage <= 0)
            {
                TakeDamage(10);
                timeToTakeDamage = 30;
            }
        }
        
        if (isInHealPlatform)
        {
            timeToHeal--;
            if (currentHealth < 100)
            {
                if (timeToHeal <= 0)
                {
                    Heal(10);
                    timeToHeal = 30;
                }
            }
            if (currentHealth == 100)
            {
                timeToHeal = 30;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(10);
        }
        
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }
        
        if (Input.GetKeyDown(GameManager.GM.jump) && extraJumps > 0)
        {
            rb2d.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(GameManager.GM.jump) && extraJumps == 0 && isGrounded)
        {
            rb2d.velocity = Vector2.up * jumpForce;
        }
    }
    #endregion

    private void Flip()
    {
        facingRight = !facingRight;
        var Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public static void freezePlayer()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        isInSomeMenu = true;
    }

    public static void freezePlayerAndFlip()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        flip = false;
    }

    public static void unfreezePlayer()
    {
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        isInSomeMenu = false;
    }

    public void SavePlayer()
    {
        stats.health = currentHealth;
        stats.maxHealth = playerMaxHealth;
        stats.shield = hpBar.GetShieldAmount();
        stats.maxShield = playerMaxShield;
        stats.hasShieldActive = hpBar.HasShieldActive();
        stats.pos.X = transform.position.x;
        stats.pos.Y = transform.position.y;
        saveManager.Save();
    }

    public void LoadPlayer()
    {
        StartCoroutine(LoadPlayerIE());
        StopCoroutine(LoadPlayerIE());
    }

    IEnumerator LoadPlayerIE()
    {
        blackScreenAnimator.SetTrigger("Start Animation");
        yield return new WaitForSeconds(0.8f);
        saveManager.Load();
        currentHealth = stats.health;
        transform.position = stats.pos.GetPos();
        hpBar.SetHealth(currentHealth);
        yield return new WaitForSeconds(0.8f);
        blackScreenAnimator.SetTrigger("Finished Loading");
        yield return null;
    }
}
