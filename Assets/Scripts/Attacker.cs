using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacker : Health
{

    [SerializeField] public  BeatAttack[] _attacks;
    [SerializeField] public  BeatAttack[] _Defenses;
    
    [SerializeField] private Attacker target;

    [SerializeField] private int AttackStartBeat;
    [SerializeField] private int AttackCurrentBeat;
    [SerializeField] private int BeatRemaining;
    public Attacker ChooseCurrentTarget
    {
        get { return target; }
        set
        {
            if (target == value) return;

            target = value;
        }
    }

    [SerializeField] public int currentAttack =0;    
    public int ChoosecurrentAttack
    {
        get { return currentAttack; }
        set
        {
            if (currentAttack == value) return;
                       
            currentAttack = value;

            HandleNewAttack();
        }
    }

    private void HandleNewAttack()
    {
        AttackCurrentBeat = 0;
        AttackStartBeat = BeatManager._instance.playedBeat +1;
    }

    [SerializeField] public int currentDefense =0;
    public int ChooseCurrentDefense
    {
        get { return currentDefense; }
        set
        {
            if (currentDefense == value) return;

            currentDefense = value;
        }
    }


    [SerializeField] private Image defenseSprite;
    [SerializeField] private Image attackSprite;
    
    
    public void Beat()
    {
        if (hp <= 0)
            return;
        
        int NumDamagesDEF = _Defenses[currentDefense].Damages.Length;
        int currentBeatDEF = BeatManager._instance.playedBeat % NumDamagesDEF;
        BeatAttack.BeatDamageProperties DefDamege = _Defenses[currentDefense].Damages[currentBeatDEF];
        if (defenseSprite != null)
        {
            defenseSprite.sprite = DefDamege.sprite;
        }
        base.healthBar.UpdateDefenseStatus(DefDamege.sprite);

        // TODO : Animate Defense


        
        // TODO : move this after sprites?
        
        //if (!target)
        //    return;
        
        // TODO : Animate Attack Preperation.


        // Attack Target
        // TODO check if current attack is less then number of attacks return  


        // TODO check if attacker is dead and return; 
        int NumDamagesATK = _attacks[currentAttack].Damages.Length;
        //int currentBeatATK = BeatManager._instance.playedBeat % NumDamagesATK;
        //BeatAttack.BeatDamageProperties AtkDamage = _attacks[currentAttack].Damages[currentBeatATK];
        BeatAttack.BeatDamageProperties AtkDamage = _attacks[currentAttack].Damages[AttackCurrentBeat];

        
        if (attackSprite != null)
        {           
            attackSprite.sprite= AtkDamage.sprite;          
        }
        base.healthBar.UpdateAttackStatus(AtkDamage.sprite);

        BeatRemaining = NumDamagesATK - AttackCurrentBeat;
        if (BeatRemaining > 0 && currentAttack != 0) 
        {
            healthBar.BeatCounter.text = BeatRemaining.ToString();
        }
        else
        {
            healthBar.BeatCounter.text = "";
        }
        
        
        
        AttackCurrentBeat++;
        
        // Perform Attack
        switch (AtkDamage._damageType)        
        {
            case BeatAttack.BeatDamageProperties.DamageType.FullDamage:
            {

                if (!target)
                {
                    break;                    
                }
                        
                int NumDamagesDEFTaret = target._Defenses[target.currentDefense].Damages.Length;
                int currentBeatDEFTarget = BeatManager._instance.playedBeat % NumDamagesDEFTaret;
                BeatAttack.BeatDamageProperties DefDamegeTarget = target._Defenses[target.currentDefense].Damages[currentBeatDEFTarget];


                
                switch (DefDamegeTarget._damageType)
                {
                    case BeatAttack.BeatDamageProperties.DamageType.FullDamage:
                    {
                        // TODO - Make better forumla that also relies on Defense. for example substruct or devide defense.
                        float damageTaken = Mathf.Max(AtkDamage.Strentgh - DefDamegeTarget.Strentgh, 0); 
                        target.Hit(damageTaken);
                        
                        
                        // TODO: Animate Attack
                        
                        break;
                    }
                    case BeatAttack.BeatDamageProperties.DamageType.NoDamage:
                    {
                        float damageTaken = AtkDamage.Strentgh;
                        target.Hit(damageTaken);
                        break;
                    }                    
                }
                break;
            }
            case BeatAttack.BeatDamageProperties.DamageType.Move:
            {
                MovementByGrid moveGrid = GetComponent<MovementByGrid>();
                if (moveGrid)
                {
                    moveGrid.MoveCharacter(AtkDamage.moveDirection);
                }
                break;
            } 
            case BeatAttack.BeatDamageProperties.DamageType.Loop:
            {
                AttackCurrentBeat = 0;
                break;
            }

        }




        if (AttackCurrentBeat >= NumDamagesATK)
        {            
            AttackCurrentBeat = 0;
            ChoosecurrentAttack = 0;
        }

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        if (target == null)
        {
            if (tag == "Enemy")
            {
                foreach (Attacker attacker in FindObjectsOfType<Attacker>())                    
                {
                    if (attacker.tag == "Player")
                    {
                        target = attacker;
                        break;
                    } 
                }
            }
        }
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
