using System;
using UnityEngine;

namespace FSM.Scripts
{
    public class FsmStateTest : FsmState
    {
        public FsmStateTest(Fsm fsm) : base(fsm) { }
        private FsmController _fsmController;

        public override void Enter()
        {
            _fsmController = GameObject.FindGameObjectWithTag("Player").GetComponent<FsmController>();
            _fsmController._animator.Play("Ulting");
            _fsmController.Ulting = true;
        }

        public override void Update()
        {
            Debug.Log("Test State UPDATE");
            if (!_fsmController._animator.GetCurrentAnimatorStateInfo(0).IsName("Ulting"))
            {
                _fsmController.Ulting = false;
                Fsm.SetState<FsmStateIdle>();
            }
        }

        public override void Exit()
        {
        }
        
    }
}