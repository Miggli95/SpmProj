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
    private Vector3 BOne = new Vector3(-40, 53, 2);
    private Vector3 BAlt = new Vector3(0, 5, 0);
    private Vector3 BTwo = new Vector3(-40, 53, 2);
    int currentRespawn = 0;
    public int currentLevel;
    public bool level1Cleared = false;
    public bool level2Cleared = false;
    public bool level3Cleared = false;
    public bool level4Cleared = false;
    public int deathCount = 0;
    //List<float> bestTime;
    //List<float> yourTime;
    public float bossTime;
    int numberOfLevels;
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        levelComplete = PlayerPrefs.GetInt("Level");
        numberOfAbilities = PlayerPrefs.GetInt("numberOfAbilities");
        unlockedAbilities = new List<int>();
        print("numberOfAbilities" + numberOfAbilities);
        deathCount = PlayerPrefs.GetInt("deathCount");

        if (SceneManager.GetActiveScene().name == "BossLevel 2" || SceneManager.GetActiveScene().name == "BossLevel 3")
        {
            bossTime = PlayerPrefs.GetFloat("BossTime");
        }
        // currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        // numberOfLevels = SceneManager.sceneCountInBuildSettings - 1;//PlayerPrefs.GetInt("numberOfLevels");

        /*   for (int i = 0; i < numberOfLevels; i++)
           {

               //print("LevelBest" + PlayerPrefs.GetInt("LevelBest" + i));
               bestTime.Add(PlayerPrefs.GetFloat("LevelBest" + i));
               yourTime.Add(PlayerPrefs.GetFloat("LevelScore" + i));
           }
           */
           for (int i = 0; i < numberOfAbilities; i++)
           {
               print("add ability" + PlayerPrefs.GetInt("Ability" + i));
               unlockedAbilities.Add(PlayerPrefs.GetInt("Ability" + i));


           }
           
        if (HaveAbility((int)Abilities.slam) && SceneManager.GetActiveScene().buildIndex == 7)
        {

            currentRespawn = PlayerPrefs.GetInt("CheckpointLvl3");
            print(currentRespawn + "CheckPoint");
        }
    }

    public void SetBossTime(float time)
    {
        bossTime = time;
        PlayerPrefs.SetFloat("BossTime", bossTime);
    }

    public float getBossTime()
    {
        return PlayerPrefs.GetFloat("BossTime", bossTime);
    }

    void Update()
    {
        if (isLevelComplete(1))
            level1Cleared = true;
        if (isLevelComplete(2))
            level2Cleared = true;
        if (isLevelComplete(3))
            level3Cleared = true;
        if (isLevelComplete(4))
            level4Cleared = true;
        if (currentLevel != PlayerPrefs.GetInt("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        }
    }

    public float getYourScore(int level)
    {
        //int i = level - 1;
        Debug.Log("LevelScore" + level + " " + PlayerPrefs.GetFloat("LevelScore" + level));
        return PlayerPrefs.GetFloat("LevelScore" + level);
    }

    public float getBestScore(int level)
    {
        //int i = level - 1;
        return PlayerPrefs.GetFloat("LevelBest" + level);
    }

    public void AddBestScore(float score)
    {
        int level = currentLevel;
        float bestScore = PlayerPrefs.GetFloat("LevelBest" + level);

        if (bestScore == 0)
        {
            PlayerPrefs.SetFloat("LevelBest" + level, score);
        }
        if (score < bestScore)
        {
            Debug.Log("New Best Score yippies");
            //bestTime[level] = score;
            PlayerPrefs.SetFloat("LevelBest" + level, score);
        }
    }

    public void AddYourScore(float score)
    {
        int level = currentLevel;
        PlayerPrefs.SetFloat("LevelScore" + level, score);
    }

    public int getDeathCount()
    {
        return deathCount;
    }

    public void SetDeathCount(int death)
    {
        deathCount = death;
        PlayerPrefs.SetInt("deathCount", deathCount);
    }

    public Vector3 getSpawnPoint()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "TempTutorial":
                return new Vector3(-10, 1, 0);
                break;
            case "BossLevel":
                return BOne;
                break;
            case "BossLevel 2":
                return BTwo;
                break;
            case "BossLevel 3":
                return BTwo;
                break;
            case "Level3(3D)":
                print("spawn " + currentRespawn);
                return respawns[currentRespawn].position;
                break;
            case "AlternativeBossLevel":
                return BAlt;
                break;
            case "testScene":
                return BOne;
                break;
                //     case "AlternativeLevel3":



                //    return new Vector3(2, 7, -9);


                //    break;
        }

        print("spawn " + currentRespawn);
        return respawns[currentRespawn].position;


    }
    public Vector3 getRotation()
    {
        return new Vector3(0, 90, 0);
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

            PlayerPrefs.SetInt("CheckpointLvl3", currentRespawn);
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
        currentRespawn = 0;
        levelComplete = 0;
        numberOfAbilities = 0;
        unlockedAbilities.Clear();
        level1Cleared = false;
        level2Cleared = false;
        level3Cleared = false;
        level4Cleared = false;
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
