using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskForNetDeveloperPosition.Models;

namespace TestTaskForNetDeveloperPosition.Bll.Contract
{
    public interface IOutput
    {
        IEnumerable<JoinResultModel> JoinTable(int id);
        IEnumerable<JoinResultModel> JoinTableGroup(IEnumerable<JoinResultModel> joinResultModels);

    }
}
