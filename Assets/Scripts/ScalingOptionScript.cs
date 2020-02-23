using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalingOptionScript : MonoBehaviour
{
    public int optionCode;

    public Option option;

    public Text description, effect;

    private void OnEnable()
    {
        optionCode = (int)(Random.value * 3);
        float range = .8f;
        switch (optionCode)
        {
            case 0:
                option = new EffectPlayerAttackDamage();                
                option.setEffect(Random.value * range - range / 2);
                option.description = "Increase or decrease the player's attack effectiveness.";
                break;
            case 1:
                option = new SpecialEffect();
                option.setEffect( (int)(Random.value * 2 ) - .5f); //true or false
                print(option.readEffect());
                option.description = "Set enable or disable inverse controls for the player";
                break;
            case 2:
                option = new EffectEnemySpeed();
                option.setEffect(Random.value * range - range / 4);
                option.description = "Increase or decreases the enemies' speed.";
                break;
            default:
                print("Options error");
                break;
        }

        PopulateTextBoxes();
    }

    private void PopulateTextBoxes()
    {
        description.text = option.description;
        effect.text = option.readEffect();
    }
    public void ResolveOption()
    {
        var gs = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
        switch (optionCode)
        {
            case 0:
                gs.playerAttackMagnitude = option.readEffectFloat();
                break;
            case 1:
                if(option.readEffectFloat()>0)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().inverse = -1;
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().inverse = 1;
                }
                break;
            case 2:
                gs.enemySpeedMagnitude = option.readEffectFloat();
                break;
            default:
                print("Options error");
                break;
        }
    }


}



public abstract class Option
{
    public string description;
    public virtual string readDescription()
    {
        return description;
    }

    public abstract string readEffect();
    public abstract void setEffect(float input);

    public abstract float readEffectFloat();
}
public class EffectPlayerAttackDamage : Option
{
    public float effect;
    
    public override string readEffect()
    {
        string prefix = effect > 0 ? "+" : "";
        return prefix + effect.ToString().Substring(0,4);
    }

    public override void setEffect(float input)
    {
        effect = input;
    }
    public override float readEffectFloat() { return effect; }
}

public class SpecialEffect : Option
{
    public bool effect;
    public override string readEffect()
    {
        return effect ? "Enable" : "Disable";
    }
    public override void setEffect(float input)
    {
        effect = input > 0;
    }
    public override float readEffectFloat() { return effect?1:-1; }
}

public class EffectEnemySpeed : Option
{
    public float effect;

    public override string readEffect()
    {
        string prefix = effect > 0 ? "+" : "";
        return prefix + effect.ToString().Substring(0, 4);
    }
    public override void setEffect(float input)
    {
        effect = input;
    }
    public override float readEffectFloat() { return effect; }
}
