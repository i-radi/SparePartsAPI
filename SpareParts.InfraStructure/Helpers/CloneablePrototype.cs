using System.Text.Json;
namespace SpareParts.InfraStructure.Helpers
{
    public abstract class CloneablePrototype<T>
    {
        /// <summary>
        /// Shallow copy
        /// Copy Stack Memory Only
        /// </summary>
        public T Clone() => (T)this.MemberwiseClone();


        /// <summary>
        /// Deep copy
        /// Copy both stack and heep Memory
        /// </summary>
        public T DeepCopy() => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(this))!;

    }
}
