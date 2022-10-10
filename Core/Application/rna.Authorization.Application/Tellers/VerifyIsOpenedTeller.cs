using System;
using MediatR;

namespace rna.Authorization.Application
{
    public class VerifyIsOpenedTeller : IRequest<Unit>
    {
        public string UserId { get; set; }
        public DateTime? OpenDate { get; set; }
        public bool ThrowExceptionIfVerified { get; set; } = false;
    }
}
