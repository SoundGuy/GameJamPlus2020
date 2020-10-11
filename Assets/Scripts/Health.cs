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
        
        DisplayDamageTaken(amount);
        
    }       

    void DisplayDamageTaken(float amount)
    {

        float effectLength = BeatManager.BeatLength * 0.8f; // put this parameter exposed  
        // Hit Points Text
        healthBar.DamageReceived.gameObject.SetActive(true);
        healthBar.DamageReceived.text = "-" + amount.ToString();
        Vector3 OrigPos = healthBar.DamageReceived.transform.localPosition;
        LeanTween.moveLocalY(healthBar.DamageReceived.gameObject, OrigPos.y+ healthBar.DamageReceivedOffeset,
            effectLength);
        LeanTween.value(healthBar.DamageReceived.gameObject, 1f, 0, effectLength).setOnUpdate((float value) =>
        {
            healthBar.DamageReceived.alpha = value;
        }).setOnComplete(() =>
        {
            healthBar.DamageReceived.gameObject.SetActive(false);
            
            healthBar.DamageReceived.transform.localPosition = OrigPos;
            healthBar.DamageReceived.alpha = 1;
        });
    }


    public void BlockedAttack()
    {
        float effectLength = BeatManager.BeatLength * 0.8f; // put this parameter exposed  
        // Hit Points Text
        healthBar.DamageBlocked.gameObject.SetActive(true);
        //healthBar.DamageReceived.text = "-" + amount.ToString();
        Vector3 OrigPos = healthBar.DamageBlocked.transform.localPosition;
        LeanTween.moveLocalY(healthBar.DamageBlocked.gameObject, OrigPos.y+ healthBar.DamageBlockeddOffeset,
            effectLength);
        LeanTween.value(healthBar.DamageBlocked.gameObject, 1f, 0f, effectLength).setOnUpdate((float value) =>
        {
            healthBar.DamageBlocked.alpha = value;
        }) .setOnComplete(() =>                
        {
            healthBar.DamageBlocked.gameObject.SetActive(false);
            
            healthBar.DamageBlocked.transform.localPosition = OrigPos;
            healthBar.DamageBlocked.alpha = 1;
        });
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
