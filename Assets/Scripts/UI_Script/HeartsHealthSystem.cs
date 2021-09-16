using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HeartsHealthSystem
{

    //Handlers
    public event EventHandler onDamaged;
    public event EventHandler onHealed;
    public event EventHandler onDead;


    private List<Heart> heartList;

    public const int MAX_FRAGMENT_AMOUNT = 2;

    public int nrOfHeartsHealed = 0;

    public HeartsHealthSystem(int heartAmount)
    {
        heartList = new List<Heart>();

        for (int i = 0; i < heartAmount; i++)
        {
            Heart heart = new Heart(MAX_FRAGMENT_AMOUNT);
            heartList.Add(heart);
        }
    }

    public List<Heart> getHeartList()
    {
        return heartList;   
    }

    public void damage(int dmgAmount)
    {
        for (int i = heartList.Count - 1; i >= 0; i--)
        {
            Heart heart = heartList[i];

            //check if heart can take damage
            if (dmgAmount > heart.getFragmentAmount())
            {
                //damage is too big for that heart, so it moves onto next one
                dmgAmount -= heart.getFragmentAmount();
                heart.damageHeart(heart.getFragmentAmount());
            }
            else
            {
                //hearts can take the dmg and live on
                heart.damageHeart(dmgAmount);
                break;
            }

        }

        if (onDamaged != null)
        {
            onDamaged(this, EventArgs.Empty);
        }

        if (isDead())
        {
            if (onDead != null)
            {
                onDead(this, EventArgs.Empty);
            }
        }

    }

    public void heal(int healAmount)
    {
        for (int i = 0; i < heartList.Count; i++)
        {
            Heart heart = heartList[i];

            int missingFragment = MAX_FRAGMENT_AMOUNT - heart.getFragmentAmount();

            if (healAmount > missingFragment)
            {
                healAmount -= missingFragment;
                heart.healHeart(missingFragment);
            }
            else
            {
                heart.healHeart(healAmount);
                break;
            }
        }


        if (onHealed != null)
        {
            onHealed(this, EventArgs.Empty);
        }
    }


    public bool isDead()
    {

        return heartList[0].getFragmentAmount() == 0;
    }



    //________________________________________Inner Class Heart________________________________________________\\

    public class Heart
    {
        private int fragments;

        public Heart(int fragments)
        {
            this.fragments = fragments;
        }

        public int getFragmentAmount()
        {
            return fragments;
        }

        public void setFragments(int fragments)
        {
            this.fragments = fragments;
        }

        public void damageHeart(int dmgAmount)
        {
            if (dmgAmount >= fragments)
            {
                fragments = 0;
            }
            else
            {
                fragments -= dmgAmount;
            }
        }

        public void healHeart(int healAmount)
        {
            if (fragments + healAmount > MAX_FRAGMENT_AMOUNT)
            {

                fragments = MAX_FRAGMENT_AMOUNT;
            }
            else
            {
                fragments += healAmount;
            }
        }
    }


}
