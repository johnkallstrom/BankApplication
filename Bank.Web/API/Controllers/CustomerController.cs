using AutoMapper;
using Bank.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Web.API.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public CustomerController(
            IMapper mapper,
            ICustomerService customerService)
        {
            _mapper = mapper;
            _customerService = customerService;
        }
    }
}