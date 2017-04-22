using UnityEngine;
using UnityEngine.InputNew;

// GENERATED FILE - DO NOT EDIT MANUALLY
public class PlayerControlls : ActionMapInput {
	public PlayerControlls (ActionMap actionMap) : base (actionMap) { }
	
	public AxisInputControl @moveX { get { return (AxisInputControl)this[0]; } }
	public AxisInputControl @moveY { get { return (AxisInputControl)this[1]; } }
	public Vector2InputControl @move { get { return (Vector2InputControl)this[2]; } }
	public ButtonInputControl @fire { get { return (ButtonInputControl)this[3]; } }
	public ButtonInputControl @jump { get { return (ButtonInputControl)this[4]; } }
	public ButtonInputControl @action { get { return (ButtonInputControl)this[5]; } }
}
