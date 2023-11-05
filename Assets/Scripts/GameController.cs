using Game.Services;
using UnityEngine;
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
        _sceneLoader.LoadScene("Village");
    }
}