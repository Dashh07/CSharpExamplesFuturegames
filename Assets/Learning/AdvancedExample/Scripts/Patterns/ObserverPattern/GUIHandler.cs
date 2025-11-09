using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Just an example. This approach is not recommended in production
public class GUIHandler : MonoBehaviour {
    public Button button;
    public TextMeshProUGUI text;
    private PlayerReference player;

    public static Action<int> OnUpdateUI;

    private void OnEnable() {
        Signals.Get<UpdateUI>().AddListener(UpdateHealth);
        OnUpdateUI += UpdateHealth;
    }
    private void Start() {
        button.onClick.AddListener(() => {
            Signals.Get<DamageSignal>().Dispatch(10);
        });
        button.onClick.AddListener(() => { ObserverExamples.OnDamageAction?.Invoke(10); });
    }

private void UpdateHealth(int value) {
        player ??= FindFirstObjectByType<PlayerReference>(); // bad practice. The entire UI behavior should be refactored
        text.text = value.ToString();
        if(player) {
            player.playerUIReference.healthBar.fillAmount = value / 100f;
        }
    }

    private void OnDisable() {
        Signals.Get<UpdateUI>().RemoveListener(UpdateHealth);
        button.onClick.RemoveAllListeners();
        OnUpdateUI -= UpdateHealth;
    }
}
