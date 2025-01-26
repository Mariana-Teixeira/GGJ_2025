using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Activity.FirstToClick
{
    [Serializable]
    public struct PlayerData
    {
        public KeyCode Key;
        public float Time;
        public bool Clicked;
        public bool ValidClick;

        public PlayerData(KeyCode code)
        {
            Key = code;
            Time = 0f;
            Clicked = false;
            ValidClick = false;
        }

        public void Reset()
        {
            Time = 0f;
            Clicked = false;
            ValidClick = false;
        }
    }
     
     public class FirstToClick : BaseActivity
     {
         [Header("Timer Parameters")]
         [SerializeField] private float _minimumWait;
         [SerializeField] private float _maximumWait;
         [SerializeField] private float _timeRange;
     
         private KeyCode _player1Key;
         private KeyCode _player2Key;

         private SpriteManager _spriteManager;
         private IEnumerator timer;

         private PlayerData _player1;
         private PlayerData _player2;
         
         private bool _isExecuting;
         private bool _isValidTimer;
         private float _stopwatchTime;
         private float _perfectTime;

         private bool ArePlayersValid => _player1.ValidClick && _player2.ValidClick;
         private bool HavePlayersClicked => _player1.Clicked && _player2.Clicked;
              
         private void Awake()
         {
             this.gameObject.SetActive(false);
             _spriteManager = GetComponent<SpriteManager>();
         }

         private void Start()
         {
             _player1Key = SceneManager.Instance.Player1Data.PrimaryKey;
             _player2Key = SceneManager.Instance.Player2Data.PrimaryKey;

             _player1 = new PlayerData(_player1Key);
             _player2 = new PlayerData(_player2Key);
         }
         
         public override void StartActivity()
         {
             SetParameters();
             _spriteManager.Reset();
             StartCoroutine(Execute());
         }

         public override void EndActivity()
         {
             StopAllCoroutines();
         }

         private void SetParameters()
         {
             _perfectTime = Random.Range(_minimumWait, _maximumWait);
             var firstTimer = new WaitForSeconds(_perfectTime);
             var secondTimer = new WaitForSeconds(_timeRange);
             timer = StartTimer(firstTimer, secondTimer);
             _player1.Reset();
             _player2.Reset();
         }
     
         private void Update()
         {
             if (!_isExecuting) return;
             
             _stopwatchTime += Time.deltaTime;
             CheckForClick(ref _player1);
             CheckForClick(ref _player2);
         }
     
         private void CheckForClick(ref PlayerData player)
         {
             if (player.Clicked) return;
             if (!Input.GetKeyDown(player.Key)) return;
             player.ValidClick = _isValidTimer;
             player.Time = _stopwatchTime;
             player.Clicked = true;
             
             if (_player1.ValidClick) _spriteManager.Player1Shout();
             if (_player2.ValidClick) _spriteManager.Player2Shout();
         }

         private IEnumerator Execute()
         {
             SetParameters();
             yield return StartCoroutine(timer);
             FinishGame();
         }
     
         private IEnumerator StartTimer(WaitForSeconds firstTimer, WaitForSeconds secondTimer)
         {
             _isExecuting = true;
             _isValidTimer = false;
             _stopwatchTime = 0;
             
             yield return firstTimer;
             Debug.Log("BATH TIME!");
             _spriteManager.ChangeToBath();
             _isValidTimer = true;
             
             yield return secondTimer;
             _isExecuting = false;
         }

         private void FinishGame()
         {
             var loser = GetWinner();
             _onFinish.Invoke(new ActivityData(loser));
         }

         private Loser GetWinner()
         {
             var player1Valid = _player1.Clicked || _player1.ValidClick;
             var player2Valid = _player2.Clicked || _player2.ValidClick;
             
             // Given at least one is invalid.
             if (!player1Valid && !player2Valid) return Loser.Both;
             else if (player1Valid && !player2Valid) return Loser.Player2;
             else if (player2Valid && !player1Valid) return Loser.Player1;
             // Given they are both valid.
             else if (_player1.Time < _player2.Time) return Loser.Player2;
             else if (_player2.Time < _player1.Time) return Loser.Player1;
             else return Loser.Both;
         }
     }   
}