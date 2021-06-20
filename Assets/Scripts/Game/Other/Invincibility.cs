using UnityEngine.U2D;
using System;

public class Invincibility : IUpdate, IDisposable
{
    private const int INVINCIBILITY_FREQUENCY = 6;
    private const float INVINCIBILITY_BLINKING = 0.5f;

    private bool _isInvincible;

    private Timer _invincibletyTimer;

    private SpriteShapeRenderer _shipShapeRenderer;

    public bool IsInvincible => _isInvincible;

    public Invincibility(BaseUnitView shipView)
    {
        if (shipView is ShipView ship)
        {
            _shipShapeRenderer = ship.ShipShapeRenderer;
        }
        _invincibletyTimer = new Timer();
    }


    public void UpdateTick()
    {
        CountTime();
    }

    public void StartInvincibility()
    {
        SetInvincibility(true);
        _invincibletyTimer.InitFrequency(INVINCIBILITY_FREQUENCY, INVINCIBILITY_BLINKING);
        _invincibletyTimer.OnFrequencyEnd += EndInvincibility;
    }

    private void SetInvincibility(bool isInvincible)
    {
        _isInvincible = isInvincible;
        if (isInvincible)
        {
            _invincibletyTimer.OnEndCountDown += Blink;
        }
        else
        {
            _invincibletyTimer.OnEndCountDown -= Blink;
        }
    }

    private void Blink()
    {
        if (_shipShapeRenderer.enabled)
        {
            _shipShapeRenderer.enabled = false;
        }
        else
        {
            _shipShapeRenderer.enabled = true;
        }
    }

    private void CountTime()
    {
        _invincibletyTimer.CheckFrequency();
    }

    private void EndInvincibility()
    {
        SetInvincibility(false);
        _shipShapeRenderer.enabled = true;
        _invincibletyTimer.OnFrequencyEnd -= EndInvincibility;
    }

    public void Dispose()
    {
        _invincibletyTimer.OnFrequencyEnd -= EndInvincibility;
    }
}
