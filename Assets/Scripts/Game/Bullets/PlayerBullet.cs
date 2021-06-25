using UnityEngine;

public class PlayerBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionType = collision.GetComponent<BaseEnemy>();
        {
            if (collisionType)
            {
                collisionType.GetDamage();
                if (collisionType is CrackingAsteroids asteroidsView)
                {
                    asteroidsView.Crack();
                }
                if (collisionType is IScoreKeeper scoreKeeper)
                {
                    scoreKeeper.GiveScore();
                }
                ChangeActiveState(false);
            }
        }
    }
}
