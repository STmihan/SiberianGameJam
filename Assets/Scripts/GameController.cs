using System;
using Game.Services;
using Game.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public enum GameEvent
{
    ShovelPickUp,
    ChessDigUp,
    DiaryKeyPickUp,
    TalkToDed,
    TalkToDevilAboutDed,
    DiaryRead,
    FindChessInDungeon,
    OpenChessInDungeon,
}

public class GameController : IStartable
{
    public static event Action<GameEvent, object> OnGameEvent; 
    private readonly SceneLoader _sceneLoader;
    private readonly DialoguesManager _dialoguesManager;
    private readonly InventoryUI _inventoryUi;

    [Inject]
    public GameController(SceneLoader sceneLoader, DialoguesManager dialoguesManager, InventoryUI inventoryUi)
    {
        _sceneLoader = sceneLoader;
        _dialoguesManager = dialoguesManager;
        _inventoryUi = inventoryUi;
        OnGameEvent += OnGameEventCallback;
    }
    
    public void Start()
    {
        Debug.Log("GameController.Start");
        _sceneLoader.LoadScene("Village", "Main");
        SetupDialogs();
    }

    private void SetupDialogs()
    {
        _dialoguesManager.LoadDialogue("Farmer", "Farmer");
        _dialoguesManager.LoadDialogue("PlaygroundChess", "ChessFromPlayground");
        _dialoguesManager.LoadDialogue("Kids", "KidsAboutShovel");
        _dialoguesManager.LoadDialogue("Ded", "DedAboutDiary");
    }
    
    public static void SendEvent(GameEvent gameEvent, object payload = null)
    {
        OnGameEvent?.Invoke(gameEvent, payload);
    }
    
    private void OnGameEventCallback(GameEvent gameEvent, object payload)
    {
        switch (gameEvent)
        {
            case GameEvent.ShovelPickUp:
                _inventoryUi.AddItem("Shovel");
                _dialoguesManager.RemoveDialogue("Farmer", "FarmerNoShovel");
                _dialoguesManager.LoadDialogue("Farmer", "Farmer");
                break;
            case GameEvent.ChessDigUp:
                _inventoryUi.RemoveItem("Shovel");
                break;
            case GameEvent.DiaryKeyPickUp:
                break;
            case GameEvent.TalkToDed:
                break;
            case GameEvent.TalkToDevilAboutDed:
                break;
            case GameEvent.DiaryRead:
                break;
            case GameEvent.FindChessInDungeon:
                break;
            case GameEvent.OpenChessInDungeon:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameEvent), gameEvent, null);
        }
    }
}