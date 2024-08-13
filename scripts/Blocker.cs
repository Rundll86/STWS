using Godot;
using System;

public partial class Blocker
{
    static bool blocked = false;
    public static bool Block()
    {
        blocked = true;
        return blocked;
    }
    public static bool Unblock()
    {
        blocked = false;
        return blocked;
    }
    public static bool IsBlocked()
    {
        return blocked;
    }
    public static void CheckBlockedAndThrow()
    {
        if (blocked)
        {
            throw new Exception("Blocked");
        }
    }
}
