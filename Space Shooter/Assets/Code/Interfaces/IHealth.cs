namespace SpaceShooter
{
	public interface IHealth
	{
		int CurrentHealth { get; }
		bool IsDead { get; }
        int MaxHealth { get; }
		void IncreaseHealth( int amount );
		void DecreaseHealth( int amount );
	}
}
