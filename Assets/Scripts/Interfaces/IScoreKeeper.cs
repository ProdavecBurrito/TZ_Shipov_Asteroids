using System;

public interface IScoreKeeper
{
    public event Action OnPlayerHit;

    public abstract void GiveScore();
}