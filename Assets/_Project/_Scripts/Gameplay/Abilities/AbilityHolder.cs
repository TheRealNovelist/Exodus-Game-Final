using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public List<SkillSystem>  ability;
    float cooldownTime;
    float activeTime;
    //FModPlayer fModPlayer;

    enum AbilityState   //declare the states of the ability
    {
        ready, active, cooldown
    }
    AbilityState state1 = AbilityState.ready;   //set ability state1 to ready
    AbilityState state2 = AbilityState.ready;   //set ability state1 to ready

    public KeyCode key1; //declare a public keycode
    public KeyCode key2; //declare a public keycode

    private void Start()
    {
        //fModPlayer = GetComponent<FModPlayer>();
    }
    // Update is called once per frame
    void Update()
    {
        switch(state1)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(key1))
                {
                    ability[0].Activate(gameObject); //call Active() in ability
                    state1 = AbilityState.active;
                    activeTime = ability[0].activeTime;
                    //fModPlayer.PlayDashSound();
                }
                break;
            case AbilityState.active:
                if (activeTime > 0) 
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability[0].BeginCooldown(gameObject);
                    state1 = AbilityState.cooldown;
                    cooldownTime = ability[0].cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state1 = AbilityState.ready;
                }
                break;
        }
        
        switch(state2)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(key2))
                {
                    ability[1].Activate(gameObject); //call Active() in ability
                    state2 = AbilityState.active;
                    activeTime = ability[1].activeTime;
                }
                break;
            case AbilityState.active:
                if (activeTime > 0) 
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability[1].BeginCooldown(gameObject);
                    state2 = AbilityState.cooldown;
                    cooldownTime = ability[1].cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state2 = AbilityState.ready;
                }
                break;
        }
        
    }

    public void SetUsingAbility()
    {
        AbilityItem abilityItemSlot1 = Inventory.Instance.equippedGuns[0] as AbilityItem;
        AbilityItem abilityItemSlot2 =  Inventory.Instance.equippedGuns[1] as AbilityItem;

        if(abilityItemSlot1!=null)
        ability[0] = abilityItemSlot1.ability;
        
        if(abilityItemSlot2!=null)
        ability[1] = abilityItemSlot2.ability;

    }
}
