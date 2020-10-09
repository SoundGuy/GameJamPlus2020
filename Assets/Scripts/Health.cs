using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] float _hp = 100;

    public float hp
    {
        get { return _hp; }
    }
    [SerializeField] float _maxHealth = 100;
    public float maxHealth
    {
        get { return _maxHealth; }
        set
        {
            if (_maxHealth == value) return;

            _maxHealth = value;
            UpdateHP();
        }
    }

    public UnityEvent OnDeath;
    private void Start()
    {
        UpdateHP();
    }

    public void Hit(float amount)
    {
        _hp = Mathf.Max(_hp - amount, 0);
        UpdateHP();
        if (_hp <= 0)
            OnDeath.Invoke();
    }

    public void Heal(float amount)
    {
        _hp = Mathf.Min(_hp + amount, maxHealth);
        UpdateHP();
    }

    void UpdateHP()
    {
        healthBar.UpdateHealth(_hp, maxHealth);
    }
}
