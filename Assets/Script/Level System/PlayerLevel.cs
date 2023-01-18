using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    public int level;

    public int currExp;
    public int maxExp;

    public Slider expBar;

    public TextMeshProUGUI expSliderDisplay, levelText;
    public void Update()
    {
        ExperienceSliderUI();

        while (currExp >= maxExp)
        {
            currExp -= maxExp;
            level++;
            maxExp += maxExp / 5;

            levelText.text = "Level " + level.ToString();
        }
    }
    public void ExperienceSliderUI()
    {
        expBar.value = currExp;
        expBar.maxValue = maxExp;

        expSliderDisplay.text = currExp + " / " + maxExp;
    }

}