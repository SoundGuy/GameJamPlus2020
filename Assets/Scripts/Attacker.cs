using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Attacker : Health
{
    [SerializeField] private GameObject SpriteImage;
    [SerializeField] float IncreasePrec = 0.8f;
    [SerializeField] private Sprite UpSprite;
    [SerializeField] private Sprite DownSprite;

    [SerializeField] public  BeatAttack[] _attacks;
    [SerializeField] public  BeatAttack[] _Defenses;
    
    [SerializeField] private Attacker target;

    [SerializeField] private int AttackStartBeat;
    [SerializeField] private int AttackCurrentBeat;
    [SerializeField] private int BeatRemaining;

    private Animator animator;

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


    [SerializeField] private SpriteRenderer defenseSprite;
    [SerializeField] private SpriteRenderer attackSprite;
    
    
    public void Beat()
    {
        if (hp <= 0)
            return;
        
        
        
        // animate Dance

        // TODO Change this when getting animation
        if (SpriteImage)
        {


            BeatSignalReciever beatSignalReciever = FindObjectOfType<BeatSignalReciever>();

            
            
            
            float PrecOfBeat = beatSignalReciever.defPrecOfBeat;

            Vector3 origScale = gameObject.transform.localScale;


            Vector3 toScale = origScale;
            ;
            toScale.y *= IncreasePrec;
            float UpLength = BeatManager.BeatLength * PrecOfBeat * (1f - beatSignalReciever.safetyBuffer);
            float DownLength = BeatManager.BeatLength * (1f - PrecOfBeat) * (1f - beatSignalReciever.safetyBuffer);
            
            
            

            GetComponent<Animator>().Play("Bent");
            
            if (DownSprite)
            {
                SpriteImage.GetComponent<SpriteRenderer>().sprite = DownSprite;
            }

            LeanTween.scale(SpriteImage, toScale, UpLength).setOnComplete(() =>
            {
                LeanTween.scale(SpriteImage, origScale, DownLength);
                if (UpSprite)
                {
                    SpriteImage.GetComponent<SpriteRenderer>().sprite = UpSprite;
                }
            });

        }


        int NumDamagesDEF = _Defenses[currentDefense].Damages.Length;
        int currentBeatDEF = BeatManager._instance.playedBeat % NumDamagesDEF;
        BeatAttack.BeatDamageProperties DefDamege = _Defenses[currentDefense].Damages[currentBeatDEF];
        
        switch (DefDamege._damageType)
        {
            case BeatAttack.BeatDamageProperties.DamageType.NoDamage:
            {
                if (defenseSprite != null )
                {
                    //defenseSprite.sprite = DefDamege.sprite;
                    float timeOn = BeatManager.BeatLength * 0.5f;
                    defenseSprite.gameObject.SetActive(true);
                    LeanTween.value(defenseSprite.gameObject, 0,0, timeOn).setOnComplete(() =>
                    {
                        defenseSprite.gameObject.SetActive(false);    
                    });
                }
                break;
            }
            case BeatAttack.BeatDamageProperties.DamageType.Move:
            {
                //Debug.Log("Moving - " + DefDamege.moveDirection);
                MovementByGrid moveGrid = GetComponent<MovementByGrid>();
                if (moveGrid)
                {
                    moveGrid.MoveCharacter(DefDamege.moveDirection);
                }
                
                break;
            }
            
        }
        
        base.healthBar.UpdateDefenseStatus(DefDamege.sprite);

        

        
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

        
        /*
        if (attackSprite != null)
        {
            
            // attackSprite.sprite= AtkDamage.sprite;          
        }*/        
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
        
        
        
        if (AtkDamage.sound.Length > 0)
        {
            int rand = Random.Range(0, AtkDamage.sound.Length);
            GetComponent<AudioSource>().PlayOneShot(AtkDamage.sound[rand]);
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


                
                // TODO: move to paremetes
                float timeOn = BeatManager.BeatLength * 0.5f;
                    //float timeOff = BeatManager.BeatLength - timeOn; 


                    // TODO: Animate Attack
                    animator.SetTrigger("isAttacking");

                    if (attackSprite)
                {
                    attackSprite.gameObject.SetActive(true);
                    LeanTween.value(attackSprite.gameObject, 0,0, timeOn).setOnComplete(() =>
                    {
                        attackSprite.gameObject.SetActive(false);    
                    });
                            
                }



                    switch (DefDamegeTarget._damageType)
                {
                    case BeatAttack.BeatDamageProperties.DamageType.FullDamage:
                    {
                        // TODO - Make better forumla that also relies on Defense. for example substruct or devide defense.
                        float damageTaken = Mathf.Max(AtkDamage.Strentgh - DefDamegeTarget.Strentgh, 0); 
                        target.Hit(damageTaken);
                        
                        
                        // Hit  Success Sound
                        if (AtkDamage.HitSuccessSound.Length > 0)
                        {
                            int rand = Random.Range(0, AtkDamage.HitSuccessSound.Length);
                            GetComponent<AudioSource>().PlayOneShot(AtkDamage.HitSuccessSound[rand]);
                        }
                        
                        // TODO: Animate Succefull Attack
                        
                        break;
                    }
                    case BeatAttack.BeatDamageProperties.DamageType.NoDamage:
                    {
                        // TODO: Animate Failed Attack                                               
                        // TODO: Animate Failed Sound
                        
                        if (AtkDamage.HitFailSound.Length > 0)
                        {
                            int rand = Random.Range(0, AtkDamage.HitFailSound.Length);
                            GetComponent<AudioSource>().PlayOneShot(AtkDamage.HitFailSound[rand]);
                        }
                        
                        target.BlockedAttack();
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


    public void Die()
    {
        animator.SetBool("isDeath", true);
        //LeanTween.rotateLocal(SpriteImage, Vector3.forward * -90f, BeatManager.BeatLength );
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>();
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
