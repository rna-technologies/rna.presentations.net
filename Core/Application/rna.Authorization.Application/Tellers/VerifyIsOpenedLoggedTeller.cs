using System;
using MediatR;

namespace rna.Authorization.Application
{
    public class VerifyIsOpenedLoggedTeller : IRequest<Unit>
    {
        public DateTime? OpenDate { get; set; }
    }
}
