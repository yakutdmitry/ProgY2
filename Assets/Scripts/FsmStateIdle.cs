using UnityEngine;

namespace FSM.Scripts
{
    public class FsmStateIdle : FsmState
    {
        public FsmStateIdle(Fsm fsm) : base(fsm) { }
        private FsmController _fsmController;
        
        public override void Enter()
        {
            _fsmController = GameObject.FindGameObjectWithTag("Player").GetComponent<FsmController>();
            Debug.Log("Idle state [ENTER]");
            
        }
        
        public override void Exit()
        {
            Debug.Log("Idle state [UPDATE]");
        }
        
        public override void Update()
        {
            
            Debug.Log("Idle state [UPDATE]");

            if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
            {
                Fsm.SetState<FsmStateWalk>();
            }
            
            _fsmController._animator.SetFloat("Velocity", 0);
        }
    }
}