using Managers;
using UnityEngine;

namespace Gameplay
{
    public class Coin : Collectables
    {
        protected override void OnCollected()
        {
            respectiveManager.OnCollectablesCollected();
        }
    }
}