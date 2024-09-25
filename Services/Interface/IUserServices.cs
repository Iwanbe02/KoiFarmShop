using BusinessObjects.ReqDto;
using BusinessObjects.ResDto;
using BusinessObjects.ReturnCommon;
using DataAccessObjects.DTOs.AccountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IUserServices
    {
        Return<LoginResDto> Login(string email, string password);
        void Register(RegisterReqDto registerReqDto);
    }
}
