using System;
using Game.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public enum GameEvent
{
    AddDialogue,
    ActivateObject,
}

public class GameController : IStartable
{
    public static event Action<GameEvent, object> OnGameEvent; 
    private readonly SceneLoader _sceneLoader;
    private readonly DialoguesManager _dialoguesManager;

    [Inject]
    public GameController(SceneLoader sceneLoader, DialoguesManager dialoguesManager)
    {
        _sceneLoader = sceneLoader;
        _dialoguesManager = dialoguesManager;
        OnGameEvent += OnOnGameEvent;
    }
    
    public void Start()
    {
        Debug.Log("GameController.Start");
        _sceneLoader.LoadScene("Village", "Main");
        SetupDialogs();
    }

    private void SetupDialogs()
    {
        _dialoguesManager.LoadDialogue("Farmer", "Dialogue1");
        _dialoguesManager.LoadDialogue("PlaygroundChess", "Dialogue1");
    }
    
    private void OnOnGameEvent(GameEvent gameEvent, object payload)
    {
        switch (gameEvent)
        {
            case GameEvent.AddDialogue:
                var (npc, dialogue) = ((string, string)) payload;
                _dialoguesManager.LoadDialogue(npc, dialogue);
                break;
            case GameEvent.ActivateObject:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameEvent), gameEvent, null);
        }
    }
}