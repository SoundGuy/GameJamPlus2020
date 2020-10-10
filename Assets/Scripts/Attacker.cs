using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacker : Health
{

    [SerializeField] public  BeatAttack[] _attacks;
    [SerializeField] public  BeatAttack[] _Defenses;
    
    [SerializeField] private Attacker target;
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
        }
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
        print($"{gameObject.name} - target: {target}, hp: {hp}");
        if (!target || hp <= 0)
            return;
        
        // TODO : Animate Defense
        
        int NumDamagesDEF = _Defenses[currentDefense].Damages.Length;
        int currentBeatDEF = BeatManager._instance.playedBeat % NumDamagesDEF;
        BeatAttack.BeatDamageProperties DefDamege = _Defenses[currentDefense].Damages[currentBeatDEF];
        
        if (defenseSprite != null)
        {           
            defenseSprite.sprite= DefDamege.sprite;          
        }
        
        // TODO : Animate Attack Preperation.
        
        
        // Attack Target
        // TODO check if current attack is less then number of attacks return  
        
                       
        
        // TODO check if attacker is dead and return; 
               
        int NumDamagesATK = _attacks[currentAttack].Damages.Length;
        int currentBeatATK = BeatManager._instance.playedBeat % NumDamagesATK;
        BeatAttack.BeatDamageProperties AtkDamage = _attacks[currentAttack].Damages[currentBeatATK];
        
        
        if (attackSprite != null)
        {           
            attackSprite.sprite= AtkDamage.sprite;          
        }
        
        
        int NumDamagesDEFTaret = target._Defenses[target.currentDefense].Damages.Length;
        int currentBeatDEFTarget = BeatManager._instance.playedBeat % NumDamagesDEFTaret;
        BeatAttack.BeatDamageProperties DefDamegeTarget = target._Defenses[target.currentDefense].Damages[currentBeatDEFTarget];
        
        
        switch (AtkDamage._damageType)        
        {
            case BeatAttack.BeatDamageProperties.DamageType.FullDamage:
            {
                switch (DefDamegeTarget._damageType)
                {
                    case BeatAttack.BeatDamageProperties.DamageType.FullDamage:
                    {
                        // TODO - Make better forumla that also relies on Defense. for example substruct or devide defense.
                        float damageTaken = AtkDamage.Strentgh; 
                        target.Hit(damageTaken);
                        
                        
                        // TODO: Animate Attack
                        
                        break;
                    }
                }
                break;
            }

        }

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
