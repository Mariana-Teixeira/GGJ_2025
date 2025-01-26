using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[Serializable]
public struct PlayerData
{
    public int Losses;
    public KeyCode PrimaryKey;
    public KeyCode SecondaryKey;
    public KeyCode TertiaryKey;
}

[RequireComponent(typeof(AnimationManager))]
public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    [Header("Interface Parameters")]
    [SerializeField] private GameObject _intermissionScreen;
    [SerializeField] private TMP_Text _intermissionTitle;
    [SerializeField] private float _transitionDuration;
    [SerializeField] private float _transitionPause;
    private AnimationManager _animationManager;
    private WaitForSeconds _waitForSeconds;
    [Space(10)]
    [SerializeField] private TMP_Text _player1Health;
    [SerializeField] private TMP_Text _player2Health;
    
    [Header("Activity Parameters")]
    [SerializeField] private BaseActivity[] _activities;
    private int _activityIndex;
    private BaseActivity _currentActivity => _activities[_activityIndex];
    
    [Header("Player Parameters")]
    [SerializeField] private int _maxLosses;
    private PlayerData _player1Data;
    private PlayerData _player2Data;
    public PlayerData Player1Data => _player1Data;
    public PlayerData Player2Data => _player2Data;
    private bool _isGameOver => _player1Data.Losses == _maxLosses || _player2Data.Losses == _maxLosses;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError("More than one 'SceneManagers' in Scene.");
        
        SetParameters();
    }

    private void Start()
    {
        GetNextActivity();
        CloseIntermissionScreen();
        _animationManager.GoToEndAnimation();
        StartCoroutine(WaitTimer(OpenTransmissionScreen, EndTransition));
    }

    public void OnFinishActivity(ActivityData data)
    {
        RemovePlayerHealth(data.Target);
        StartCoroutine(WaitTimer(StartTransition, EndTransition));
    }

    private void StartTransition()
    {
        CloseActivity();
        GetNextActivity();
        if (_isGameOver) OpenScoreScreen(); 
        else OpenTransmissionScreen();
    }

    private void EndTransition()
    {
        CloseIntermissionScreen();
        OpenActivity();
    }

    private void OpenTransmissionScreen()
    {
        _intermissionTitle.text = _currentActivity.name;
        _player1Health.text = Player1Data.Losses.ToString();
        _player2Health.text = Player2Data.Losses.ToString();
        _animationManager.PlayAnimation(_transitionDuration);
    }

    private void CloseIntermissionScreen()
    {
        _animationManager.RewindAnimation(_transitionDuration);
    }

    private void OpenActivity()
    {
        _currentActivity.gameObject.SetActive(true);
        _currentActivity.StartActivity();
    }

    private void GetNextActivity()
    {
        if (_activities.Length <= 1) return;
        
        var index = _activityIndex;
        while (index == _activityIndex) index = Random.Range(0, _activities.Length);
        _activityIndex = index;
    }

    private void CloseActivity()
    {
        _currentActivity.EndActivity();
        _currentActivity.gameObject.SetActive(false);
    }

    private void OpenScoreScreen()
    {
        _intermissionTitle.text = GetWinner();
        _player1Health.text = Player1Data.Losses.ToString();
        _player2Health.text = Player2Data.Losses.ToString();
        _intermissionScreen.SetActive(true);
    }

    private void CloseScoreScreen()
    {
        
    }

    private void SetParameters()
    {
        _animationManager = GetComponent<AnimationManager>();
        _waitForSeconds = new WaitForSeconds(_transitionPause);
        
        _player1Data = new PlayerData
        {
            PrimaryKey = KeyCode.Q,
            SecondaryKey = KeyCode.W,
            TertiaryKey = KeyCode.E
        };
        
        _player2Data = new PlayerData
        {
            PrimaryKey = KeyCode.I,
            SecondaryKey = KeyCode.O,
            TertiaryKey = KeyCode.P
        };
    }

    private IEnumerator WaitTimer(Action onStartTimer, Action onEndTimer)
    {
        onStartTimer.Invoke();
        yield return _waitForSeconds;
        onEndTimer.Invoke();
    }

    private void RemovePlayerHealth(Loser loser)
    {
        if (loser == Loser.Player1) _player1Data.Losses++;
        else if (loser == Loser.Player2) _player2Data.Losses++;
        else { _player1Data.Losses++; _player2Data.Losses++; }
    }

    private string GetWinner()
    {
        if (_player1Data.Losses == _maxLosses && _player2Data.Losses == _maxLosses) return "Both Players Lost!";
        return _player1Data.Losses == _maxLosses ? "Player 1 Lost" : "Player 2 Lost";
    }
}
