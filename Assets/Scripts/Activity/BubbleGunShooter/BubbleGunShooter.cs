using UnityEngine;
using TMPro;

public class BubbleGunShooter : BaseActivity
{
    private KeyCode _player1option1;
    private KeyCode _player1option2;
    private KeyCode _player1option3;

    private KeyCode _player2option1;
    private KeyCode _player2option2;
    private KeyCode _player2option3;

    [SerializeField] private Transform _parents;
    [SerializeField] private Transform _character1;
    [SerializeField] private Transform _character2;
    [SerializeField] private TMP_Text _timerText;

    [SerializeField] private float _startTime;
    private float _targetTime;
    private int _parentsPosition = 1;
    private int _character1Position = 2;
    private int _character2Position = 3;
    [SerializeField] private Vector2 _leftVector;
    [SerializeField] private Vector2 _centerVector;
    [SerializeField] private Vector2 _rightVector;
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
            _timerText.text = "Time to shoot: " + (int)_targetTime;
        }

        if (_player1RightPosition && _player2RightPosition)
        {
            PlayersSurvive();
            _onFinish.Invoke(new ActivityData());
        }
        else if (_targetTime <= 0.0f && _player1RightPosition)
        {
            //only player 1 got the right position so player 2 takes damage
            _onFinish.Invoke(new ActivityData(Loser.Player2));
            Debug.Log("Player 2 takes damage");
        }
        else if (_targetTime <= 0.0f && _player2RightPosition)
        {
            //only player 2 got the right position so player 1 takes damage
            _onFinish.Invoke(new ActivityData(Loser.Player1));
            Debug.Log("Player 1 takes damage");
        }
        else if (_targetTime <= 0.0f || (_player1WrongPosition && _player2WrongPosition))
        {
            TimerEnded();
            _onFinish.Invoke(new ActivityData());
        }

        if (Input.GetKeyDown(_player1option1) && !_player1WrongPosition)
        {
            if (_parentsPosition == 1)
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
            if (_parentsPosition == 2)
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
            if (_parentsPosition == 3)
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
            if (_parentsPosition == 1)
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
            if (_parentsPosition == 2)
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
            if (_parentsPosition == 3)
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
        _parentsPosition = Random.Range(1, 4);
        _character1Position = Random.Range(1, 4);
        _character2Position = Random.Range(1, 4);

        while (_character1Position == _parentsPosition)
        {
            _character1Position = Random.Range(1, 4);
        }
        while (_character2Position == _parentsPosition || _character2Position == _character1Position)
        {
            _character2Position = Random.Range(1, 4);
        }

        if (_parentsPosition == 1)
        {
            _parents.position = _leftVector;
        }
        if (_parentsPosition == 2)
        {
            _parents.position = _centerVector;
        }
        if (_parentsPosition == 3)
        {
            _parents.position = _rightVector;
        }
        
        if (_character1Position == 1)
        {
            _character1.position = _leftVector;
        }
        if (_character1Position == 2)
        {
            _character1.position = _centerVector;
        }
        if (_character1Position == 3)
        {
            _character1.position = _rightVector;
        }
        
        if (_character2Position == 1)
        {
            _character2.position = _leftVector;
        }
        if (_character2Position == 2)
        {
            _character2.position = _centerVector;
        }
        if (_character2Position == 3)
        {
            _character2.position = _rightVector;
        }

        _timer = true;
    }

    private void PlayersSurvive()
    {
        //both players don't take damage
        _onFinish.Invoke(new ActivityData());
        Debug.Log("Both players didn't take damage");
    }

    private void TimerEnded()
    {
        //both players take damage
        _onFinish.Invoke(new ActivityData(Loser.Both));
        Debug.Log("Both players missed");
    }
}
