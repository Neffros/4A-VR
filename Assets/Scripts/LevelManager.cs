using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Pattern> patternPrefabs;

    private static LevelManager _instance;


    public static LevelManager Instance => _instance;

    private Pattern currentPattern;

    private GameManager _gameManager;

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
    }

    public void NextPattern()
    {
        if(currentPattern != null)
        {
            Destroy(currentPattern.gameObject);
        }

        currentPattern = Instantiate(patternPrefabs[Random.Range(0, patternPrefabs.Count)]);
    }
}
