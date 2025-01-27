using Unity.VisualScripting;
using UnityEngine;

namespace FSM.Scripts
{
    public class FsmStateVision : FsmState
    {
        protected readonly SkinnedMeshRenderer MeshRenderer;
        protected float Duration;
        private float _vDur;
        
        public FsmStateVision(Fsm fsm, SkinnedMeshRenderer _meshRenderer, float _duration) : base(fsm)
        {
            MeshRenderer = _meshRenderer;
            Duration = _duration;
        }
        
        public override void Enter()
        {
            Debug.Log("Entered FsmStateVision");
            MeshRenderer.material.EnableKeyword("_EMISSION");
            _vDur = Duration;
        }

        public override void Update()
        {
            
             _vDur -= Time.deltaTime;
             if (_vDur <= 0)
             {
                 Fsm.SetState<FsmStateIdle>();
             } // TIMER
             
        }
        public override void Exit()
        {
            Debug.Log("Exiting FsmStateVision");
            MeshRenderer.material.DisableKeyword("_EMISSION");
        }
    }
}