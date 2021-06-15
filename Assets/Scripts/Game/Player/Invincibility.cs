using UnityEngine.U2D;

public class Invincibility
{
    private const int INVINCIBILITY_FREQUENCY = 6;
    private const float INVINCIBILITY_BLINKING = 0.5f;

    private bool _isInvincible;

    private Timer _invincibletyTimer;

    private SpriteShapeRenderer _shipShapeRenderer;

    public bool IsInvincible => _isInvincible;

    public Invincibility(ShipView shipView)
    {
        _shipShapeRenderer = shipView.ShipShapeRenderer;
        _invincibletyTimer = new Timer();
        UpdatingController.SubscribeToTUpdate(CountTime);
    }

    private void CountTime()
    {
        _invincibletyTimer.CheckFrequency();
    }

    public void StartInvincibility()
    {
        SetInvincibility(true);
        _invincibletyTimer.InitFrequency(INVINCIBILITY_FREQUENCY, INVINCIBILITY_BLINKING);
        _invincibletyTimer.OnFrequencyEnd += EndInvincibility;
    }

    private void EndInvincibility()
    {
        SetInvincibility(false);
        _shipShapeRenderer.enabled = true;
        _invincibletyTimer.OnFrequencyEnd -= EndInvincibility;

    }

    public void SetInvincibility(bool isInvincible)
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

    public void Blink()
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
}
