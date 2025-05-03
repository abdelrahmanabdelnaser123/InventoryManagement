using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Services.DTOS;
using InventoryManagement.Services.Response;

namespace InventoryManagement.Services.IServices
{
  public interface IAuthServices
    {
        Task<Response<bool>> Register(RegisterDTO registerDTO);
        Task<Response<LoginResponseDTO>> Login(LoginDTO loginDTO);
     
    }
}
