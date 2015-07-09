using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AOCMDB.Controllers.api.V1
{
    /// <summary>
    /// This will note the API methods required of any controller that exposes the Version 1
    /// API of Dependencies.
    /// </summary>
    interface IDependenciesControllerAPIV1<T>
    {
        [ResponseType(typeof(List<T>))]
        Task<IHttpActionResult> GetDependencies();

        [ResponseType(typeof(T))]
        Task<IHttpActionResult> GetDependency(long Id);

        [ResponseType(typeof(void))]
        Task<IHttpActionResult> PutDependency(long id, T dependency);

        [ResponseType(typeof(T))]
        Task<IHttpActionResult> PostDependency(T dependency);

        [ResponseType(typeof(string))]
        Task<IHttpActionResult> GetDependencyName(long Id);

        [ResponseType(typeof(string))]
        Task<IHttpActionResult> GetDependencyFriendlyName(long Id);

        [ResponseType(typeof(string))]
        Task<IHttpActionResult> GetDependencyDetails(long Id);

        [ResponseType(typeof(void))]
        Task<IHttpActionResult> PutDependencyName(long upstreamId, string downstreamId);

        [ResponseType(typeof(string))]
        Task<IHttpActionResult> PutDependencyFriendlyName(long Id, string Value);

        [ResponseType(typeof(string))]
        Task<IHttpActionResult> PutDependencyDetails(long Id, string Value);

        [ResponseType(typeof(List<T>))]
        Task<IHttpActionResult> GetDependencyDownstreamDependencies();

        [ResponseType(typeof(void))]
        Task<IHttpActionResult> PutDependencyDownStreamDependency(long Id, long DownStreamId);

    }
}
