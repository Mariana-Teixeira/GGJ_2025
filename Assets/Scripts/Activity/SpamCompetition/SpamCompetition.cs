using System;
using UnityEngine;

public class SpamCompetition : MonoBehaviour
{
    [SerializeField] private Transform _circle1;
    [SerializeField] private Transform _circle2;

    private int _player1Counter;
    private int _player2Counter;

    private float _targetTime;

    private bool _timer;

    private KeyCode _player1key;
    private KeyCode _player2key;

    private ActivityManager _activityManager;

    private void Awake()
    {
        _activityManager = GetComponent<ActivityManager>();
        _activityManager.gameObject.SetActive(false);
    }

    private void Start()
    {
        _player1key = SceneManager.Instance.Player1Keys.PrimaryKey;
        _player2key = SceneManager.Instance.Player2Keys.PrimaryKey;
    }

    private void OnEnable()
    {
        _player1Counter = 0;
        _player2Counter = 0;
        _targetTime = 5.0f;
        _timer = false;
        StartTimer();
    }

    private void Update()
    {
        if (!_timer) return;
        
        _targetTime -= Time.deltaTime;

        if(_targetTime <= 0.0f)
        {
            TimerEnded();
        }

        //player 1 key
        if (Input.GetKeyDown(_player1key))
        {
            _player1Counter++;
            _circle1.localScale = new Vector3(_player1Counter, _player1Counter);
        }

        //player 2 key
        if (Input.GetKeyDown(_player2key))
        {
            _player2Counter++;
            _circle2.localScale = new Vector3(_player2Counter, _player2Counter);
        }
    }

    private void TimerEnded()
    {        
        // if counters are equal
        if (_player1Counter == _player2Counter)
        {
            //both players take damage
            Debug.Log("players draw");
        }
        else if (_player1Counter > _player2Counter)
        {
            //player2 takes damage
            Debug.Log("player 1 won");
        }
        else
        {
            //player 1 takes damage
            Debug.Log("player 2 won");
        }
    }

    private void StartTimer()
    {
        _timer = true;
    }
}
