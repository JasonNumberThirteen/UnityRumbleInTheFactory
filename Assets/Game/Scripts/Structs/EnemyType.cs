public readonly struct EnemyType
{
	public int Index {get;}
	public bool IsBonus {get;}

	public EnemyType(int index, bool isBonus)
	{
		(Index, IsBonus) = (index, isBonus);
	}
}