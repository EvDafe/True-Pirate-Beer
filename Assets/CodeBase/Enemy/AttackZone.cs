using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(Collider))]
    public class AttackZone : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _trigger;
        [SerializeField] private Attack _attack;

        private void Start()
        {
            _attack.Disable();
            _trigger.TriggerEnter += TriggerEntered;
            _trigger.TriggerExit += TriggerExit;
        }

        private void TriggerExit(Collider obj) => 
            _attack.Disable();

        private void TriggerEntered(Collider obj) => 
            _attack.Enable();
    }
}
