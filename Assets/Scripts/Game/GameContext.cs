using System;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : Singleton<GameContext>
{
    [SerializeField] private GameState[] _gameStates;

    private Dictionary<GameStateName, GameState> _gameStatesDict;

    public GameStateMachine<GameState> GameStateMachine { get; private set; }
    public PauseHandler PauseHandler { get; private set; }
    public UIManager UIManager { get; private set; }
    public ScoreResult ScoreResult { get; private set; }
    public ScoreResult LastScoreResult => _dataSaver.LastScoreResult;

    private IDataSaver _dataSaver;
    
    public override void Initialize()
    {
        base.Initialize();
        
        _gameStatesDict = new Dictionary<GameStateName, GameState>();        
        InitGameStatesDict();
        
        GameStateMachine = new GameStateMachine<GameState>();       
        PauseHandler = new PauseHandler();
        UIManager = new UIManager();
        
        _dataSaver = new JsonDataSaver();
        _dataSaver.InitDataSaver();
    }

    public void StartGame()
    {
        InitFirstState(GameStateName.GameInProgress);
    }

    private void InitGameStatesDict()
    {
        for (int i = 0; i < _gameStates.Length; i++)
        {
            _gameStates[i].InitGameState(this);
            _gameStatesDict.Add(_gameStates[i].GetStateName(), _gameStates[i]);
        }        
    }

    public void PauseGame(){
        PauseHandler.SetPause(true);
    }

    public void UnpauseGame(){
        PauseHandler.SetPause(false);
    }

    public void EnableUI(GameObject ui){
        ui.SetActive(true);
    }

    public void DisableUI(GameObject ui){
        ui.SetActive(false);
    }

    public void ChangeGameState(GameStateName stateName){
        if (_gameStatesDict.TryGetValue(stateName, out GameState gameState))
            GameStateMachine.ChangeState(gameState);
        else
            throw new Exception($"{stateName} was not found");
    }

    private void InitFirstState(GameStateName stateName){
        if (_gameStatesDict.TryGetValue(stateName, out GameState gameState))
            GameStateMachine.InitializeFirstState(gameState);
        else
            throw new Exception($"{stateName} was not found");
    }

    public void SaveScoreResult(ScoreResult scoreResult)
    {
        ScoreResult = scoreResult;
        
        _dataSaver.Save(scoreResult);
    }
}
