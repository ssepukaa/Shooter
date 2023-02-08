using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerV : MonoBehaviour
    {
        private PlayerC playerC;

        private void Awake()
        {
            playerC = GetComponent<PlayerC>();
        }





    }
}