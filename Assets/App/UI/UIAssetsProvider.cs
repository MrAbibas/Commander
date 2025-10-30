using UnityEngine;

namespace App.UI
{
    public class UIAssetsProvider
    {
        private const string UI_ASSETS_CONFIG_PATH = "UI/UIAssetsConfig";
        private UIAssetsConfig _uiAssetsConfig;

        public UIAssetsConfig UIAssetsConfig
        {
            get
            {
                _uiAssetsConfig ??= LoadUIAssetsConfig();
                return _uiAssetsConfig;
            }
        }

        private UIAssetsConfig LoadUIAssetsConfig()
        {
            return Resources.Load<UIAssetsConfig>(UI_ASSETS_CONFIG_PATH);
        }
    }
}