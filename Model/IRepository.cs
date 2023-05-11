public interface IRepository{
    void Set(string key);
    string? Get(string key);
    void Remove(string key);
    bool Contains(string key);
    IEnumerable<string> GetS(string key);
}