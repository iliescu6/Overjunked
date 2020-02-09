using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InputManager : MonoBehaviour
{

    public static InputManager instance;

    public bool playersInputInitialization = false;

    List <PlayerAssignedController >  playersControllers= new List<PlayerAssignedController >();

    private void Awake()
    {
        instance = this;
        PlayerMovementResponsive.PlayerAdded += OnPlayerAdded;
        PlayerMovementResponsive.PlayerRemoved += OnPlayerRemoved;
    }

	private int JoystickCount
	{
		get
		{
			int count = 0;
			for (int i = 0; i < Input.GetJoystickNames().Length; i++)
				if (!string.IsNullOrEmpty(Input.GetJoystickNames()[i]))
					count++;
			return count;
		}
	}

    private int AssignedJoysticksCount
    {
        get
        {
            int count = 0;
            foreach (PlayerAssignedController player in playersControllers)
            {
                if (player.controller.Contains("Joystick"))
                {
                    count++;
                }
            }
            return count;
        }
    }

    private int AssignedKeyboardCount
    {
        get
        {
            int count = 0;
            foreach (PlayerAssignedController player in playersControllers)
            {
                if (player.controller.Contains("Keyboard"))
                {
                    count++;
                }
            }
            return count;
        }
    }

  

	void OnPlayerAdded(PlayerMovementResponsive player)
	{

        if (JoystickCount > AssignedJoysticksCount)
        {
            playersControllers.Add(new PlayerAssignedController(player, "Joystick" + (AssignedJoysticksCount + 1).ToString()));

        }
        else
        {
            playersControllers.Add(new PlayerAssignedController(player, "Keyboard" + (AssignedKeyboardCount + 1).ToString()));

        }
	}

	void OnPlayerRemoved(PlayerMovementResponsive player)
	{

	}

    public class PlayerAssignedController
    {
        public IPlayerInput player;
        public string controller;

        public PlayerAssignedController(IPlayerInput player, string controller)
        {
            this.player = player;
            this.controller = controller;
        }
    }

    private void Update()
    {

        foreach (PlayerAssignedController  _player in playersControllers)
        {
			float x = 0;
			float y = 0;	

            if (IsRightPressed(_player.controller))
            {
				x = 1;
            }
            if (IsLeftPressed(_player.controller))
            {
				x = -1;
            }
            if (IsUpPressed(_player.controller))
            {
				y = 1;
            }
            if (IsDownPressed(_player.controller))
            {
				y = -1;
            }

			_player.player.MoveDirection(x, y);
            
            if (IsUsePressed(_player.controller))
            {
                _player.player.Use();
            }
            if (IsDropPressed(_player.controller))
            {
                _player.player.Drop();
            }

        }

    }

    public bool IsRightPressed(string controller)
    {
        if (controller.Contains("Joystick"))
        {
            if (Input.GetAxis(controller + "_Horizontal") > 0)
            {
                return true;
            }

        }
        else
        {
            if ((controller == "Keyboard1" && Input.GetKey(KeyCode.D)) || (controller == "Keyboard2" && Input.GetKey(KeyCode.RightArrow)))
            {
                return true;
            }
        }
        return false;
    }
    public bool IsLeftPressed(string controller)
    {
        //Debug.Log("Joystick" + index.ToString() + "_Horizontal");
        if (controller.Contains("Joystick"))
        {
            if (Input.GetAxis(controller + "_Horizontal") < 0)
            {
                return true;
            }

        }
        else
        {
            if ((controller == "Keyboard1" && Input.GetKey(KeyCode.A)) || (controller == "Keyboard2" && Input.GetKey(KeyCode.LeftArrow)))
            {
                return true;
            }
        }
        return false;
    }
    public bool IsUpPressed(string controller)
    {


        if (controller.Contains("Joystick"))
        {
            if (Input.GetAxis(controller + "_Vertical") > 0)
            {
                return true;
            }

        }
        else
        {
            if ((controller == "Keyboard1" && Input.GetKey(KeyCode.W)) || (controller == "Keyboard2" && Input.GetKey(KeyCode.UpArrow)))
            {
                return true;
            }
        }
        return false;
    }
    public bool IsDownPressed(string controller)
    {

        if (controller.Contains("Joystick"))
        {
            if (Input.GetAxis(controller + "_Vertical") < 0)
            {
                return true;
            }

        }
        else
        {
            if ((controller == "Keyboard1" && Input.GetKey(KeyCode.S)) || (controller == "Keyboard2" && Input.GetKey(KeyCode.DownArrow)))
            {
                return true;
            }
        }
        return false;
    }

    public bool IsDropPressed(string controller)
    {
        if ((controller == "Keyboard1" && Input.GetKey(KeyCode.H)|| (controller == "Keyboard2" && Input.GetKey(KeyCode.Backspace))))
        {
            return true;
        }
        else { return false; }
    }

    public bool IsUsePressed(string controller)
    {
        int index = 666;
        if (controller.Contains("Joystick"))
        {
            index = Convert.ToInt32(controller.Replace("Joystick", string.Empty));
        }
        JoystickType type = GetJoystickType(index);
        string keyBinding = "";
        if (type == JoystickType.PS)
        {
            keyBinding = DefaultBindings_PS4.Use;
        }
        else if (type == JoystickType.Xbox)
        {
            keyBinding = DefaultBindings_Xbox.Use;
        }

        if (keyBinding != "")
        {
            KeyCode keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), controller + keyBinding);
            if (Input.GetKeyDown(keyCode))
            {
                return true;
            }
        }
        else
        {
            if ((controller == "Keyboard1" && Input.GetKey(KeyCode.Space)) || (controller == "Keyboard2" && Input.GetKey(KeyCode.Return)))
            {
                return true;
            }
        }
        return false;
    }

    public JoystickType GetJoystickType(int index)
    {
        string name = null;
        if (index <= Input.GetJoystickNames().Length)
        {
             name = Input.GetJoystickNames()[index -1];
        }
        if (!string.IsNullOrEmpty(name))
        { 

            if (name.Contains("Xbox") || name.Contains("XBOX"))
            {
               // Debug.Log("xbox joystick");
                return JoystickType.Xbox;
            }
            else if (name.Contains("PS") || name.Contains("Wireless Controller"))
            {
                //Debug.Log("ps4 joystick");
                return JoystickType.PS;
            }
        }

        return JoystickType.None;
    }
}
public enum JoystickType
{
    Xbox,
    PS,
    None
}
