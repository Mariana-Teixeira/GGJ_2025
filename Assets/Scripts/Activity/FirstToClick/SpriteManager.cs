using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Activity.FirstToClick
{
    [Serializable]
    public struct PlayerSprites
    {
        public Sprite Idle;
        public Sprite Shouting;
        public Sprite Embarassed;
    }
    
    public class SpriteManager : MonoBehaviour
    {
        [Header("Sprite Parameters")]
        [SerializeField] private SpriteRenderer _player1Renderer;
        [SerializeField] private PlayerSprites _player1Sprites;
        [Space(10)]
        [SerializeField] private SpriteRenderer _player2Renderer;
        [SerializeField] private PlayerSprites _player2Sprites;
        
        [Header("Text Parameters")]
        [SerializeField] private TMP_Text _bathTimeLine;
        [SerializeField] private TMP_Text _player1Line;
        [SerializeField] private TMP_Text _player2Line;
        [SerializeField] private TMP_Text _parent1Line;
        [SerializeField] private TMP_Text _parent2Line;
        [SerializeField] private float _popSpeed = 0.2f;
        [SerializeField] private Ease _popEase;
        [Space(10)]
        [SerializeField] private string _bathTimeMessage;
        [SerializeField] private string _shoutMessage;
        [SerializeField] private string _embarassedMessage;

        private void Awake()
        {
            SetText();
            Reset();
        }

        private void SetText()
        {
            _bathTimeLine.text = _bathTimeMessage;
            _player1Line.text = _shoutMessage;
            _player2Line.text = _shoutMessage;
            _parent1Line.text = _embarassedMessage;
            _parent2Line.text = _embarassedMessage;
        }

        public void Reset()
        {
            _player1Renderer.sprite = _player1Sprites.Idle;
            _player2Renderer.sprite = _player2Sprites.Idle;
            _bathTimeLine.transform.localScale = Vector3.zero;
            _player1Line.transform.localScale = Vector3.zero;
            _player2Line.transform.localScale = Vector3.zero;
            _parent1Line.transform.localScale = Vector3.zero;
            _parent2Line.transform.localScale = Vector3.zero;
        }

        public void ChangeToBath()
        {
            _bathTimeLine.transform.DOScale(Vector3.one, _popSpeed).SetEase(_popEase);
        }

        public void PlayerEmbarassed(int i)
        {
            if (i == 1)
            {
                _player1Renderer.sprite = _player1Sprites.Embarassed;
                _parent1Line.transform.DOScale(Vector3.one, _popSpeed).SetEase(_popEase);
            }
            else if (i == 2)
            {
                _player2Renderer.sprite = _player2Sprites.Embarassed;
                _parent2Line.transform.DOScale(Vector3.one, _popSpeed).SetEase(_popEase);
            }
        }

        public void PlayerShout(int i)
        {
            if (i == 1)
            {
                _player1Renderer.sprite = _player1Sprites.Shouting;
                _player1Line.transform.DOScale(Vector3.one, _popSpeed).SetEase(_popEase);
            }
            else if (i == 2)
            {
                _player2Renderer.sprite = _player2Sprites.Shouting;
                _player2Line.transform.DOScale(Vector3.one, _popSpeed).SetEase(_popEase);
            }
        }
    }
}