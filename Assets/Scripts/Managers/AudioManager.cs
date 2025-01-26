using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

namespace SpaceGame.Components
{
    public class AudioManager : MonoBehaviour
    {
        private EventInstance _musicInstance;
        private EventInstance _SnotClick;
        private EventInstance _SnotPop;
        private EventInstance _BathTimeYell;
        private EventInstance _SiblingNo;
        private EventInstance _MillionareCorrect;
        private EventInstance _MillionareWrong;
        private EventInstance _BubbleGunPow;
        private EventInstance _NannyHelp;

        private void Start()
        {
            _musicInstance = RuntimeManager.CreateInstance(FMODEvents.Instance.PleaseMusic);
            _musicInstance.start();
        }

        public void GameMusicStart(int index)
        {
            _musicInstance.setParameterByName("MinigameStatus", index);
        }

        public void SnotClick(int index)
        {
            RuntimeManager.PlayOneShot(FMODEvents.Instance.SnotClick);
        }

        public void SnotPop(int index)
        {
            RuntimeManager.PlayOneShot(FMODEvents.Instance.SnotPop);
        }

        public void BathTimeYell(int index)
        {
            RuntimeManager.PlayOneShot(FMODEvents.Instance.BathTimeYell);
        }

        public void SiblingNo(int index)
        {
            RuntimeManager.PlayOneShot(FMODEvents.Instance.SiblingNo);
        }

        public void MillionareCorrect(int index)
        {
            RuntimeManager.PlayOneShot(FMODEvents.Instance.MillionareCorrect);
        }

        public void MillionareWrong(int index)
        {
            RuntimeManager.PlayOneShot(FMODEvents.Instance.MillionareWrong);
        }

        public void BubbleGunPow(int index)
        {
            RuntimeManager.PlayOneShot(FMODEvents.Instance.BubbleGunPow);
        }

        public void NannyHelp(int index)
        {
            RuntimeManager.PlayOneShot(FMODEvents.Instance.NannyHelp);
        }
    }

}