using UnityEngine.InputSystem;

public sealed class Inputs
{
	private static Inputs s_instance;
	public static Inputs Instance => s_instance ??= new Inputs();
	
	private InputSystem_Actions _inputSystem;
	public InputAction MaskOn;

	private Inputs()
	{
		SetupInput();
	}
	
	private void SetupInput() {
		_inputSystem = new InputSystem_Actions();
		MaskOn = _inputSystem.Player.MaskOn;
		MaskOn.Enable();
	}
}