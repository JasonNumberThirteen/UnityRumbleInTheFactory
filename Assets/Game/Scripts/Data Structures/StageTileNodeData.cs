public class StageTileNodeData
{
	public float TotalCost => RealValue + HeuristicValue;
	
	public StageTileNode Parent {get; set;}
	public float RealValue {get; private set;}
	public float HeuristicValue {get; private set;}

	public void SetValues(StageTileNode parentStageTileNode, float realValue, float heuristicValue)
	{
		(Parent, RealValue, HeuristicValue) = (parentStageTileNode, realValue, heuristicValue);
	}
}