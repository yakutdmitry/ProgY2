using System;
using UnityEngine;

namespace FSM.Scripts
{
    public class FsmController : MonoBehaviour
    {
        private Fsm _fsm;
        
        public Animator _animator;
        public Rigidbody _enemyRigid;
        public Collider _collider;
        public SkinnedMeshRenderer _skin;
        
        
        
        private float _walkSpeed = 10f;
        private float _runSpeed = 20f;
        public int AttackDuration = 3;
        [SerializeField] private float visionDuration = 4.5f;
        public bool Ulting;

        private void Start()
        {
            _fsm = new Fsm();

            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider>();
            // _enemyRigid = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Rigidbody>();
            
            _fsm.AddState(new FsmStateIdle(_fsm));
            _fsm.AddState(new FsmStateWalk(_fsm, transform, _walkSpeed));
            _fsm.AddState(new FsmStateRun(_fsm, transform, _runSpeed));
            _fsm.AddState(new FsmStateTest(_fsm));
            _fsm.AddState(new FsmStateVision(_fsm, _skin, visionDuration));
            
            _fsm.SetState<FsmStateIdle>();
        }

        private void Update()
        {
            _fsm.Update();
            // Debug.Log(anim);

            if (Input.GetKeyDown(KeyCode.T)){_fsm.SetState<FsmStateTest>();}
            if(Input.GetKeyDown(KeyCode.Q)){_fsm.SetState<FsmStateVision>();}
            
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log(("COLLISION"));
            if (other.gameObject.CompareTag("Enemy") && Ulting)
            {
                Debug.Log("ENEMY DETECTED");
                Destroy(other.gameObject);
            }
        }
    }
}