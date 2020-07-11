using System;
using CodeMonkey;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = 25f;
    
    public Slider fillSlider;
    private Slider damagedSlider;
    private Slider shieldSlider;
    private float damagedHealthShrinkTimer;
    public int maxHealth = 100;
    private HealthSystem healthSystem;
    public AudioSource shieldHitSound;
    
    private Image _fillImage;

    private void Awake()
    {
        damagedSlider = transform.Find("Damaged Fill Slider").GetComponent<Slider>();
        shieldSlider = transform.Find("Shield Fill Slider").GetComponent<Slider>();
    }
    
    private void Update()
    {
        if (damagedSlider.value <= fillSlider.value)
        {
            damagedSlider.value = fillSlider.value;
        }
        
        damagedHealthShrinkTimer -= Time.deltaTime * maxHealth;

        if (!(damagedHealthShrinkTimer < 0)) return;
        if (!(fillSlider.value < damagedSlider.value)) return;
        const float shrinkSpeed = 1f;
        damagedSlider.value -= shrinkSpeed * Time.deltaTime * maxHealth;
    }

    private void Start()
    {
        _fillImage = fillSlider
            .fillRect
            .gameObject
            .GetComponent<Image>();
        healthSystem = new HealthSystem(maxHealth);
        shieldSlider.value = 0;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
        healthSystem.OnShieled += HealthSystem_OnShieled;

        /* CMDebug.ButtonUI(new Vector2(-100, -50), "Damage", () => healthSystem.Damage(10));
        CMDebug.ButtonUI(new Vector2(+100, -50), "Heal", () => healthSystem.Heal(10)); */
    }

    public void SetMaxHealth(int maxHealth)
    {
        fillSlider.maxValue = maxHealth;
        damagedSlider.maxValue = maxHealth;
        shieldSlider.maxValue = maxHealth;
        fillSlider.value = maxHealth;
        damagedSlider.value = maxHealth;
        shieldSlider.value = 0;
    }

    public void SetFillHealth(int health)
    {
        fillSlider.value = health;
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        if (healthSystem.GetShieldActivated())
        {
            shieldSlider.value = healthSystem.GetShieldAmount();
            return;
        }

        if (healthSystem.GetShieldAmount() == 0)
        {
            _fillImage
                .color = Color.red; // A Bright Red
        }
        SetFillHealth(healthSystem.GetHealth());
    }
    
    private void HealthSystem_OnHealed(object sender, EventArgs e)
    {
        SetFillHealth(healthSystem.GetHealth());
    }

    private void HealthSystem_OnShieled(object sender, EventArgs e)
    {
        shieldSlider.value = healthSystem.GetShieldAmount();
    }

    public void TakeDamage(int dmgAmount)
    {
        if (healthSystem.GetShieldActivated())
        {
            shieldHitSound.Play(0);
        }
        healthSystem.Damage(dmgAmount);
    }
    
    public void Heal(int healAmount)
    {
        healthSystem.Heal(healAmount);
        if (healthSystem.GetShieldActivated())
        {
            shieldSlider.value = healthSystem.GetShieldAmount();
        }
    }

    public int GetHealth()
    {
        return healthSystem.GetHealth();
    }

    public void SetHealth(int hpAmount)
    {
        healthSystem.SetHealth(hpAmount);
        fillSlider.value = healthSystem.GetHealth();
        damagedSlider.value = healthSystem.GetHealth();
    }

    public void Shield(int shieldAmount, int maxShieldAmount)
    {
        if (!healthSystem.GetShieldActivated())
        {
            healthSystem.SetShieldActive(maxShieldAmount);
        }

        healthSystem.Shield(shieldAmount);
        shieldSlider.value = healthSystem.GetShieldAmount();
        
        if (healthSystem.GetShieldAmount() >= 1)
        {
            fillSlider
                .fillRect
                .gameObject
                .GetComponent<Image>()
                .color = new Color(127f/255f, 0, 0, 255f/255f); // A Dark Red
        }
    }

    public bool HasShieldActive()
    {
       return healthSystem.GetShieldActivated();
    }

    public int GetShieldAmount()
    {
        return healthSystem.GetShieldAmount();
    }
}
