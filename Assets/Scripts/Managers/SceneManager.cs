using System;
using System.Collections;
using TMPro;
using UnityEngine;

[Serializable]
public struct PlayerKeys
{
    public KeyCode PrimaryKey;
    public KeyCode SecondaryKey;
    public KeyCode TertiaryKey;
}

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    [Header("Interface Parameters")]
    [SerializeField] private GameObject _intermissionScreen;
    [SerializeField] private TMP_Text _activityTitle;
    [SerializeField] private float _transitionTime;
    
    [Header("Activity Parameters")]
    [SerializeField] private BaseActivity[] _activities;
    private BaseActivity _currentActivity => _activities[_activityIndex];
    private int _activityIndex = 0;

    private WaitForSeconds _waitForSeconds;

    public PlayerKeys Player1Keys { get; private set; }
    public PlayerKeys Player2Keys { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError("More than one 'SceneManagers' in Scene.");
        
        SetParameters();
    }

    private void Start()
    {
        StartCoroutine(WaitTimer(() =>
            {
                _activityTitle.text = _currentActivity.name;
                _intermissionScreen.SetActive(true);
            },
            () =>
            {
                _intermissionScreen.SetActive(false);
                _currentActivity.gameObject.SetActive(true);
                _currentActivity.StartActivity();
            }));
    }

    public void OnFinishActivity()
    {
        StartCoroutine(WaitTimer(() =>
            {
                _currentActivity.EndActivity();
                _currentActivity.gameObject.SetActive(false);
                _activityIndex++;
                _activityTitle.text = _currentActivity.name;
                _intermissionScreen.SetActive(true);
            }, () =>
            {
                _intermissionScreen.SetActive(false);
                _currentActivity.gameObject.SetActive(true);
                _currentActivity.StartActivity();
            }));
    }

    public void OnFinishGame()
    {
        _currentActivity.EndActivity();
        Debug.Log("Game Done");
    }

    private void SetParameters()
    {
        _intermissionScreen.SetActive(false);
        _activityTitle.text = "null";
        _waitForSeconds = new WaitForSeconds(_transitionTime);
        
        Player1Keys = new PlayerKeys
        {
            PrimaryKey = KeyCode.Q,
            SecondaryKey = KeyCode.W,
            TertiaryKey = KeyCode.E
        };

        Player2Keys = new PlayerKeys()
        {
            PrimaryKey = KeyCode.I,
            SecondaryKey = KeyCode.O,
            TertiaryKey = KeyCode.P
        };
    }

    private IEnumerator WaitTimer(Action onStartTimer, Action onEndTimer)
    {
        _intermissionScreen.SetActive(true);
        onStartTimer.Invoke();
        yield return _waitForSeconds;
        _intermissionScreen.SetActive(false);
        onEndTimer.Invoke();
    }
}
