using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagment
{
    public interface IAssetProvider : IService
    {
        TComponent Instantiate<TComponent>(string path) where TComponent : Object;
        TComponent Instantiate<TComponent>(string path, Vector3 position) where TComponent : Object;
    }
}