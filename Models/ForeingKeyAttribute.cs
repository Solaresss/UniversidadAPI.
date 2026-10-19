
namespace UniversidadAPI.Models
{
    internal class ForeingKeyAttribute : Attribute
    {
        private string v;

        public ForeingKeyAttribute(string v)
        {
            this.v = v;
        }
    }
}