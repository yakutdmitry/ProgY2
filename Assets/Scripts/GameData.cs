using Unity.VisualScripting;
using UnityEngine;
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
        public int ultCounter;
        public int enemiesKilled;

        [Header("Enemies")] 
        public float enemyPointRange;
        public float enemyCooldown;
        public float enemyBaseSpeed;
        public float enemySightDistance;
        public float enemyAttackDistance;
        


}
