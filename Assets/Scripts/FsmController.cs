using System;
using UnityEngine;

namespace FSM.Scripts
{
    public class FsmController : MonoBehaviour
    {
        private Fsm _fsm;
        private float _walkSpeed = 10f;
        private float _runSpeed = 20f;
        public Animator _animator;
        public Rigidbody _enemyRigid;
        public Collider _collider;
        public bool Ulting;
        public int AttackDuration = 3;

        private void Start()
        {
            _fsm = new Fsm();

            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider>();
            // _enemyRigid = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Rigidbody>();
            
            _fsm.AddState(new FsmStateIdle(_fsm));
            _fsm.AddState(new FsmStateWalk(_fsm, transform, _walkSpeed));
            _fsm.AddState(new FsmStateRun(_fsm, transform, _runSpeed));
            _fsm.AddState(new FsmStateUltAttack(_fsm, AttackDuration));
            _fsm.AddState(new FsmStateTest(_fsm));
            
            _fsm.SetState<FsmStateIdle>();
        }

        private void Update()
        {
            _fsm.Update();
            // Debug.Log(anim);

            if (Input.GetKeyDown(KeyCode.T)){_fsm.SetState<FsmStateTest>();}
            
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