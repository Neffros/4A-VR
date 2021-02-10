using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Pattern> patternPrefabs;
    [SerializeField] private List<platforme> platformes;
    private static LevelManager _instance;

    public static LevelManager Instance => _instance;

    private Pattern currentPattern;

    private GameManager _gameManager;

    private int currentPatternIndex;
    private int _currentPlatformIndex;

    public EnemySphere enemySphere => currentPattern != null ? currentPattern.enemySphere : null;

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
        platformes[_currentPlatformIndex].StopSource();
        DestroyLevel();
        //currentPatternIndex++;
        //currentPatternIndex %= patternPrefabs.Count;
        int rand = Random.Range(0, 3);
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
        


        _currentPlatformIndex = Random.Range(0, platformes.Count);
        currentPatternIndex = Random.Range(0, patternPrefabs.Count);
                
        Transform targetTransform = _gameManager.GameData.Seated ? platformes[_currentPlatformIndex].seatedSpawnPoint : platformes[_currentPlatformIndex].standingSpawnPoint;
        targetTransform.rotation = rotation;

        Pattern pattern = patternPrefabs[currentPatternIndex];
        Transform currentPatternTrans = pattern.transform;
        float scaleInc = 1 - platformes[_currentPlatformIndex].Scale - 0.5f;
        currentPatternTrans.localScale= new Vector3(currentPatternTrans.localScale.x + scaleInc, currentPatternTrans.localScale.y + scaleInc, currentPatternTrans.localScale.z + scaleInc);

        
        platformes[_currentPlatformIndex].PlaySource();
        if(_gameManager.GameData.Seated)
            currentPattern = Instantiate(patternPrefabs[currentPatternIndex], targetTransform.transform);
        else
            currentPattern = Instantiate(patternPrefabs[currentPatternIndex], targetTransform.transform);
        
    }
}
