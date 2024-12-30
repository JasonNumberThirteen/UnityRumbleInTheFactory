public class EnemyType
{
	private readonly int index;
	private readonly bool isBonus;

	public EnemyType(int index, bool isBonus)
	{
		this.index = index;
		this.isBonus = isBonus;
	}

	public int GetIndex() => index;
	public bool IsBonus() => isBonus;
}