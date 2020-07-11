using System;
using UnityEngine;

[Serializable]
public struct SerializableVector3
{
    public float X;
    public float Y;
    // private float z;

    public Vector2 GetPos()
    {
        return new Vector2(X, Y);
    }
}

[Serializable]
public class PlayerStats
{
    // Here goes all player stats, e.g Health, Attack Points, Defense Points, Speed Points etc.
    public int health;
    public int maxHealth;
    public int shield;
    public int maxShield;
    public bool hasShieldActive;

    // Here goes Unity "Custom" types such as VectorN (Where N means 2, 3) Quaternion or Color
    public SerializableVector3 pos;
}
