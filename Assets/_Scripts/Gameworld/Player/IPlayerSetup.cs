namespace PolygonArcana.Settings
{
	public interface IPlayerSetup
	{
		AttackPattern AttackPattern { get; }
		int MaxHealth { get; }
		float MoveSpeed { get; }
		float AttackPeriod { get; }
		IBulletSetup BulletSetup { get; }
	}
}
