using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float _hp = 10;
    public float hp
    {
        get { return _hp; }
    }
    public float maxHealth = 100;

    public UnityEvent OnDeath;

    public void Hit(float amount)
    {
        _hp -= amount;
        if (_hp <= 0)
            OnDeath.Invoke();
    }

    public void Heal(float amount)
    {
        _hp = Mathf.Min(_hp + amount, maxHealth);
    }
}
