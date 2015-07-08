using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCMDB.Controllers.api.V1
{
    /// <summary>
    /// This will note the API methods required of any controller that exposes the Version 1
    /// API of Dependencies.
    /// </summary>
    interface IDependenciesControllerAPIV1<T>
    {
        ICollection<T> Get(string APIVersion);

        T Get(string APIVersion, long Id);

        string GetName(string APIVersion, long Id);

        string GetFriendlyName(string APIVersion, long Id);

        string GetDetails(string APIVersion, long Id);

        string SetName(string APIVersion, long Id, string Value);

        string SetFriendlyName(string APIVersion, long Id, string Value);

        string SetDetails(string APIVersion, long Id, string Value);

        ICollection<T> GetDownstreamDependencies(string APIVersion);

        T GetDownstreamDependencies(string APIVersion, long Id);

        void AddDownStreamDependency(string APIVersion, long Id, long DownStreamId);

    }
}
