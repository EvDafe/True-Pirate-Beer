using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private Follow _follow;
        [SerializeField] private TriggerObserver _trigger;
        [SerializeField] private float _cooldown;

        private Coroutine _switchOffCoroutine;

        private void OnEnable()
        {
            _trigger.TriggerEnter += TriggerEnter;
            _trigger.TriggerExit += TriggerExit;
            SwitchFollowOff();
        }

        private void OnDisable()
        {
            _trigger.TriggerEnter -= TriggerEnter;
            _trigger.TriggerExit -= TriggerExit;
        }

        private void TriggerEnter(Collider obj)
        {
            if(_switchOffCoroutine != null)
                StopCoroutine(_switchOffCoroutine);
            SwitchFollowOn();
        }

        private IEnumerator SwitchFollowOffOnCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            SwitchFollowOff();
        }

        private void TriggerExit(Collider obj) =>
            _switchOffCoroutine = StartCoroutine(SwitchFollowOffOnCooldown());

        private void SwitchFollowOn() => 
            _follow.enabled = true;

        private void SwitchFollowOff() => 
            _follow.enabled = false;
    }
}
