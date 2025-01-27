using UnityEngine;

namespace FSM.Scripts
{
    public class FsmStateRun : FsmStateMovement
    {
        public FsmStateRun(Fsm fsm, Transform transform, float speed) : base(fsm, transform, speed) { }
        private FsmController _fsmController;
        public override void Enter()
        {
            _fsmController = GameObject.FindGameObjectWithTag("Player").GetComponent<FsmController>();
            _fsmController._animator.Play("Running");
        }
        public override void Update()
        {
            Debug.Log($"Run state [UPDATE] with speed {Speed}");

            var inputDirection = ReadInput();

            if (inputDirection.sqrMagnitude == 0f)
            {
                Fsm.SetState<FsmStateIdle>();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Fsm.SetState<FsmStateWalk>();
            }
            
            Move(inputDirection);
        }
    }
}