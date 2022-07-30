using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.LIB.Data
{
    public class ConstVar
    {
        public const string SP_empdetails_getall_paging = "EXEC [empdetails_getall_paging] @PageNum={0},@PageSize={1}";
        public const string SP_empsalary_getall_paging = "EXEC [empsalary_getall_paging] @PageNum={0},@PageSize={1}";
    }
}
