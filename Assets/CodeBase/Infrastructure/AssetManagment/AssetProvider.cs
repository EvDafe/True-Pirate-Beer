using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagment
{
    public class AssetProvider : IAssetProvider
    {
        public TComponent Instantiate<TComponent>(string path) where TComponent : Object
        {
            TComponent prefab = Resources.Load<TComponent>(path);
            return Object.Instantiate(prefab);
        }

        public TComponent Instantiate<TComponent>(string path, Vector3 position) where TComponent : Object
        {
            TComponent prefab = Resources.Load<TComponent>(path);
            return Object.Instantiate(prefab, position, Quaternion.identity);
        }
    }
}
