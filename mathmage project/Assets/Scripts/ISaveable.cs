using System.Collections.Generic;
public interface ISaveable
{
    public List<object> SaveData();

    public void LoadData(List<object> data);

}
