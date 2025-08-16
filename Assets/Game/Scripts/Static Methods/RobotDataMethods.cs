public static class RobotDataMethods
{
	public static bool RobotDataHasOrdinalNumber(RobotData robotData, int ordinalNumber) => GetOrdinalNumber(robotData) == ordinalNumber;
	public static int GetOrdinalNumber(RobotData robotData) => robotData != null ? robotData.GetOrdinalNumber() : -1;
}