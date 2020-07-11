using UnityEngine;

[System.Serializable]
public class PlayerData
{
   // TODO: Implement levels system
   // public int level;

   public int health;
   public float[] position;

   public PlayerData(PlayerController playerController)
   {
      health = playerController.currentHealth;
      
      position = new float[3];
      position[0] = playerController.transform.position.x;
      position[1] = playerController.transform.position.y;
      position[2] = playerController.transform.position.z;
   }

}
