using UnityEngine;
[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameData : ScriptableObject
{
        public float walkSpeed;
        public float runSpeed;
        public float projectileSpeed;        
        
        public int projectilesFired;
        public int ultCounter;
        public int enemiesKilled;
        

        
}
