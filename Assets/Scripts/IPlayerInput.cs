using UnityEngine;

// Interface through which players receive input from keyboard, joystick, AI, etc.
public interface IPlayerInput
{
	void MoveDirection(float x, float z); // Move the player in a direction
    void Use(); // Activate totems
    void Drop();
}
