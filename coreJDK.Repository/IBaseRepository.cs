using coreJDK.Common;

namespace coreJDK.Repository
{

    public interface IBaseRepository<T> : IDependency where T : class
    {

    }
}
