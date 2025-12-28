using UnityEngine;
using TMPro;

using UnityEngine.UI;

public class PsychologicalState : MonoBehaviour
{
    // [SerializeField] private PlayerStatistics playerStats;
    // [SerializeField] private int _health = 100;
    // public TMP_Text psyche;
    // public Button restButton;
    // public int Health
    // {
    //     get => _health;
    //     set
    //     {
    //         _health = value;
    //         update_morale();
    //     }
    // }

    // void Start()
    // {
    //     restButton.onClick.AddListener(increment_health);
    //     if (playerStats != null)
    //     {
    //         _health = playerStats.StartingMorale;
    //     }
    //     else
    //     {
    //         Debug.LogWarning("PlayerStatistics reference missing on PsychologicalState, using default starting morale.");
    //     }

    //     update_morale();
    // }

    // void update_morale()
    // {
    //     psyche.text = "Morale: " + _health;
    // }

    // public void decrement_health()
    // {
    //     int moraleStep = playerStats != null ? playerStats.MoraleStep : 10;
    //     Health -= moraleStep;  
    // }

    // public void increment_health()
    // {
    //     int moraleStep = 10;
    //     Health = Mathf.Min(playerStats.MaxMorale, Health + moraleStep);  
    // }
}
