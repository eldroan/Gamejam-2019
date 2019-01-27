using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public string Name;
    public KeyCode KeyCode;
    public KeyCode KeyCodeJoystick;

}
public class PlayerInputs
{
    private static Dictionary<string, List<Action>> playerInputCollection = new Dictionary<string, List<Action>>
    {
        { Constants.PLAYER_1_TAG, new List<Action>
            {
                new Action { Name = Constants.JUMP, KeyCode = KeyCode.W},
                new Action { Name = Constants.LEFTH, KeyCode = KeyCode.A},
                new Action { Name = Constants.RIGHT, KeyCode = KeyCode.D},
                new Action { Name = Constants.ATTACK, KeyCode = KeyCode.E},
                new Action { Name = Constants.BLOCK, KeyCode = KeyCode.Q}
            }
        },
        { Constants.PLAYER_2_TAG, new List<Action>
            {
                new Action { Name = Constants.JUMP, KeyCode = KeyCode.I},
                new Action { Name = Constants.LEFTH, KeyCode = KeyCode.J},
                new Action { Name = Constants.RIGHT, KeyCode = KeyCode.L},
                new Action { Name = Constants.ATTACK, KeyCode = KeyCode.O},
                new Action { Name = Constants.BLOCK, KeyCode = KeyCode.U}
            }
        }
    };

    public static KeyCode GetKey(string player, string action)
    {
        return playerInputCollection.Select(x => x).Where(x => x.Key == player).FirstOrDefault().Value.Select(x => x).Where(x => x.Name == action).FirstOrDefault().KeyCode;
    }


}
