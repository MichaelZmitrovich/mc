namespace MC.Core.Dal
{
    public interface IDao
    {
        void Delete(object entity);
        void SaveChanges();
    }
}
