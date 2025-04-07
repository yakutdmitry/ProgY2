using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameData : ScriptableObject
{
        [Header("Player and Projectile")] 
        public Vector3 playerPosition;
        public float walkSpeed;
        public float runSpeed;
        public float projectileSpeed;

        [Header("Stats")]
        public int projectilesFired;
        public int attacksPerformed;
        public int pointsSet;
        public int ultimatesPerformed;
        public int enemiesKilled;

        [Header("Enemies")] 
        public float enemyPointRange;
        public float enemyCooldown;
        public float enemyBaseSpeed;
        public float enemySightDistance;
        public float enemyAttackDistance;

        [Header("Environment")]
        public int objectsLifted;
        public float forceMultiplier;
        public float liftDuration;
        


}
