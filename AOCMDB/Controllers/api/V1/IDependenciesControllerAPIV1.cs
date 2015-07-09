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
        public async Task<IHttpActionResult> Get(string APIVersion);

        [ResponseType(typeof(T))]
        public async Task<IHttpActionResult> Get(long Id);

        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetName(long Id);

        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetFriendlyName(long Id);

        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetDetails(long Id);

        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> SetName(long Id, string Value);

        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> SetFriendlyName(long Id, string Value);

        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> SetDetails(long Id, string Value);

        [ResponseType(typeof(List<T>))]
        public async Task<IHttpActionResult> GetDownstreamDependencies();

        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDownStreamDependency(long Id, long DownStreamId);

    }
}
