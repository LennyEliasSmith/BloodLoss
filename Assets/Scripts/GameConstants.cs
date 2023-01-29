
using UnityEngine;

namespace Main.Game
{
    public class GameConstants
    {
        public enum Gamestates
        {
            RUNNING,
            PAUSED,
            HUNTED,
            END
        }
        public enum PlayerStates
        {
            BLEEDING,
            NORMAL
        }
        public enum StageStates
        {
            STATIC,
            CHANGING
        }

        public static Gamestates gamestates;
        public static PlayerStates playerStates;
        public static StageStates stageStates;

        public const string VerticalAxis = "Vertical";
        public const string HorizontalAxis = "Horizontal";

        public const string MouseX = "Mouse X";
        public const string MouseY = "Mouse Y";

        public const string ScareTag = "Scare";
        public const string PlayerTag = "Player";

        public static string enemyGlitchAnim = "Armature_glitcyboi";
        public static string enemyWalkAnim = "Armature_Walk";

        public static string interactionInput = "E";
        public static string keyCardInteractionInput = "No Key Card";

        public static string empty = " ";

    }
}

