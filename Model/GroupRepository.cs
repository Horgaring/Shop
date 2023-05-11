class GroupRepository : IRepository
{
    public GroupRepository(){
        Groups = new();
    }

    private List<string> Groups { get; set; }
    public string? Get(string key) => Groups.Contains(key)? Groups[Groups.IndexOf(key)]: null;

    public void Remove(string key){
        if (Groups.Contains(key) == false)
            return;
        Groups.Remove(key);
    }
    public bool Contains(string key){
        if (Groups.Contains(key)){
            return true;
        }
        return false;
    }
    public void Set(string key) => Groups.Add(key);

    public IEnumerable<string> GetS(string key)
    {
        var result = new List<string>();
        foreach (var item in Groups){
            if (item.Contains(key)){
                result.Add(item);
            }
        }
        return result;
    }
}