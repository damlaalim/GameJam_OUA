using UnityEngine;

namespace _game.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        PlayerFall playerfall;
        private void Start()
        {
            playerfall = GetComponent<PlayerFall>();
        }
        public void Dead()
        {
            playerfall.ResetPosition();
        }
    }
}