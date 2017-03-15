using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int levelComplete;
    int unlockedAbilities;
	void Start()
    {
        levelComplete = PlayerPrefs.GetInt("Level");
        unlockedAbilities = PlayerPrefs.GetInt("Abilities");
	}

    public void UnlockAbility(int ability)
    {
        if (ability > unlockedAbilities)
        {
            unlockedAbilities = ability;
            PlayerPrefs.SetInt("Abilities", unlockedAbilities);
        }
    }

    public void LevelComplete(int level)
    {
        if (level > levelComplete)
        {
            levelComplete = level;
            PlayerPrefs.SetInt("Level", levelComplete);
        }
    }

    public void ResetProgression()
    {
        levelComplete = 0;
        PlayerPrefs.SetInt("Level", levelComplete);
        unlockedAbilities = 0;
        PlayerPrefs.SetInt("Abilities", unlockedAbilities);
    }

    public bool isLevelComplete(int level)
    {
        return levelComplete >= level;
    }

    public bool HaveAbility(int ability)
    {
        return unlockedAbilities >= ability;
    }
}
