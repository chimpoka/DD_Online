using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameState gameState;

    private static Game _instance;
    public static Game Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject game = Instantiate(Resources.Load("Prefabs/Game")) as GameObject;
                _instance = game.GetComponent<Game>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        SkillContainer.Instance.init();
        gameState = new GameState_TeamBuilding(this);
    }

    void Update()
    {
        gameState.update();
    }
}
