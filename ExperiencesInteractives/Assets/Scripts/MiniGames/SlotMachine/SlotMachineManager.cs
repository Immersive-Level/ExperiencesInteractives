using UnityEngine;
using UnityEngine.UI;
using SlotMachine.Core;
using SlotMachineComponent = SlotMachine.Core.SlotMachine;

public class SlotMachineManager : MonoBehaviour
{
    [SerializeField] private SlotMachineComponent _slotMachine;
    [SerializeField] private Button _spinButton;
    [SerializeField] private Text _creditsText;
    [SerializeField] private Text _winText;

    private void Start()
    {
        // Inicializar máquina tragamonedas
        _slotMachine.Initialize();
        _slotMachine.RegisterSpinCompleteCallback(OnSpinComplete);

        // Configurar botón
        _spinButton.onClick.AddListener(OnSpinButtonClicked);

        // Actualizar UI
        UpdateCreditsUI();
        _winText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _slotMachine.UnregisterSpinCompleteCallback(OnSpinComplete);
        _spinButton.onClick.RemoveListener(OnSpinButtonClicked);
    }

    private void OnSpinButtonClicked()
    {
        _spinButton.interactable = false;
        _winText.gameObject.SetActive(false);
        _slotMachine.Spin();
    }

    private void OnSpinComplete(bool isWin, int reward)
    {
        _spinButton.interactable = true;
        UpdateCreditsUI();

        if (isWin)
        {
            _winText.text = $"¡Ganaste {reward} créditos!";
            _winText.gameObject.SetActive(true);
        }
    }

    private void UpdateCreditsUI()
    {
        _creditsText.text = $"Créditos: {_slotMachine.CurrentCredits}";
    }
}