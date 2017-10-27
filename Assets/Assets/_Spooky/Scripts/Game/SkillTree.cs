using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour 
{
    //Referencia de la bala mejorada del player.
    public Player spooky;
    public AIAbility atractSoulAbility;
    public AIAbility spookyEscapeAbility;

    //Metodos que van a estar unidos a los botones para mejorar el jugador.
    //Atack Upgrades
    public void IncreasePlayerDamage()
    {
        //PlayerController.Instance.playerDamage += 1;
    }
    public void IncreaseAtackSpeed()
    {
        //PlayerController.Instance.attackSpeed += 1;
    }
    public void AddFireDamage()
    {

    }
    //Spooky Upgrades
    public void IncreaseMovementSpeed()
    {

    }
    public void ScapeFromCrows()
    {

    }
    public void AtractSouls()
    {

    }
    //Farm Upgrades
    public void DecreasePlantTime()
    {

    }
    public void ReduceCost()
    {
        //PlayerMove.Instance.costRatio -=0.5f;
    }
    public void IncreaseSoulRatio()
    {
        //PlayerMove.Instance.soulGainRatio -=0.5f;
    }
}
