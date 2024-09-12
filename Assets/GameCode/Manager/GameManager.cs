using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState
{
    Menu,
    Loading,
    Playing,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager _gameManager;
    public GameState _currentGameState = GameState.Menu;

    public static int _stage;                        // static으로 전역에서 할 수 있게, 게임 스테이지
    public event Action<GameState> _onGameStateChange;   //  옵저버 패턴 사용 event 

    private void Awake()
    {
        _gameManager = this;

        _stage = 1;
    }
    void Start()
    {
        StartGame();
    }

    void Update()
    {

    }
    public void StartGame()
    {
        // 게임이 시작되면 플레이 상태로
        SetGameState(GameState.Playing);
    }
    public void GameOver()
    {
        SetGameState(GameState.GameOver);
    }
    
    void SetGameState(GameState newGameState)
    {
        //  Menu 상태
        if (newGameState == GameState.Menu)
        {

        }
        //  Loading 상태
        else if (newGameState == GameState.Loading)
        {

        }
        //  Play 상태
        else if (newGameState == GameState.Playing)
        {
        }
        //  GameOver 상태
        else if (newGameState == GameState.GameOver)
        {
        }
        _currentGameState = newGameState;
        _onGameStateChange?.Invoke(newGameState);
    }
}