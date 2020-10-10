using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] float _hp = 100;


    [SerializeField] private LevelManager _levelManager;

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
    protected virtual void Start()
    {
        UpdateHP();
        _levelManager = FindObjectOfType<LevelManager>();
    }
    
    public void Hit(float amount)
    {
        if (_hp <= 0) return;

        _hp = Mathf.Max(_hp - amount, 0);
        UpdateHP();
        if (_hp <= 0) // DEAD
        {            
            OnDeath.Invoke();
            if (_levelManager)
            {
                _levelManager.AnnouceDeath(this);
            }
        }
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
