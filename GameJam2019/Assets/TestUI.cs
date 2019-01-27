using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Joystick1Button0)
         || Input.GetKey(KeyCode.Joystick1Button1)
         || Input.GetKey(KeyCode.Joystick1Button2)
         || Input.GetKey(KeyCode.Joystick1Button3)

         || Input.GetKey(KeyCode.Joystick1Button4)

         || Input.GetKey(KeyCode.Joystick1Button5)

         || Input.GetKey(KeyCode.Joystick1Button6)

         || Input.GetKey(KeyCode.Joystick1Button7)

         || Input.GetKey(KeyCode.Joystick1Button8)

         || Input.GetKey(KeyCode.Joystick1Button9)

         ||  Input.GetKey(KeyCode.Joystick1Button10)

         ||  Input.GetKey(KeyCode.Joystick1Button11)

         ||  Input.GetKey(KeyCode.Joystick1Button12)

         ||  Input.GetKey(KeyCode.Joystick1Button13)

         ||  Input.GetKey(KeyCode.Joystick1Button14)

         ||  Input.GetKey(KeyCode.Joystick1Button15)

         ||  Input.GetKey(KeyCode.Joystick1Button16)

         ||  Input.GetKey(KeyCode.Joystick1Button17)

         ||  Input.GetKey(KeyCode.Joystick1Button18)

         ||  Input.GetKey(KeyCode.Joystick1Button19)
         )
        {
            Debug.Log("0 " + Input.GetKey(KeyCode.Joystick1Button0));
            Debug.Log("1 " + Input.GetKey(KeyCode.Joystick1Button1));
            Debug.Log("2 " + Input.GetKey(KeyCode.Joystick1Button2));
            Debug.Log("3 " + Input.GetKey(KeyCode.Joystick1Button3));
            Debug.Log("4 " + Input.GetKey(KeyCode.Joystick1Button4));
            Debug.Log("5 " + Input.GetKey(KeyCode.Joystick1Button5));
            Debug.Log("6 " + Input.GetKey(KeyCode.Joystick1Button6));
            Debug.Log("7 " + Input.GetKey(KeyCode.Joystick1Button7));
            Debug.Log("8 " + Input.GetKey(KeyCode.Joystick1Button8));
            Debug.Log("9 " + Input.GetKey(KeyCode.Joystick1Button9));
            Debug.Log("10 " + Input.GetKey(KeyCode.Joystick1Button10));
            Debug.Log("11 " + Input.GetKey(KeyCode.Joystick1Button11));
            Debug.Log("12 " + Input.GetKey(KeyCode.Joystick1Button12));
            Debug.Log("13 " + Input.GetKey(KeyCode.Joystick1Button13));
            Debug.Log("14 " + Input.GetKey(KeyCode.Joystick1Button14));
            Debug.Log("15 " + Input.GetKey(KeyCode.Joystick1Button15));
            Debug.Log("16 " + Input.GetKey(KeyCode.Joystick1Button16));
            Debug.Log("17 " + Input.GetKey(KeyCode.Joystick1Button17));
            Debug.Log("18 " + Input.GetKey(KeyCode.Joystick1Button18));
            Debug.Log("19 " + Input.GetKey(KeyCode.Joystick1Button19));
        }
    }
}
