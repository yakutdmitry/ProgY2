using System.Collections;
using UnityEngine;
using Input = UnityEngine.Windows.Input;

namespace FSM.Scripts
{
    public class FsmStateUltAttack : FsmState
    {
        private FsmController _fsmController;
        
        public FsmStateUltAttack(Fsm fsm, int AttackDuration) : base(fsm) { }
        public int AttackDuration;
        public override void Enter()
        {
            _fsmController = GameObject.FindGameObjectWithTag("Player").GetComponent<FsmController>();
            _fsmController.Ulting = true;
           
            Debug.Log("ULTING ENTER");
            
        }

        public override void Update()
        {
            Debug.Log("Player is Ulting");
            _fsmController._animator.Play("Ulting");
            if (_fsmController._animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                Fsm.SetState<FsmStateIdle>();
            }
            
        }
        
        
    }
}