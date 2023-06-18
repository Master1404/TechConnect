namespace TechConnect.Models
{
    public interface IRecord<TId>
      where TId : struct, IEquatable<TId>
    {
        public TId Id { get; set; }
    }
}
