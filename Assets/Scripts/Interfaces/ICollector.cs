
public interface ICollector {
    /// <summary>
    /// Activated by a trigger
    /// </summary>
    /// <param name="power">New Power</param>
    public void SetPowerItem(EquipmentType power);
}
