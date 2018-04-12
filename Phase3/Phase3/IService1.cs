using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Phase3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        Dictionary<string, int> AvgRatingsPerGenre(int minRating, int maxRating, string titleKeyword, string tagKeyword);

        [OperationContract]
        Dictionary<string, int> CountPerGenre(int minRating, int maxRating, string titleKeyword, string tagKeyword);

    }
    
}
