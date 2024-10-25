using Helpers;
using Managers;
using Ui.Screens;
using UnityEngine;

namespace Ui.ScreenHandlers
{
    public class HomeScreenHandler : BaseScreenHandler
    {
        [SerializeField] private HomeScreenScreen homeScreen;

        private void OnEnable()
        {
            homeScreen.OnPlayButtonPressed += PlayButtonPressed;
            homeScreen.OnQuitButtonPressed += OnQuitButtonPressed;
        }


        private void OnDisable()
        {
            homeScreen.OnPlayButtonPressed -= PlayButtonPressed;
            homeScreen.OnQuitButtonPressed -= OnQuitButtonPressed;
        }
        private void OnQuitButtonPressed()
        {
            Utils.QuitGame();
        }

        private void PlayButtonPressed()
        {
            SwitchScreen(GameScreen.CardPicker);
            GameManager.Instance.ChangeState(GameState.CardPicker);
        }
    }
}