using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace AOCMDB.Models.Data
{
    /// <summary>
    /// This may represent a physical or virtual server/appliance, such as a windows server, Mainframe, NAS, SAN, or vendor appliance (TPAM, Nvidia Grid).
    /// Generally this should refer to a scoped entity and not a platform, such as Azure Cloud, or VMAX, but use your best judgment. In the example of
    /// Nvidia grid, this object would represent a single physical box (ie. Appliance Box 01), not the collective which would be summarized as a technology dependency. 
    /// </summary>
    public class ServerOrAppliance : Dependency
    {
        

    }
}