namespace Gemfruit.Mod.API
{
    /// <summary>
    /// Methods annotated with this attribute will be expected to act like
    /// hooks for the Game Bus to call when an event is fired on
    /// the Game Bus. Any method declared this way should take
    /// one parameter of a type deriving EventBase - any method that
    /// does not fit this prototype will be rejected from registration,
    /// and will not be called. 
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class GameBusHookAttribute : System.Attribute
    {
    }
}