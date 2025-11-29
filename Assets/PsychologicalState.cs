using UnityEngine;
using TMPro;

public class PsychologicalState : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    public TMP_Text psyche;

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            update_morale();
        }
    }

    void Start()
    {
        update_morale();
    }

    void update_morale()
    {
        psyche.text = "Morale: " + _health;
    }

    public void decrement_health()
    {
        Health -= 10;  
    }

    public void increment_health()
    {
        Health += 10;    
    }
}
