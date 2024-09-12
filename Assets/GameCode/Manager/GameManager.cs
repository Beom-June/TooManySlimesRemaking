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

    public static int _stage;                        // static���� �������� �� �� �ְ�, ���� ��������
    public event Action<GameState> _onGameStateChange;   //  ������ ���� ��� event 

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
        // ������ ���۵Ǹ� �÷��� ���·�
        SetGameState(GameState.Playing);
    }
    public void GameOver()
    {
        SetGameState(GameState.GameOver);
    }
    
    void SetGameState(GameState newGameState)
    {
        //  Menu ����
        if (newGameState == GameState.Menu)
        {

        }
        //  Loading ����
        else if (newGameState == GameState.Loading)
        {

        }
        //  Play ����
        else if (newGameState == GameState.Playing)
        {
        }
        //  GameOver ����
        else if (newGameState == GameState.GameOver)
        {
        }
        _currentGameState = newGameState;
        _onGameStateChange?.Invoke(newGameState);
    }
}