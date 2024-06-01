using CodeBase.Services;
using CodeBase.Services.SaveLoad;
using StarterAssets;
using UnityEngine;

namespace CodeBase.GameLogic
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FirstPersonController player))
            {
                AllServices.Container.Single<ISaveLoadService>().SaveProgress();
                Debug.Log("Save complete");
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            if (_collider == null)
                return;

            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }
    }
}
