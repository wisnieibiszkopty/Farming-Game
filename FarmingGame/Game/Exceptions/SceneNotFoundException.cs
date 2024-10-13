using System;

namespace FarmingGame.Game.Exceptions;

public class SceneNotFoundException : Exception
{
    public SceneNotFoundException(string message) : base(message) {}
}