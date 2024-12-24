using UnityEngine;
using UnityEngine.UI;

public class HelthRound : MonoBehaviour
{
    [SerializeField] private Image _healthCircle; 
    [SerializeField] private Rock _rock;

    private void OnEnable()
    {
        _rock.HealthChanged += OnValueChanged;
        _healthCircle.fillAmount = 1;
    }

    private void OnDisable()
    {
        _rock.HealthChanged -= OnValueChanged;
    }

    private void OnValueChanged(int value, int maxValue)
    {
        _healthCircle.fillAmount = (float)value / maxValue; 
    }
}
