using App.UI.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace App.UI.StartFightPopUp
{
    public class StartFightPopUp: BaseWindow
    {
        [SerializeField] private Button fightButton;
        public UnityEvent OnClick => fightButton.onClick;
    }
}