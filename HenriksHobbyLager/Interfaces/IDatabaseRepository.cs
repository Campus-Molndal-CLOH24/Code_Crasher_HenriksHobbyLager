namespace HenriksHobbyLager.Interfaces
{
    public interface IDatabaseRepository<T>
    {
        void Add(T item);
        void Update(T item);
        void Delete(int id);
        IEnumerable<T> GetAll();
    }

}
