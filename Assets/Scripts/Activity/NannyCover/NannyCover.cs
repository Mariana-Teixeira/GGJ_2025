using TMPro;
using UnityEngine;

public class NannyCover : BaseActivity
{

    private KeyCode _player1option1;
    private KeyCode _player1option2;
    private KeyCode _player1option3;

    private KeyCode _player2option1;
    private KeyCode _player2option2;
    private KeyCode _player2option3;

    [SerializeField] private Transform _nanny;
    [SerializeField] private Transform _character1;
    [SerializeField] private Transform _character2;
    [SerializeField] private TMP_Text _timerText;

    [SerializeField] private float _startTime;
    private float _targetTime;
    private int _nannyPosition = 1;
    private int _character1Position = 2;
    private int _character2Position = 3;
    [SerializeField] private Vector2 _leftVector;
    [SerializeField] private Vector2 _centerVector;
    [SerializeField] private Vector2 _rightVector;
    private bool _timer;
    private bool _player1WrongPosition;
    private bool _player2WrongPosition;
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
            _timerText.text = "Time to hide: " + (int)_targetTime;
        }

        if (_targetTime <= 0.0f || (_player1WrongPosition && _player2WrongPosition))
        {
            TimerEnded();
            _onFinish.Invoke(new ActivityData());
        }

        if (Input.GetKeyDown(_player1option1) && !_player1WrongPosition)
        {
            if (_nannyPosition == 1)
            {
                RightPosition(_player1Won);
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
            if (_nannyPosition == 2)
            {
                RightPosition(_player1Won);
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
            if (_nannyPosition == 3)
            {
                RightPosition(_player1Won);
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
            if (_nannyPosition == 1)
            {
                RightPosition(_player2Won);
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
            if (_nannyPosition == 2)
            {
                RightPosition(_player1Won);
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
            if (_nannyPosition == 3)
            {
                RightPosition(_player2Won);
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
        _nannyPosition = Random.Range(1, 4);
        _character1Position = Random.Range(1, 4);
        _character2Position = Random.Range(1, 4);

        while (_character1Position == _nannyPosition)
        {
            _character1Position = Random.Range(1, 4);
        }
        while (_character2Position == _nannyPosition || _character2Position == _character1Position)
        {
            _character2Position = Random.Range(1, 4);
        }

        if (_nannyPosition == 1)
        {
            _nanny.position = _leftVector;
        }
        if (_nannyPosition == 2)
        {
            _nanny.position = _centerVector;
        }
        if (_nannyPosition == 3)
        {
            _nanny.position = _rightVector;
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

    private void RightPosition(string message)
    {
        if (message.Equals(_player1Won))
        {
            Debug.Log("Player 1 won");
        }
        else
        {
            Debug.Log("Player 2 won");
        }

        _onFinish.Invoke(new ActivityData());
    }

    private void TimerEnded()
    {
        //both players take damage
        _onFinish.Invoke(new ActivityData(Loser.Both));
        Debug.Log("Both players missed");
    }
}
