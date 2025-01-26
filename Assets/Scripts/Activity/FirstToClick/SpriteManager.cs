using System;
using UnityEngine;

namespace Activity.FirstToClick
{
    [Serializable]
    public struct PlayerSprites
    {
        public Sprite Idle;
        public Sprite Winner;
        public Sprite Loser;
    }
    
    public class SpriteManager : MonoBehaviour
    {
        [Header("Sprite Parameters")]
        [SerializeField] private SpriteRenderer _player1Renderer;
        [SerializeField] private PlayerSprites _player1Sprites;
        [SerializeField] private SpriteRenderer _player2Renderer;
        [SerializeField] private PlayerSprites _player2Sprites;

        private void Awake()
        {
            _player1Renderer.sprite = _player1Sprites.Idle;
            _player2Renderer.sprite = _player2Sprites.Idle;
        }

        public void ChangeSprites(ActivityData data)
        {
            if (data.Target == Loser.Player1)
            {
                _player1Renderer.sprite = _player1Sprites.Loser;
                _player2Renderer.sprite = _player2Sprites.Winner;
            }
            else if (data.Target == Loser.Player2)
            {
                _player1Renderer.sprite = _player1Sprites.Winner;
                _player2Renderer.sprite = _player2Sprites.Loser;
            }
            else
            {
                _player1Renderer.sprite = _player1Sprites.Loser;
                _player2Renderer.sprite = _player2Sprites.Loser;
            }
        }
    }
}