using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneMenu : MonoBehaviour
{
    // Gameobjects
    [SerializeField] private GameObject zoneImage;

    // Ints
    [HideInInspector] public int clockLevel = 1;
    [HideInInspector] public int speedLevel = 1;
    [HideInInspector] public int skipLevel = 1;

    // Floats
    private float CostTimeCalculations;
    private float CostSpeedCalculations;
    private float CostSkipZoneCalculations;

    // Texts
    [SerializeField] private Text textCostTime;
    [SerializeField] private Text textCostSpeed;
    [SerializeField] private Text textCostSkipZone;

    private static ZoneMenu _instance;

    public static ZoneMenu instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
        TextCostForTimeDropChance();
        TextCostForDecreseSpeed();
        TextCostForSkipZone();
    }


    public void DecreaseSpeed()
    {
        // Decreases the player speed if his current speed is bigger then 10 and the gots 2.1x the time needed to buy that upgrade 
        TextCostForDecreseSpeed();
        if (Player.instance.speed >= 10 && CountdownTimer.instance.timer > CostSpeedCalculations * 2.1f)
        {
            // increases the speed lvl and decreases the player speed
            speedLevel++;
            CountdownTimer.instance.timer -=  10 * (speedLevel / 2.0f );
            Player.instance.speed -=  1.5f;
            TextCostForDecreseSpeed();
        }
    }

    public void TextCostForDecreseSpeed()
    {
        // generates the cost to show in the UI
        CostSpeedCalculations = 10 * (speedLevel / 2.0f);
        textCostSpeed.text = CostSpeedCalculations.ToString();

    }

    public void IncreaseTimeDropChance()
    {
        // Increases the reward from the clock in case the player has 2.1x the time needed to upgrade it
        TextCostForTimeDropChance();
        if (CountdownTimer.instance.timer > CostTimeCalculations * 2.1f)
        {            
            clockLevel++;
            CountdownTimer.instance.timer -= 50 * ( clockLevel / 2.0f );
            TextCostForTimeDropChance();
        }

    }

    public void TextCostForTimeDropChance()
    {
        // Shows the current cost to upgrade the clock time reward
        CostTimeCalculations = 50 * (clockLevel / 2.0f);
        textCostTime.text = CostTimeCalculations.ToString();
    }

    public void SkipZone()
    {
        // Skips the current zone case the players has 2.1x the amount of time needed to buy the upgrade
        TextCostForSkipZone();
        if (CountdownTimer.instance.timer > CostSkipZoneCalculations * 2.1f)
        {
            skipLevel++;
            GameManager.instance.currentZone--;
            GameManager.instance.CurrentZone();
            CountdownTimer.instance.timer -= 100 * (skipLevel / 2.0f);
            TextCostForSkipZone();
        }
    }

    public void TextCostForSkipZone()
    {
        // Show the cost to upgrade zone in the UI
        CostSkipZoneCalculations = 100 * (skipLevel / 2.0f);
        textCostSkipZone.text = CostSkipZoneCalculations.ToString();
    }



    public void BuyShield()
    {
        // In case the players has more then 110time he can buy a shield to be invulnerable
        if (CountdownTimer.instance.timer > 110)
        {
            Player.instance.isShieldActive = true;
            CountdownTimer.instance.timer -=  100;
        }
    }

    public void Resume()
    {
        // Resume the game by finishing the upgrade page and decreasing the player current zone
        GameManager.instance.currentZone--;
        GameManager.instance.CurrentZone();
        Time.timeScale = 1f;
        zoneImage.SetActive(false);
    }

    public void Pause()
    {
        // Show the upgrade page and stop the game
        Time.timeScale = 0f;
        zoneImage.SetActive(true);
    }
}
