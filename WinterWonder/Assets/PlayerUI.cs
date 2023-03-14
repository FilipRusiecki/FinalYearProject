using UnityEngine;
using UnityEngine.UI;

namespace Mirror.Examples.Basic
{
    public class PlayerUI : MonoBehaviour
    {
        public Player player;

        [Header("Player Components")]
        public Image image;

        [Header("Child Text Objects")]
        public Text playerNameText;
        public Button buttonToReadyUp;
        /// <summary>
        /// Sets a highlight color for the local player
        /// </summary>
        public void SetLocalPlayer()
        {
            // add a visual background for the local player in the UI
            image.color = new Color(1f, 1f, 1f, 0.1f);
        }

        /// <summary>
        /// This value can change as clients leave and join
        /// </summary>
        /// <param name="newPlayerNumber"></param>
        public void OnPlayerNumberChanged(byte newPlayerNumber)
        {
            playerNameText.text = string.Format("Player {0:00}", newPlayerNumber);
        }

        /// <summary>
        /// Random color set by Player::OnStartServer
        /// </summary>
        /// <param name="newPlayerColor"></param>
        public void OnPlayerColorChanged(Color32 newPlayerColor)
        {
            playerNameText.color = newPlayerColor;
        }

        /// <summary>
        /// Sets the player to ready in the lobby 
        /// </summary>
        public void SetPlayerReady()
        {
            player.GetComponent<NetworkRoomPlayer>().CmdChangeReadyState(true);
            player.enabled = false;
            buttonToReadyUp.gameObject.SetActive(false);
        }
    }
}