using UnityEngine;

public class BubbleGunShooter : BaseActivity
{
    private KeyCode _player1option1;
    private KeyCode _player1option2;
    private KeyCode _player1option3;

    private KeyCode _player2option1;
    private KeyCode _player2option2;
    private KeyCode _player2option3;

    [SerializeField] private float _startTime;
    private float _targetTime;
    private int _parentPosition = 1;
    private bool _timer;
    private bool _player1WrongPosition;
    private bool _player2WrongPosition;    
    private bool _player1RightPosition;
    private bool _player2RightPosition;
    private string _player1Won = "player1won";
    private string _player2Won = "player2won"; 
    
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        _player1option1 = SceneManager.Instance.Player1Data.PrimaryKey;
        _player1option2 = SceneManager.Instance.Player1Data.SecondaryKey;
        _player1option3 = SceneManager.Instance.Player1Data.TertiaryKey;

        _player2option1 = SceneManager.Instance.Player2Data.PrimaryKey;
        _player2option2 = SceneManager.Instance.Player2Data.SecondaryKey;
        _player2option3 = SceneManager.Instance.Player2Data.TertiaryKey;
    }
    public override void StartActivity()
    {
        _targetTime = _startTime;
        _player1WrongPosition = false;
        _player2WrongPosition = false;

        StartTimer();
    }

    public override void EndActivity()
    {
        _timer = false;
    }

    void Update()
    {
        if (_timer)
        {
            _targetTime -= Time.deltaTime;
        }

        if (_player1RightPosition && _player2RightPosition)
        {
            PlayersSurvive();
            _onFinish.Invoke(new ActivityData());
        }
        else if (_targetTime <= 0.0f && _player1RightPosition)
        {
            //only player 1 got the right position so player 2 takes damage
            Debug.Log("Player 2 takes damage");
        }
        else if (_targetTime <= 0.0f && _player2RightPosition)
        {
            //only player 2 got the right position so player 1 takes damage
            Debug.Log("Player 1 takes damage");
        }
        else if (_targetTime <= 0.0f || (_player1WrongPosition && _player2WrongPosition))
        {
            TimerEnded();
            _onFinish.Invoke(new ActivityData());
        }

        if (Input.GetKeyDown(_player1option1) && !_player1WrongPosition)
        {
            if (_parentPosition == 1)
            {
                _player1RightPosition = true;
                Debug.Log("Right Position");
            }
            else
            {
                _player1WrongPosition = true;
                Debug.Log("Wrong Position");
            }
        }
        if (Input.GetKeyDown(_player1option2) && !_player1WrongPosition)
        {
            if (_parentPosition == 2)
            {
                _player1RightPosition = true;
                Debug.Log("Right Position");
            }
            else
            {
                _player1WrongPosition = true;
                Debug.Log("Wrong Position");
            }
        }
        if (Input.GetKeyDown(_player1option3) && !_player1WrongPosition)
        {
            if (_parentPosition == 3)
            {
                _player1RightPosition = true;
                Debug.Log("Right Position");
            }
            else
            {
                _player1WrongPosition = true;
                Debug.Log("Wrong Position");
            }
        }

        if (Input.GetKeyDown(_player2option1) && !_player2WrongPosition)
        {
            if (_parentPosition == 1)
            {
                _player2RightPosition = true;
                Debug.Log("Right Position");
            }
            else
            {
                _player2WrongPosition = true;
                Debug.Log("Wrong Position");
            }
        }
        if (Input.GetKeyDown(_player2option2) && !_player2WrongPosition)
        {
            if (_parentPosition == 2)
            {
                _player2RightPosition = true;
                Debug.Log("Right Position");
            }
            else
            {
                _player2WrongPosition = true;
                Debug.Log("Wrong Position");
            }
        }
        if (Input.GetKeyDown(_player2option3) && !_player2WrongPosition)
        {
            if (_parentPosition == 3)
            {
                _player2RightPosition = true;
                Debug.Log("Right Position");
            }
            else
            {
                _player2WrongPosition = true;
                Debug.Log("Wrong Position");
            }
        }
    }

    private void StartTimer()
    {
        _parentPosition = Random.Range(1, 4);

        _timer = true;
    }

    private void PlayersSurvive()
    {
        //both players don't take damage
        Debug.Log("Both players didn't take damage");
    }

    private void TimerEnded()
    {
        //both players take damage

        Debug.Log("Both players missed");
    }
}
