using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Ketoan.BL;
using MISA.AMIS.Ketoan.Common.Entities;
using MySqlConnector;

namespace MISA.AMIS.Ketoan.API.Controllers
{
    [ApiController]
    public class DepartmentsController : BasesController<Department>
    {
        public DepartmentsController(IBaseBL<Department> baseBL) : base(baseBL)
        {

        }
    }
}
