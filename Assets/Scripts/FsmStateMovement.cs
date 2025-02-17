using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FSM.Scripts
{
    public class FsmStateMovement : FsmState
    {
        protected readonly Transform Transform;
        protected readonly float Speed;
        private FsmController _fsmController;

        public FsmStateMovement(Fsm fsm, Transform transform, float speed) : base(fsm)
        {
            Transform = transform;
            Speed = speed;
            
        }

        public override void Enter()
        {
            Debug.Log($"Movement ({this.GetType().Name}) state [ENTER]");
            _fsmController = GameObject.FindGameObjectWithTag("Player").GetComponent<FsmController>();
        }
        
        public override void Exit()
        {
            Debug.Log($"Movement ({this.GetType().Name}) state [EXIT]");
        }
        
        public override void Update()
        {
            Debug.Log($"Movement ({this.GetType().Name}) state [UPDATE] with speed {Speed}");

            var inputDirection = ReadInput();

            if (inputDirection.sqrMagnitude == 0)
            {
                Debug.Log("no input");
                Fsm.SetState<FsmStateIdle>();
                
            }

            Move(inputDirection);
            
        }

        protected Vector2 ReadInput()
        {
            var inputHorizontal = Input.GetAxis("Horizontal");
            var inputVertical = Input.GetAxis("Vertical");
            var inputDirection = new Vector2(inputHorizontal, inputVertical);

            return inputDirection;
        }

        protected virtual void Move(Vector2 inputDirection)
        {
            Transform.position += new Vector3(inputDirection.x, 0f, inputDirection.y) * (Speed * Time.deltaTime);
            // Debug.Log(Speed * Time.deltaTime);
            _fsmController = GameObject.FindGameObjectWithTag("Player").GetComponent<FsmController>();
            _fsmController._animator.SetFloat("Velocity", Speed * Time.deltaTime);
            
        }
        
    }
}