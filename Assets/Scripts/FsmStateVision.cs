using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace FSM.Scripts
{
    public class FsmStateVision : FsmState
    {
        protected readonly GameObject TargetGroup;
        protected float Duration;
        private float _vDur;
        // private SkinnedMeshRenderer _meshRenderer;
        
        public FsmStateVision(Fsm fsm, GameObject _targetGroup, float _duration) : base(fsm)
        {
            TargetGroup = _targetGroup;
            Duration = _duration;
        }
        
        public override void Enter()
        {
            foreach (var renderer in TargetGroup.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                renderer.material.EnableKeyword("_EMISSION");
                Debug.Log("ENABLED");
            }
            RenderSettings.fog = true;
            
            // _meshRenderer = TargetGroup.GetComponentInChildren<SkinnedMeshRenderer>();
            Debug.Log("Entered FsmStateVision");
            // _meshRenderer.material.EnableKeyword("_EMISSION");
            _vDur = Duration;
            Disable();
            Fsm.SetState<FsmStateIdle>();
        }

        public override void Update()
        {
            
             // _vDur -= Time.deltaTime;
             // if (_vDur <= 0)
             // {
             //     Fsm.SetState<FsmStateIdle>();
             // } // TIMER
             //
             
        }
        public override void Exit()
        {
            // Debug.Log("Exiting FsmStateVision");
            // foreach (var renderer in TargetGroup.GetComponentsInChildren<SkinnedMeshRenderer>())
            // {
            //     renderer.material.DisableKeyword("_EMISSION");
            // }
            // RenderSettings.fog = false;
        }

        private async void Disable()
        {
            await Task.Delay(3000);
            Debug.Log("Exiting FsmStateVision");
            foreach (var renderer in TargetGroup.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                renderer.material.DisableKeyword("_EMISSION");
            }
            RenderSettings.fog = false;
        }
    }
}