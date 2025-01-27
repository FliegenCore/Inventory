namespace Common
{
    public class AssetName
    {
        public string Name { get; set; }

        public AssetName(string name)
        {
            Name = name;
        }

        public static implicit operator AssetName(string name)
        {
            return new AssetName(name);
        }
    }
}
