using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Action
{
    public string Name;
    public KeyCode KeyCode;
    public string Button;
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
                new Action { Name = Constants.BLOCK, KeyCode = KeyCode.Q},
                new Action { Name = Constants.DOWN, KeyCode = KeyCode.S},
                new Action { Name = Constants.UP, KeyCode = KeyCode.W},
                new Action { Name = Constants.ESCAPE, KeyCode = KeyCode.Escape},
                new Action { Name = Constants.SELECT, KeyCode = KeyCode.E}
            }
        },
        { Constants.PLAYER_2_TAG, new List<Action>
            {
                new Action { Name = Constants.JUMP, KeyCode = KeyCode.I},
                new Action { Name = Constants.LEFTH, KeyCode = KeyCode.J},
                new Action { Name = Constants.RIGHT, KeyCode = KeyCode.L},
                new Action { Name = Constants.ATTACK, KeyCode = KeyCode.O},
                new Action { Name = Constants.BLOCK, KeyCode = KeyCode.U},
                new Action { Name = Constants.DOWN, KeyCode = KeyCode.K},
                new Action { Name = Constants.ESCAPE, KeyCode = KeyCode.Escape},
                new Action { Name = Constants.UP, KeyCode = KeyCode.I},
                new Action { Name = Constants.SELECT, KeyCode = KeyCode.O}
            }
        }
    };

    private static Dictionary<string, List<Action>> playerInputCollectionJoystick = new Dictionary<string, List<Action>>
    {
        { Constants.PLAYER_1_TAG, new List<Action>
            {
                new Action { Name = Constants.JUMP, Button = "Jump1"},
                new Action { Name = Constants.ATTACK, Button = "Attack1"},
                new Action { Name = Constants.BLOCK, Button = "Block1"},
                new Action { Name = Constants.SELECT, Button = "Select1"},
                new Action { Name = Constants.ESCAPE, Button = "Escape1"}

            }
        },
        { Constants.PLAYER_2_TAG, new List<Action>
            {
                new Action { Name = Constants.JUMP, Button = "Jump2"},
                new Action { Name = Constants.ATTACK, Button = "Attack2"},
                new Action { Name = Constants.BLOCK, Button = "Block2"},
                new Action { Name = Constants.SELECT, Button = "Select2"},
                new Action { Name = Constants.ESCAPE, Button = "Escape2"}


            }
        }
    };

    internal static bool GetKeyDown(string player, string action)
    {
        var joyst = Input.GetJoystickNames();

        if (joyst.Length == 1)
        {
            if (player == Constants.PLAYER_2_TAG)
            {
                return Input.GetButtonDown(playerInputCollectionJoystick.Select(x => x).Where(x => x.Key == player).FirstOrDefault().Value.Select(x => x).Where(x => x.Name == action).FirstOrDefault().Button);
            }
            else
            {
                return Input.GetKeyDown(playerInputCollection.Select(x => x).Where(x => x.Key == player).FirstOrDefault().Value.Select(x => x).Where(x => x.Name == action).FirstOrDefault().KeyCode);
            }
        }

        if (joyst.Length == 2)
        {
            var a = playerInputCollectionJoystick.Select(x => x).Where(x => x.Key == player).FirstOrDefault();
            var b = a.Value.Select(x => x).Where(x => x.Name == action).FirstOrDefault();
            var button = b.Button;
            return Input.GetButtonDown(button);
        }

        return Input.GetKeyDown(playerInputCollection.Select(x => x).Where(x => x.Key == player).FirstOrDefault().Value.Select(x => x).Where(x => x.Name == action).FirstOrDefault().KeyCode);
    }

    /// <summary>
    /// Deprecated. El original. Antes de los joysticks
    /// </summary>
    /// <param name="player"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    //public static KeyCode GetKey(string player, string action)
    //{
    //    var joyst = Input.GetJoystickNames();

    //    if (joyst.Length == 1)
    //    {
    //        if (player == Constants.PLAYER_2_TAG)
    //        {
    //            return playerInputCollectionJoystick.Select(x => x).Where(x => x.Key == player).FirstOrDefault().Value.Select(x => x).Where(x => x.Name == action).FirstOrDefault().KeyCode;

    //        }
    //        else
    //        {
    //            return playerInputCollection.Select(x => x).Where(x => x.Key == player).FirstOrDefault().Value.Select(x => x).Where(x => x.Name == action).FirstOrDefault().KeyCode;
    //        }
    //    }

    //    if (joyst.Length == 2)
    //    {
    //        return playerInputCollectionJoystick.Select(x => x).Where(x => x.Key == player).FirstOrDefault().Value.Select(x => x).Where(x => x.Name == action).FirstOrDefault().KeyCode;
    //    }

    //    return playerInputCollection.Select(x => x).Where(x => x.Key == player).FirstOrDefault().Value.Select(x => x).Where(x => x.Name == action).FirstOrDefault().KeyCode;
    //}

    public static bool GetKeyUp(string player, string action)
    {
        var joyst = Input.GetJoystickNames();

        if (joyst.Length == 1)
        {
            if (player == Constants.PLAYER_2_TAG)
            {
                return Input.GetButtonUp(playerInputCollectionJoystick.Select(x => x).Where(x => x.Key == player).FirstOrDefault().Value.Select(x => x).Where(x => x.Name == action).FirstOrDefault().Button);

            }
            else
            {
                return Input.GetKeyUp(playerInputCollection.Select(x => x).Where(x => x.Key == player).FirstOrDefault().Value.Select(x => x).Where(x => x.Name == action).FirstOrDefault().KeyCode);
            }
        }

        if (joyst.Length == 2)
        {
            return Input.GetButtonUp(playerInputCollectionJoystick.Select(x => x).Where(x => x.Key == player).FirstOrDefault().Value.Select(x => x).Where(x => x.Name == action).FirstOrDefault().Button);
        }

        return Input.GetKeyUp(playerInputCollection.Select(x => x).Where(x => x.Key == player).FirstOrDefault().Value.Select(x => x).Where(x => x.Name == action).FirstOrDefault().KeyCode);
    }

    public static bool GetAxisOrKey(string player, string action)
    {
        var joyst = Input.GetJoystickNames();

        if (joyst.Length == 1)
        {
            if (player == Constants.PLAYER_2_TAG)
            {
                if (Input.GetAxis("HorizontalJ1") < 0 && action == Constants.LEFTH)
                    return true;
                if (Input.GetAxis("HorizontalJ1") > 0 && action == Constants.RIGHT)
                    return true;

                if (Input.GetAxisRaw("VerticalJ1") < 0 && action == Constants.DOWN)
                    return true;
                if (Input.GetAxisRaw("VerticalJ1") > 0 && action == Constants.UP)
                    return true;

                return false;

            }
            else
            {
                return Input.GetKey(playerInputCollection.Select(x => x).Where(x => x.Key == player).FirstOrDefault().Value.Select(x => x).Where(x => x.Name == action).FirstOrDefault().KeyCode);
            }
        }

        if (joyst.Length == 2)
        {
            if (player == Constants.PLAYER_2_TAG)
            {
                if (Input.GetAxis("HorizontalJ1") < 0 && action == Constants.LEFTH)
                    return true;
                if (Input.GetAxis("HorizontalJ1") > 0 && action == Constants.RIGHT)
                    return true;
                if (Input.GetAxisRaw("VerticalJ1") < 0 && action == Constants.DOWN)
                    return true;
                if (Input.GetAxisRaw("VerticalJ1") > 0 && action == Constants.UP)
                    return true;
                return false;
            }
            else
            {
                if (Input.GetAxis("HorizontalJ2") < 0 && action == Constants.LEFTH)
                    return true;
                if (Input.GetAxis("HorizontalJ2") > 0 && action == Constants.RIGHT)
                    return true;
                if (Input.GetAxisRaw("VerticalJ2") < 0 && action == Constants.DOWN)
                    return true;
                if (Input.GetAxisRaw("VerticalJ2") > 0 && action == Constants.UP)
                    return true;
                return false;
            }
        }

        return Input.GetKey(playerInputCollection.Select(x => x).Where(x => x.Key == player).FirstOrDefault().Value.Select(x => x).Where(x => x.Name == action).FirstOrDefault().KeyCode);
    }
}
