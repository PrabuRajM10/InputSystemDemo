using System;
using UnityEngine;

namespace Ui.Screens
{
    public enum GameScreen
    {
        Gameplay,
        Setting,
        Home,
        GameResult,
        CardPicker,
        Pause
    }
    public abstract class BaseUi : MonoBehaviour
    {
        [SerializeField] private GameScreen screen;

        protected virtual void OnDisable()
        {
            Reset();
        }

        public GameScreen Screen
        {
            get => screen;
            set => screen = value;
        }

        public virtual void Reset()
        {
            
        }
    }
}