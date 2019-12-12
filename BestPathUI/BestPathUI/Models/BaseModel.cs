using Models.Interfaces;

namespace Models
{
    public abstract class BaseModel : IBaseModel
    {
        public virtual int Id { get; set; }
    }
}