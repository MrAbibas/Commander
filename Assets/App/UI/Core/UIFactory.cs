using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.UI.Core
{
    public class UIFactory
    {
        private Transform _uiRoot;
    
        private readonly IObjectResolver _resolver;
        private readonly UIAssetsProvider _assetsProvider;

        public UIFactory(IObjectResolver resolver,
            UIAssetsProvider assetsProvider,
            GameObject uiRoot)
        {
            _resolver = resolver;
            _assetsProvider = assetsProvider;
            _uiRoot = uiRoot.transform;
        }

        public T CreateWindow<T>() where T: BaseWindow
        {
            var window = InstantiateWindow<T>();
            window.Init();
            return window;
        }

        public Transform UIRoot()
        {
            if (!_uiRoot)
                _uiRoot = Object.Instantiate(_assetsProvider.UIAssetsConfig.uiRoot).transform;

            return _uiRoot;
        }

        private T InstantiateWindow<T>() where T : BaseWindow
        {
            var prefab = WindowPrefab<T>();
            var instance = _resolver.Instantiate(prefab, UIRoot());
            return instance as T;
        }

        private BaseWindow WindowPrefab<T>() =>
            _assetsProvider.UIAssetsConfig.Windows[typeof(T)];
    }
}