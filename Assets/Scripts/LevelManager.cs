using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Pattern> patternPrefabs;

    private static LevelManager _instance;

    public Transform seatedSpawnPoint;
    public Transform standingSpawnPoint;
    public static LevelManager Instance => _instance;

    private Pattern currentPattern;

    private GameManager _gameManager;

    private int currentPatternIndex;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        _gameManager = GameManager.Instance;

        if (patternPrefabs.Count <= 0)
        {
            throw new UnityException("Pas de patterns ...");
        }

        _gameManager.LevelManager = this;

        currentPatternIndex = -1;
        _gameManager.GameRules.NextLevel();
    }

    
    public void DestroyLevel()
    {
        if(currentPattern != null)
        {
            Destroy(currentPattern.gameObject);
        }
    }

    public void NextPattern()
    {
        DestroyLevel();
        Transform targetTransform = _gameManager.GameData.Seated ? seatedSpawnPoint : standingSpawnPoint;
        //currentPatternIndex++;
        //currentPatternIndex %= patternPrefabs.Count;
        int rand = Random.Range(0, 2);
        float randomZ = 0.0f;
        switch (rand)
        {
            case 0:
                randomZ = 0.0f;
                break;
            case 1:
                randomZ = 90.0f;
                break;
            case 2:
                randomZ = 180.0f;
                break;
        }
        
        Quaternion rotation = new Quaternion(0,0,randomZ,0);
        targetTransform.rotation = rotation;

        currentPatternIndex = Random.Range(0, patternPrefabs.Count);
        if(_gameManager.GameData.Seated)
            currentPattern = Instantiate(patternPrefabs[currentPatternIndex], targetTransform.transform);
        else
            currentPattern = Instantiate(patternPrefabs[currentPatternIndex], targetTransform.transform);
        
    }
}
