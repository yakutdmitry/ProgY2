using UnityEngine;
[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameData : ScriptableObject
{
        public float walkSpeed;
        public float runSpeed;

        public int ultCounter;
        public int enemiesKilled;
        
        public float projectileSpeed;
        public float projectilesFird;
}
