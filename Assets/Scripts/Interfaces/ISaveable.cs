namespace Interfaces
{
    public interface ISaveable
    {
        void Save(int uniqueId);
        void Load(int uniqueId);
    }
}