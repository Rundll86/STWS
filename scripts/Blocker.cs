using Godot;
using System;

public partial class Blocker
{
    static bool blocked = false;
    static bool timePause = false;
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
    public static bool PauseTime()
    {
        blocked = true;
        timePause = true;
        return timePause;
    }
    public static bool ResumeTime()
    {
        blocked = false;
        timePause = false;
        return timePause;
    }
    public static bool IsTimePaused()
    {
        return timePause;
    }
    public static bool IsBlocked()
    {
        return blocked;
    }
}
