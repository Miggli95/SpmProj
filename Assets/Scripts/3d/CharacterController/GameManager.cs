using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int levelComplete;
    int numberOfAbilities;
    List<int> unlockedAbilities;
    public List<Transform> respawns;
    int currentRespawn = 0;
	void Start()
    {
        levelComplete = PlayerPrefs.GetInt("Level");
        numberOfAbilities = PlayerPrefs.GetInt("numberOfAbilities");
        unlockedAbilities = new List<int>();
        print("numberOfAbilities" + numberOfAbilities);
        for (int i = 0; i < numberOfAbilities; i++)
        {
            print("add ability" + PlayerPrefs.GetInt("Ability" + i));
            unlockedAbilities.Add(PlayerPrefs.GetInt("Ability" + i));
        }
	}

    public Vector3 getSpawnPoint()
    {
        print("spawn " + currentRespawn);
        return respawns[currentRespawn].position;
    }

    public void UnlockAbility(int ability)
    {
        if (!unlockedAbilities.Contains(ability))
        {
            unlockedAbilities.Add(ability);
        }

        PlayerPrefs.SetInt("numberOfAbilities", unlockedAbilities.Count);
        for (int i = 0; i < unlockedAbilities.Count; i++)
        {
            PlayerPrefs.SetInt("Ability" + i, unlockedAbilities[i]);
        }
       /* if (ability > unlockedAbilities)
        {
            unlockedAbilities = ability;
            PlayerPrefs.SetInt("Abilities", unlockedAbilities);
        }*/
    }
    
    public void setRespawn(int i)
    {
        if (i > currentRespawn)
        {
            currentRespawn = i;
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
        PlayerPrefs.DeleteAll();
    }

    public bool isLevelComplete(int level)
    {
        return levelComplete >= level;
    }

    public bool HaveAbility(int ability)
    {
        return unlockedAbilities.Contains(ability);
    }
}
