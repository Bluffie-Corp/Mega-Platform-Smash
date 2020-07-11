using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;

    private HealthBar hpBar;

    private int healthAmount;
    private int shieldAmount;
    private int shieldAmountMax;
    private bool hasShieldActivated;
    private int healthAmountMax;

    public HealthSystem(int healthAmount)
    {
        healthAmountMax = healthAmount;
        this.healthAmount = healthAmount;
        hasShieldActivated = false;
        shieldAmount = 0;
    }

    public void Damage(int dmgAmount)
    {
        if (shieldAmount > 0 && hasShieldActivated)
        {
            if (healthAmount < 0)
            {
                healthAmount = 0;
            }
            
            shieldAmount -= dmgAmount;
            OnDamaged?.Invoke(this, EventArgs.Empty);

            if (shieldAmount == 0)
            {
                hasShieldActivated = false;
            }
            return;
        }
        
        healthAmount -= dmgAmount;
        if (healthAmount < 0)
        {
            healthAmount = 0;
        }
        
        OnDamaged?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        if (hasShieldActivated)
        {
            shieldAmount += healAmount;
            if (shieldAmount > 100)
            {
                shieldAmount = shieldAmountMax;
            }
            OnHealed?.Invoke(this, EventArgs.Empty);
            return;
        }
        
        healthAmount += healAmount;
        if (healthAmount > healthAmountMax)
        {
            healthAmount = healthAmountMax;
        }
        
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public int GetHealth()
    {
        return healthAmount;
    }

    public void SetHealth(int healthAmount)
    {
        this.healthAmount = healthAmount;
    }

    public bool GetShieldActivated()
    {
        return hasShieldActivated;
    }

    public int GetShieldAmount()
    {
        return shieldAmount;
    }
    
    public void SetShieldActive(int maxShield)
    {
        hasShieldActivated = true;
        shieldAmountMax = maxShield;
    }
}
