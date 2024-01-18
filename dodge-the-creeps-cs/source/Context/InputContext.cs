using Teoti.App;

namespace Teoti.Context;

public class InputContext
{
    //-----------------------------------------------------------------------------
    // Private :: Variables
    //-----------------------------------------------------------------------------

    private readonly GameService _gameService;

    //-----------------------------------------------------------------------------
    // Constructors
    //-----------------------------------------------------------------------------

    public InputContext(GameService gameService)
    {
        _gameService = gameService;
    }
    
    public void Init()
    {
        // https://docs.godotengine.org/en/stable/tutorials/inputs/inputevent.html
        // https://docs.godotengine.org/en/stable/classes/class_inputmap.html#class-inputmap
        // https://gamedevacademy.org/inputeventaction-in-godot-complete-guide/
        // TODO why isn't this working?
        
        // Define an input action "move_right" and assign a key event to it
        // InputEventKey input_event = new InputEventKey();
        // input_event.PhysicalKeycode = Key.Right;
        // InputMap.ActionAddEvent("move_right", input_event);

        // input_event = new InputEventKey();
        // input_event.PhysicalKeycode = Key.Left;
        // InputMap.ActionAddEvent("move_left", input_event);
        //       
        // input_event = new InputEventKey();
        // input_event.PhysicalKeycode = Key.Up;
        // InputMap.ActionAddEvent("move_up", input_event);
        //       
        // input_event = new InputEventKey();
        // input_event.PhysicalKeycode = Key.Down;
        // InputMap.ActionAddEvent("move_down", input_event);
    }
}