using System;
using System.IO;
using UnityEngine;

namespace FSM.Scripts
{
    public class FsmController : MonoBehaviour
    {
        private Fsm _fsm;
        private float _walkSpeed;
        private float _runSpeed;
        public int AttackDuration = 3;
        [SerializeField] private float visionDuration = 4.5f;
        public bool Ulting;
        
        public AudioSource _soudns;
        public Animator _animator;
        public GameObject Targets;
        
        public GameData _gameData;
        private string _filePath;

        
        
        // private void LoadData()
        // {
        //     if (File.Exists(_filePath))
        //     {
        //         string json = File.ReadAllText(_filePath); 
        //         JsonUtility.FromJsonOverwrite(json, _gameData);
        //     }
        //     else
        //     {
        //         Debug.Log("No file found");
        //     }
        // }
        
        private void Start()
        {
            _filePath = Path.Combine(Application.persistentDataPath, "Data.json");
            
            DataManager.loadData(_gameData, "Data.json");   
            
            _walkSpeed = _gameData.walkSpeed;
            _runSpeed = _gameData.runSpeed;
            
            _fsm = new Fsm();
            _animator = GetComponent<Animator>();
            _soudns = GetComponent<AudioSource>();
            _fsm.AddState(new FsmStateIdle(_fsm));
            _fsm.AddState(new FsmStateWalk(_fsm, transform, _walkSpeed));
            _fsm.AddState(new FsmStateRun(_fsm, transform, _runSpeed));
            _fsm.AddState(new FsmStateTest(_fsm));
            _fsm.AddState(new FsmStateVision(_fsm, Targets, visionDuration));
            
            _fsm.SetState<FsmStateIdle>();
        }

        private void Update()
        {
            _fsm.Update();
            // Debug.Log(anim);

            if (Input.GetKeyDown(KeyCode.T)){_fsm.SetState<FsmStateTest>();}

            if (Input.GetKeyDown(KeyCode.Q))
            {
                _gameData.ultCounter++;
                DataManager.saveData(_gameData, "Data.json");  
                _fsm.SetState<FsmStateVision>();
            }
            
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log(("COLLISION"));
            if (other.gameObject.CompareTag("Enemy") && Ulting)
            {
                Debug.Log("ENEMY DETECTED");
                Destroy(other.gameObject);
                _gameData.enemiesKilled++; 
                DataManager.saveData(_gameData, "Data.json");
            }
        }

        // private void SaveData()
        // {
        //     string json = JsonUtility.ToJson(_gameData);
        //     File.WriteAllText(_filePath, json);
        //     Debug.Log(_filePath);
        //     
        // }

        private void OnApplicationQuit()
        {
            DataManager.saveData(_gameData, "Data.json");
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            DataManager.saveData(_gameData, "Data.json");
        }
    }
}