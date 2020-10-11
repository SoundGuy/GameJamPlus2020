using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Image attackImage;
    [SerializeField] Image defenseImage;
    
    [SerializeField] TextMeshProUGUI DamageReceived;
    [SerializeField] TextMeshProUGUI DamageBlocked;
    
    [SerializeField] public TextMeshProUGUI BeatCounter;

    Animator animator;

    public void UpdateHealth(float hp, float maxHp)
    {
        float normalizedHealth = (maxHp != 0) ? hp / maxHp : 0;
        if (!animator) animator = GetComponent<Animator>();
        animator.SetFloat("Health", normalizedHealth);
        text.text = $"{hp} / {maxHp}";
    }

    public void UpdateDefenseStatus(Sprite sprite)
    {
        defenseImage.sprite = sprite ? sprite : defaultSprite;
    }

    public void UpdateAttackStatus(Sprite sprite)
    {
        attackImage.sprite = sprite ? sprite : defaultSprite;
    }
}
