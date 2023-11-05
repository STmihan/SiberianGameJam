using Game.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class GameController : IStartable
{
    private readonly SceneLoader _sceneLoader;

    [Inject]
    public GameController(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void Start()
    {
        Debug.Log("GameController.Start");
        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            _sceneLoader.LoadScene("Village");
        }
    }
}