using System;

namespace ProProxy
{
    public interface IShell
    {
        dynamic PreDelegate { get; set; }
        dynamic PostDelegate { get; set; }
        dynamic CatchDelegate { get; set; }
    }
}