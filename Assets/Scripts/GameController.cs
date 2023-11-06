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
    TalkToDed,
    TalkToDevilAboutDed,
    ReadDiary,
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
        _dialoguesManager.LoadDialogue("Kids", "KidsAboutShovel");
        _dialoguesManager.LoadDialogue("Kids", "KidsAboutDed");
        _dialoguesManager.LoadDialogue("Ded", "DedAboutDiary");
        _dialoguesManager.LoadDialogue("Devil", "Devil");
        _dialoguesManager.LoadDialogue("Miner", "Miner");
        _dialoguesManager.LoadDialogue("PlaygroundChess", "ChessFromPlayground");
        _dialoguesManager.LoadDialogue("ShopBook", "ShopBook");
    }

    public static void SendEvent(GameEvent gameEvent, object payload = null)
    {
        OnGameEvent?.Invoke(gameEvent, payload);
    }

    private bool _diaryRead = false;
    private bool _foundChessInDungeon = false;
    private bool _openedChessInPlayground = false;
    private bool _openedChessInDungeon = false;

    private void OnGameEventCallback(GameEvent gameEvent, object payload)
    {
        Debug.Log(gameEvent.ToString());
        switch (gameEvent)
        {
            case GameEvent.ShovelPickUp:
                _inventoryUi.AddItem("Shovel");
                _dialoguesManager.RemoveDialogue("Farmer", "FarmerNoShovel");
                _dialoguesManager.LoadDialogue("Farmer", "Farmer");
                break;
            case GameEvent.ChessDigUp:
                _dialoguesManager.LoadDialogue("Ded", _diaryRead ? "DedAboutMiner" : "DedAboutMinerWithoutDiary");
                _openedChessInPlayground = true;
                break;
            case GameEvent.TalkToDed:
                _dialoguesManager.LoadDialogue("Devil", "DevilAboutDedsKey");
                break;
            case GameEvent.TalkToDevilAboutDed:
                _inventoryUi.AddItem("DedsKey");
                break;
            case GameEvent.ReadDiary:
                // _inventoryUi.RemoveItem("DedsKey");
                _diaryRead = true;
                _dialoguesManager.StartDialogue("DedsDiary");
                break;
            case GameEvent.FindChessInDungeon:
                if (_foundChessInDungeon) return;
                _foundChessInDungeon = true;
                _dialoguesManager.LoadDialogue("Miner", "MinerAfterFindChessInDZ");
                _dialoguesManager.LoadDialogue("Kids", "KidsAboutMiner");
                break;
            case GameEvent.OpenChessInDungeon:
                _dialoguesManager.RemoveDialogue("Miner", "MinerAfterFindChessInDZ");
                _dialoguesManager.LoadDialogue("Miner", "MinerAfterOpenChess");
                _dialoguesManager.RemoveDialogue("PlaygroundChess", "ChessFromPlayground");
                _dialoguesManager.LoadDialogue("PlaygroundChess", "ChessFromPlaygroundWithDungeonChess");
                _dialoguesManager.StartDialogue(_openedChessInPlayground
                    ? "OpenChessOnDungeon"
                    : "OpenChessOnDungeonNoPGChess");
                _openedChessInDungeon = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameEvent), gameEvent, null);
        }
    }
}