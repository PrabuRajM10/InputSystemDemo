using UnityEngine;

namespace Gameplay
{
    public class DropBomb : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            var player = requiredObject.GetComponent<Player>();
            player.DropBomb();
        }
    }
}