using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Presentation.Views
{
    [RequireComponent(typeof(UIDocument))]
    public class HeroUpgradeView : MonoBehaviour
    {
        private Label _levelLabel;
        private Label _healthLabel;
        private Label _strengthLabel;
        private Button _upgradeButton;
        private VisualElement _root;

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _levelLabel = _root.Q<Label>("Level_text");
            _healthLabel = _root.Q<Label>("Health_text");
            _strengthLabel = _root.Q<Label>("Strength_text");
            _upgradeButton = _root.Q<Button>("Upgrade_button");

            _upgradeButton.clicked += () => Debug.Log("[HeroUpgradeView] Upgrade button clicked");
        }

        public void Show() => _root.style.display = DisplayStyle.Flex;
        public void Hide() => _root.style.display = DisplayStyle.None;

        public void UpdateStats(int level, int health, int strength)
        {
            _levelLabel.text = $"Level: {level}";
            _healthLabel.text = $"Health: {health}";
            _strengthLabel.text = $"Strength: {strength}";
        }

        public void SubscribeUpgradeButton(Action callback) => _upgradeButton.clicked += callback;
        public void UnsubscribeUpgradeButton(Action callback) => _upgradeButton.clicked -= callback;
    }
}