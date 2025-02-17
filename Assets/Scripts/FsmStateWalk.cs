using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FSM.Scripts
{
    public class FsmStateWalk : FsmStateMovement
    {
        public FsmStateWalk(Fsm fsm, Transform transform, float speed) : base(fsm, transform, speed) { }
        
        private FsmController _fsmController;

        public override void Enter()
        {
            _fsmController = GameObject.FindGameObjectWithTag("Player").GetComponent<FsmController>();
            // _fsmController._animator.Play("Walking");
        }
        public override void Update()
        {
            Debug.Log($"Walk state [UPDATE] with speed {Speed}");

            var inputDirection = ReadInput();

            if (inputDirection.sqrMagnitude == 0f)
            {
                Fsm.SetState<FsmStateIdle>();
                
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Fsm.SetState<FsmStateRun>();
            }
            Move(inputDirection);
        }
    }
}