
namespace Quackmageddon
{
    /// <summary>
    /// Contains gameplay event types
    /// </summary>
    public enum GameplayEventType
    {
        BulletFired,

        EnemySpawned,
        EnemyHit,
        EnemyDestroyed,
        EnemyBeakshot,

        PlayerHit,

        ScoreUpdate,
        HealthUpdate,

        PauseSpawning,
        ResumeSpawning
    }
}